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
    public TextMeshProUGUI speedText;

    public TextMeshProUGUI costText;

    public RawImage artwork;

    // Initialize the content of the card prefab.
    void Start()
    {
        System.Random rnd = new System.Random();
        int healthValue = rnd.Next(card.healthMin, card.healthMax + 1);
        int atkValue = rnd.Next(card.atkMin, card.atkMax + 1);
        int armorValue = rnd.Next(card.armorMin, card.armorMax + 1);
        int speedValue = rnd.Next(card.speedMin, card.speedMax + 1);
        int costValue = card.cost;

        typeText.SetText(card.type);

        healthText.SetText(healthValue.ToString());
        atkText.SetText(atkValue.ToString());
        armorText.SetText(armorValue.ToString());
        speedText.SetText(speedValue.ToString());

        costText.SetText(costValue.ToString());

        artwork.texture = card.artwork;
    }

}
