﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MAX_LIFE_POINTS = 10;
    const int STARTING_HAND_SIZE = 5;

    private int lifePoints;
    private Card activeCard; // TODO ActiveCardController
    private List<Card> hand;
    Deck deck; // deck has discard pile

    public CombatManager combatManager;

    public PlayerCardPicker picker;

    private bool isPlaying;
    private bool isSacrificing;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region PUBLIC FUNCTIONS
    public void BeginTurn()
    {
        deck.Draw();
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
        deck.Discard(activeCard);
        activeCard = null;
        combatManager.StateFinish(this.gameObject, Combat_State.End_Turn);
    }

    public int GetLifePoints()
    {
        return lifePoints;
    }

    public void DiscardCard(Card card)
    {
        deck.Discard(card);
        // combatManager.StateFinish(this.gameObject, Combat_State.)
    }

    public void Init()
    {
        // Init deck
        deck.Init();

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
            deck.Draw();
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

        // TODO return sacrificed
        // pick a card and give it to sacrifice
        //Card card = pickedCard();
        Card card = null;
        combatManager.Sacrificed_Card(card.getCostValue());
        hand.Remove(card);
        Destroy(card.gameObject);
    }

    // Mofify player's life points by the lp value, which can be negative or positive
    public void TakeDamage(int lp)
    {
        lifePoints += lp;
    }

    #endregion

    public void PickedCard(GameObject card)
    {
        picker.enabled = false;

        if (isPlaying)
        {
            isPlaying = false;
            combatManager.Choosen_Card(card.GetComponent<Card>());
            combatManager.StateFinish(this.gameObject, Combat_State.Player_Choose);
        }
        else if (isSacrificing)
        {
            isSacrificing = false;
            combatManager.Sacrificed_Card(card.GetComponent<Card>().getCostValue());
            // deck.destroy(card);
            combatManager.StateFinish(this.gameObject, Combat_State.Sacrifising);
        }
        else
        {
            Debug.Log("WTF ?!");
        }
        
    }
}
