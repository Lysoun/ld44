using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCard : MonoBehaviour
{
    public CombatManager combatManager;
    public PlayerController player;
     

    private Card card;
    private int health;
    private int cost;
    private ActiveCardDisplay display;

    void Start()
    {
        display = GetComponent<ActiveCardDisplay>();
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
        cost -= value;
    }

    /// <summary>
    /// The current card is discarded and returns to the discard pile of the player.
    /// </summary>
    public void DiscardCard()
    {
        player.AddToDiscard(card);
        card = null;
        display.Hide();
    }

    /// <summary>
    /// The current card is destroyed and doesn't return to the discard pile of the player.
    /// </summary>
    public void DestroyCard()
    {
        Destroy(card.gameObject);
        card = null;
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
