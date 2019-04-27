using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> cards;
    List<Card> discardPile;

    // TODO deck as parameter given by GameController
    public void initDeck()
    {
        cards = new List<Card>(); // TODO put cards inside deck
        discardPile = new List<Card>();
    }

    public Card draw()
    {
        if (cards.Count < 1)
        {
            if (discardPile.Count < 1)
            {
                // TODO you lose because you have no cards
            }
            else
            {
                cards = discardPile;
                discardPile.Clear();
            }
        }

        System.Random rand = new System.Random();
        int index = rand.Next(0, cards.Count);
       
        return cards[index];
    }

    public void discard(Card card)
    {
        discardPile.Add(card);
    }
}
