using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MAX_LIFE_POINTS = 10;
    const int STARTING_HAND_SIZE = 5;

    private int lifePoints;
    private int activeCard; // TODO ActiveCardController
    private List<Card> hand;
    Deck deck; // deck has discard pile

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initPlayer()
    {
        deck.initDeck();
        lifePoints = MAX_LIFE_POINTS;
        hand = new List<Card>();
        initHand();
        // TODO coroutine to call GameController.initFinished(this)
    }

    void initHand()
    {
        // TODO
        // draw STARTING_HAND_SIZE cards from deck
        // deck.draw(); xSTARTING_HAND_SIZE
    }

    void beginTurn()
    {
        deck.draw();
    }

    void play()
    {
        // TODO find a card clicked on in hand
    }

    bool keepActiveCard()
    {
        // TODO
        return false;
    }

    // TODO return value
    void endTurn()
    {

    }

    // Mofify player's life points by the lp value, which can be negative or positive
    void changeLifePoints(int lp)
    {
        lifePoints += lp;
    }

    int getLifePoints()
    {
        return lifePoints;
    }

    // TODO Sacrifices
}
