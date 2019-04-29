using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCard : MonoBehaviour
{
    public CombatManager combatManager;
    public PlayerController player;
     

    public Card card;
    private int health;
    private int cost;
    public ActiveCardDisplay display;

    public GameObject button;

    void Start()
    {
        display = GetComponent<ActiveCardDisplay>();
    }

    void Update()
    {
        UpdatePayButtonVisibility();
    }

    private void UpdatePayButtonVisibility()
    {
        if (cost > 0)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }

    #region PUBLIC FUNCTIONS

    /// <summary>
    /// Tell if the ActiveCard container is empty or not.
    /// If there is no active card, it means that the ActiveCard is empty.
    /// </summary>
    /// <returns>True is there is a card; False if not.</returns>
    public bool IsEmpty()
    {
        return card == null;
    }

    /// <summary>
    /// Called when the object must be initialized.
    /// Call the function StateFinish when finished.
    /// </summary>
    public void Init()
    {
        card = null;
        display.Hide();
        combatManager.StateFinish(this.gameObject, Combat_State.Init);
    }

    /// <summary>
    /// Called when the object must prepare itself for the begin of the turn.
    /// Call the function StateFinish when finished.
    /// </summary>
    public void BeginTurn()
    {

        combatManager.StateFinish(this.gameObject, Combat_State.Begin_Turn);
    }

    /// <summary>
    /// Gives a new card to the ActiveCard. If there was another one in place (which should not occur), it is discarded.
    /// </summary>
    /// <param name="new_card">The new card.</param>
    public void NewCard(Card new_card)
    {
        if (card != null)
        {
            Debug.Log("The active card has been override. The old card has been discarded.");
            DiscardCard();
        }
        card = new_card;
        GameObject card_object = card.gameObject;
        card_object.transform.SetParent(display.canvas.transform);
        card_object.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        display.Display();
        cost = card.getCostValue();
        health = card.getHealthValue();
    }

    /// <summary>
    /// Damage taken by the card. If value is negative, it becomes an heal.
    /// </summary>
    /// <param name="value">The amount of damage taken by the card.</param>
    public void TakeDamage(int value)
    {
        card.SetHealthValue(card.getHealthValue() - value);
    }

    /// <summary>
    /// The current card is discarded and returns to the discard pile of the player.
    /// </summary>
    public void DiscardCard()
    {
        player.DiscardCard(card);
        card = null;
        display.Hide();
    }

    /// <summary>
    /// The current card is destroyed and doesn't return to the discard pile of the player.
    /// </summary>
    public void DestroyCard()
    {
        Destroy(card.gameObject, 1);
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        card.GetComponent<Animator>().SetTrigger("Dead");
        card = null;
        yield return new WaitForSeconds(1);
        display.Hide();
    }

    /// <summary>
    /// Returns the remaining cost to pay to play the card.
    /// </summary>
    /// <returns>The remaining cost. If the returned value is 0 or under, the cost has been payed. </returns>
    public int RemainingCost()
    {
        return cost;
    }

    /// <summary>
    /// Pay a certain amount to substract to the cost.
    /// </summary>
    /// <param name="value">The amount to substract.</param>
    public void Pay(int value)
    {
        cost -= value;
    }

    /// <summary>
    /// Called when the object must prepare itself for the end of the turn.
    /// Call the function StateFinish when finished.
    /// </summary>
    public void EndTurn()
    {
        // XP the card
        // card.AddXP(1);

        if (card.getHealthValue() > 0)
        {
            card.AddXp(1);
        }
        else
        {
            DestroyCard();
        }


        combatManager.StateFinish(this.gameObject, Combat_State.End_Turn);
    }

    /// <summary>
    /// Called when the object must prepare itself for the end of the combat.
    /// Call the function StateFinish when finished.
    /// </summary>
    public void EndCombat()
    {
        if (card != null)
        {
            DiscardCard();
        }
        combatManager.StateFinish(this.gameObject, Combat_State.End_Combat);
    }

    #endregion
}
