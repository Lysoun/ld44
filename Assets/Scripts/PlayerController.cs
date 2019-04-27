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

    public void BeginTurn()
    {
        deck.Draw();
        // TODO tell GameController when done
    }

    // TODO return value
    public void EndCombat()
    {

    }

    // TODO return value
    public void EndTurn()
    {

    }

    public int GetLifePoints()
    {
        return lifePoints;
    }

    public bool GoToDiscard()
    {
        // TODO
        return false;
    }

    public void Init()
    {
        deck.Init();
        lifePoints = MAX_LIFE_POINTS;
        hand = new List<Card>();
        InitHand();
        // TODO coroutine to call GameController.initFinished(this)
    }

    void InitHand()
    {
        // TODO
        // draw STARTING_HAND_SIZE cards from deck
        // deck.draw(); xSTARTING_HAND_SIZE
    }

    public void Play()
    {
        // TODO find a card clicked on in hand
        // return played card
    }

    public void Sacrifice()
    {
        // TODO return sacrificed
    }

    // Mofify player's life points by the lp value, which can be negative or positive
    public void TakeDamage(int lp)
    {
        lifePoints += lp;
    }
}
