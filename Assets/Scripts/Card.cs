using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    /// <summary>
    /// Card datas.
    /// </summary>
    public CardData card;

    /// <summary>
    /// TextMeshPro of the type of the card prefab.
    /// </summary>
    public TextMeshProUGUI typeText;

    /// <summary>
    /// TextMeshPro of the health of the card prefab.
    /// </summary>
    public TextMeshProUGUI healthText;

    /// <summary>
    /// TextMeshPro of the attack of the card prefab.
    /// </summary>
    public TextMeshProUGUI atkText;

    /// <summary>
    /// TextMeshPro of the armor of the card prefab.
    /// </summary>
    public TextMeshProUGUI armorText;

    /// <summary>
    /// TextMeshPro of the speed of the card prefab.
    /// </summary>
    public TextMeshProUGUI speedText;

    /// <summary>
    /// TextMeshPro of the cost of the card prefab.
    /// </summary>
    public TextMeshProUGUI costText;

    /// <summary>
    /// RawImage of the artwork of the card prefab.
    /// </summary>
    public RawImage artwork;

    public GameObject graphicsChildren;
    ///////////////////////////////////////////////////////////////////
    /// Values of the different stats.
    /// 

    /// <summary>
    /// Type of the card.
    /// </summary>
    private string typeValue;

    /// <summary>
    /// Health of the card.
    /// </summary>
    private int healthValue;

    /// <summary>
    /// Attack of the card.
    /// </summary>
    private int atkValue;

    /// <summary>
    /// Armor of the card.
    /// </summary>
    private int armorValue;

    /// <summary>
    /// Speed of the card.
    /// </summary>
    private int speedValue;

    /// <summary>
    /// Cost of the card.
    /// </summary>
    private int costValue;

    /// <summary>
    /// Artwork of the card.
    /// </summary>
    private Texture artworkImage;
    
    
    // Initialize the content of the card prefab.
    void Start()
    {
        System.Random rnd = new System.Random();
        typeValue = card.type;
        healthValue = rnd.Next(card.healthMin, card.healthMax + 1);
        atkValue = rnd.Next(card.atkMin, card.atkMax + 1);
        armorValue = rnd.Next(card.armorMin, card.armorMax + 1);
        speedValue = rnd.Next(card.speedMin, card.speedMax + 1);
        costValue = card.cost;
        artworkImage = card.artwork;

        typeText.SetText(typeValue);

        healthText.SetText(healthValue.ToString());
        atkText.SetText(atkValue.ToString());
        armorText.SetText(armorValue.ToString());
        speedText.SetText(speedValue.ToString());

        costText.SetText(costValue.ToString());

        artwork.texture = artworkImage;
    }

    public void Hide()
    {
        graphicsChildren.SetActive(false);
    }

    public void Display()
    {
        graphicsChildren.SetActive(true);
    }


    ///////////////////////////////////////////////////////////////////
    /// Getter of all the statistics.
    /// 

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    public string getTypeValue()
    {
        return typeValue;
    }

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    public int getHealthValue()
    {
        return healthValue;
    }

    /// <summary>
    /// Return the attack value of the card.
    /// </summary>
    public int getAttackValue()
    {
        return atkValue;
    }

    /// <summary>
    /// Return the armor value of the card.
    /// </summary>
    public int getArmorValue()
    {
        return armorValue;
    }

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    public int getSpeedValue()
    {
        return speedValue;
    }

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    public int getCostValue()
    {
        return costValue;
    }

    /// <summary>
    /// Return the artwork image of the card.
    /// </summary>
    public Texture getArtworkImage()
    {
        return artworkImage;
    }

    ///////////////////////////////////////////////////////////////////
    /// Setter of the modifiable statistics.
    /// 
     
    /// <summary>
    /// Set the health value at the given value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public void SetHealthValue(int value)
    {
        healthValue = value;
    }
}
