using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MAX_LIFE_POINTS = 10;
    const int STARTING_HAND_SIZE = 5;

    private int lifePoints;
    private int activeCard; // TODO Card object
    private int[] hand; // TODO Card type
    private int[] deck; // TODO Deck type
    private int[] discard; // TODO Discard type

    // Start is called before the first frame update
    void Start()
    {
        lifePoints = MAX_LIFE_POINTS;
        hand = new int[STARTING_HAND_SIZE];
        initializeHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void play()
    {
        if (isAlive())
        {
            // TODO play new turn
            playCard(0); // TODO choose card;
        }
        else
        {
            // TODO trigger end of game --> you lose
        }
    }

    // Mofify player's life points by the lp value, which can be negative or positive
    void changeLifePoints(int lp)
    {
        lifePoints += lp;
    }

    void initializeHand()
    {
        // TODO
        // draw STARTING_HAND_SIZE cards from deck
        // deck.draw(STARTING_HAND_SIZE);
    }

    // TODO change prototype to accept a card clicked on in hand
    void playCard(int card)
    {
        if (activeCard != null)
        {
            // TODO cardsInHand.remove(card);
            activeCard = card;
        } else
        {
            // TODO display cannot play card
            // OR 
            // do not allow card selection in hand while there is an active card
        }
    }

    bool isAlive()
    {
        return lifePoints > 0;
    }
}
