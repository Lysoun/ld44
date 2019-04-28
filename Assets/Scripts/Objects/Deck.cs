using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> cards;
    List<Card> discardPile;

    public Deck()
    {
        Init();
    }

    // TODO deck as parameter given by GameController
    public void Init()
    {
        cards = new List<Card>(); // TODO put cards inside deck
        discardPile = new List<Card>();
    }

    public void Populate(List<GameObject> Card_Prefab_At_Beginning, List<int> multiplicity, GameObject parent)
    {
        for (int i = 0; i < multiplicity.Count; i++)
        {
            for (int j = 0; j < multiplicity[i]; j++)
            {
                GameObject new_card = GameObject.Instantiate(Card_Prefab_At_Beginning[i], parent.transform);
                new_card.GetComponent<Card>().RandomizeValues();
                AddToDeck(new_card.GetComponent<Card>());
            }
        }

        
    }

    public Card Draw()
    {
        if (cards.Count < 1)
        {
            if (discardPile.Count < 1)
            {
                // TODO you lose because you have no cards
                Debug.Log("Plus de carte, pouet");
                return null;
            }
            else
            {
                int n = discardPile.Count;
                for (int i = 0; i < n; i++)
                {
                    cards.Add(discardPile[0]);
                    discardPile.RemoveAt(0);
                }
                discardPile.Clear();
            }
        }

        
        int index = Random.Range((int)0, cards.Count);
        Debug.Log("Index = " + index.ToString());
        Card drawnCard = cards[index];
        drawnCard.Display();
        cards.Remove(drawnCard);
        return drawnCard;
    }

    public void AddToDiscard(Card card)
    {
        card.Hide();
        discardPile.Add(card);
    }

    public void AddToDeck(Card card)
    {
        card.Hide();
        cards.Add(card);
    }
}
