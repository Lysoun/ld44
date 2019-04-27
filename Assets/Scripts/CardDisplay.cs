using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public CardData card;

    public TextMeshProUGUI typeText;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI armorText;

    public TextMeshProUGUI costText;

    public RawImage artwork;

    // Initialize the content of the card prefab.
    void Start()
    {
        System.Random rnd = new System.Random();

        typeText.SetText(card.type);

        healthText.SetText((card.health).ToString());
        atkText.SetText((rnd.Next(card.atkMin, card.atkMax)).ToString());
        armorText.SetText((rnd.Next(card.armorMin, card.armorMax)).ToString());

        costText.SetText((card.cost).ToString());

        artwork.texture = card.artwork;
    }

}
