using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const int MAX_LIFE_POINTS = 10;
    const int STARTING_HAND_SIZE = 5;

    public List<GameObject> Card_Prefab_At_Beginning;
    public List<int> multiplicity;

    public GameObject canvas_hand;
    public GameObject card_container;

    private int lifePoints;
    private Card activeCard; // TODO ActiveCardController
    private List<Card> hand;
    Deck deck; // deck has discard pile

    public CombatManager combatManager;
    public PlayerCardPicker picker;
    public Slider healthBar;

    private bool isPlaying;
    private bool isSacrificing;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = MAX_LIFE_POINTS;
        healthBar.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PositionHand();
        healthBar.value = lifePoints;
    }

    private void PositionHand()
    {
        foreach (Card card in hand)
        {
            card.transform.SetParent(canvas_hand.transform);
        }
        
        for (int i = 0; i < hand.Count; i++)
        {
            Card card = hand[i];
            RectTransform card_transform = card.GetComponent<RectTransform>();

            RectTransform canvas_transform = canvas_hand.GetComponent<RectTransform>();

            float x_pos = -canvas_transform.rect.width / 2 + (i + 1) * canvas_transform.rect.width / (hand.Count + 1);

            card_transform.anchoredPosition = new Vector2(x_pos, 0f);

            /*RectTransform canvas_rect = canvas_hand.GetComponent<RectTransform>();
            Debug.Log(canvas_rect.position);
            Debug.Log(canvas_rect.rect.width);
            hand[i].GetComponent<RectTransform>().position = canvas_rect.position + i * canvas_rect.rect.width / hand.Count * new Vector3(1, 0, 0);
            hand[i].transform.position = canvas_rect.position + canvas_rect.rect.width * new Vector3(1, 0, 0);*/
        }
    }

    #region PUBLIC FUNCTIONS
    public void BeginTurn()
    {
        Card drawn_card = deck.Draw();
        hand.Add(drawn_card);
        combatManager.StateFinish(this.gameObject, Combat_State.Begin_Turn);
    }

    // TODO return value
    public void EndCombat()
    {
        combatManager.StateFinish(this.gameObject, Combat_State.End_Combat);
    }

    public void EndTurn()
    {
        // discard active card
        //deck.AddToDiscard(activeCard);
        //activeCard = null;
        combatManager.StateFinish(this.gameObject, Combat_State.End_Turn);
    }

    public int GetLifePoints()
    {
        return lifePoints;
    }

    public void DiscardCard(Card card)
    {
        deck.AddToDiscard(card);
        // combatManager.StateFinish(this.gameObject, Combat_State.)
    }

    public void Init()
    {
        // Init deck
        deck = new Deck();

        // Populate deck
        deck.Populate(Card_Prefab_At_Beginning, multiplicity, card_container);

        // Init life points
        lifePoints = MAX_LIFE_POINTS;

        // Init hand
        hand = new List<Card>();
        InitHand();

        // Telling the combat manager that I'm done with init
        combatManager.StateFinish(this.gameObject, Combat_State.Init);
    }




    private void InitHand()
    {
        // Draw STARTING_HAND_SIZE cards from deck
        for (int i = 0; i < STARTING_HAND_SIZE; i++)
            hand.Add(deck.Draw());
    }

    public void Play()
    {
        picker.enabled = true;
        isPlaying = true;
        // TODO find a card clicked on in hand
        // return played card
        // combatManager.Choosen_Card(selectedCard);
        // combatManager.StateFinish(this.gameObject, Combat_State.Player_Choose);
    }

    public void Sacrifice()
    {
        picker.enabled = true;
        isSacrificing = true;
        // TODO return sacrificed
        // pick a card and give it to sacrifice
        //Card card = pickedCard();
        //Card card = null;
        //combatManager.Sacrificed_Card(card.getCostValue());
        //hand.Remove(card);
        //Destroy(card.gameObject);
    }

    // Mofify player's life points by the lp value, which can be negative or positive
    public void TakeDamage(int lp)
    {
        lifePoints -= lp;
    }

    public void PlayedCard(Card card)
    {
        bool res = hand.Remove(card);
        Debug.Log(res);
    }

    public void PickedCard(GameObject card)
    {
        picker.enabled = false;

        if (isPlaying)
        {
            isPlaying = false;
            combatManager.Choosen_Card(card.GetComponent<Card>());
            // hand.Remove(card.GetComponent<Card>());
            combatManager.StateFinish(this.gameObject, Combat_State.Player_Choose);
        }
        else if (isSacrificing)
        {
            isSacrificing = false;
            combatManager.Sacrificed_Card(card.GetComponent<Card>().getCostValue());
            hand.Remove(card.GetComponent<Card>());
            Destroy(card);
            // deck.destroy(card);
            combatManager.StateFinish(this.gameObject, Combat_State.Sacrifising);
        }
        else
        {
            Debug.Log("WTF ?!");
        }
        
    }
    
    
    #endregion
}
