using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Card : MonoBehaviour
{
    enum Class { Tank, Healer, Mage, Ranger };

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
    /// TextMeshPro of the age of the card.
    /// </summary>
    public TextMeshProUGUI ageText;

    /// <summary>
    /// TextMeshPro of the cost of the card prefab.
    /// </summary>
    public TextMeshProUGUI costText;

    /// <summary>
    /// RawImage of the artwork of the card prefab.
    /// </summary>
    public RawImage artwork;

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
    /// Age of the card.
    /// </summary>
    private int ageValue;

    /// <summary>
    /// Cost of the card.
    /// </summary>
    private int costValue;

    /// <summary>
    /// Artwork of the card.
    /// </summary>
    private Texture artworkImage;

    /// <summary>
    /// Amount of xp needded to up.
    /// </summary>
    private int xpToUpValue;

    /// <summary>
    /// Xp of the card.
    /// </summary>
    private int xpValue;

    ///////////////////////////////////////////////////////////////////
    /// Fixed level up variables.
    /// 

    private int tankHealth2 = 3;
    private int tankHealth3 = -1;
    private int healerHealth2 = 2;
    private int healerHealth3 = 0;
    private int mageHealth2 = 1;
    private int mageHealth3 = -3;
    private int rangerHealth2 = 1;
    private int rangerHealth3 = -2;

    private int tankAtk2 = 2;
    private int tankAtk3 = -1;
    private int healerAtk2 = 0;
    private int healerAtk3 = -2;
    private int mageAtk2 = 3;
    private int mageAtk3 = 2;
    private int rangerAtk2 = 4;
    private int rangerAtk3 = 1;

    private int tankSpeed2 = 0;
    private int tankSpeed3 = -1;
    private int healerSpeed2 = 1;
    private int healerSpeed3 = 0;
    private int mageSpeed2 = 1;
    private int mageSpeed3 = -2;
    private int rangerSpeed2 = 3;
    private int rangerSpeed3 = 0;

    private int tankArmor2 = 3;
    private int tankArmor3 = 3;
    private int healerArmor2 = 1;
    private int healerArmor3 = 0;
    private int mageArmor2 = 0;
    private int mageArmor3 = 0;
    private int rangerArmor2 = 1;
    private int rangerArmor3 = 0;


    // Initialize the content of the card prefab.
    void Start()
    {
        System.Random rnd = new System.Random();
        typeValue = card.type;
        healthValue = rnd.Next(card.healthMin, card.healthMax + 1);
        atkValue = rnd.Next(card.atkMin, card.atkMax + 1);
        armorValue = rnd.Next(card.armorMin, card.armorMax + 1);
        speedValue = rnd.Next(card.speedMin, card.speedMax + 1);
        ageValue = rnd.Next(card.ageMin, card.ageMax + 1);
        costValue = card.cost;
        artworkImage = card.artwork;
        xpToUpValue = card.xpMax;
        xpValue = card.getXp();

        typeText.SetText(typeValue);

        healthText.SetText(healthValue.ToString());
        atkText.SetText(atkValue.ToString());
        armorText.SetText(armorValue.ToString());
        speedText.SetText(speedValue.ToString());
        SetAgeText(ageValue);

        costText.SetText(costValue.ToString());

        artwork.texture = artworkImage;
    }

    ///////////////////////////////////////////////////////////////////
    /// Getter of all the statistics.
    /// 

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    /// <returns></returns>
    public string getTypeValue()
    {
        return typeValue;
    }

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    /// <returns></returns>
    public int getHealthValue()
    {
        return healthValue;
    }

    /// <summary>
    /// Return the attack value of the card.
    /// </summary>
    /// <returns></returns>
    public int getAttackValue()
    {
        return atkValue;
    }

    /// <summary>
    /// Return the armor value of the card.
    /// </summary>
    /// <returns></returns>
    public int getArmorValue()
    {
        return armorValue;
    }

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    /// <returns></returns>
    public int getSpeedValue()
    {
        return speedValue;
    }

    /// <summary>
    /// Return the age of the card.
    /// </summary>
    /// <returns></returns>
    public int getAgeValue()
    {
        return ageValue;
    }

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    /// <returns></returns>
    public int getCostValue()
    {
        return costValue;
    }

    /// <summary>
    /// Return the artwork image of the card.
    /// </summary>
    /// <returns></returns>
    public Texture getArtworkImage()
    {
        return artworkImage;
    }

    /// <summary>
    /// Return the current xp of the card.
    /// </summary>
    /// <returns></returns>
    public int getXpValue()
    {
        return xpValue;
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

    /// <summary>
    /// Setter of the TextMeshPro of the age.
    /// Set the age with roman int.
    /// </summary>
    /// <param name="value"></param>
    public void SetAgeText(int value)
    {
        switch (value)
        {
            case 1:
                ageText.SetText("I");
                break;
            case 2:
                ageText.SetText("II");
                break;
            case 3:
                ageText.SetText("III");
                break;
            default:
                ageText.SetText("IV");
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////
    /// Public function for the update of the xp and the age.
    /// 
    
    /// <summary>
    /// Add an amount of xp to the current xp of the card
    /// and update the age if neddeed.
    /// Does nothing if the age is = 4.
    /// </summary>
    /// <param name="value"></param>
    public void AddXp(int value)
    {
        if (ageValue < 4)
        {
            if ((xpValue + value) >= xpToUpValue)
            {
                ageValue++;
                SetAgeText(ageValue);
            }

            if (ageValue < 4)
            {
                xpValue = (xpValue + value) % xpToUpValue;
            }
            else
            {
                xpValue = 0;
            }

            UpdateStats();
        }
    }

    /// <summary>
    /// Update the statistics of the card during a level up.
    /// </summary>
    public void UpdateStats()
    {
       if (ageValue < 4)
       {
            Class cardClass = (Class)System.Enum.Parse(typeof(Class), typeValue);
            switch (cardClass)
            {
                case Class.Tank:
                    if (ageValue == 2)
                    {
                        healthValue += tankHealth2;
                        atkValue += tankAtk2;
                        armorValue += tankArmor2;
                        speedValue += tankSpeed2;
                    }
                    else
                    {
                        healthValue += tankHealth3;
                        atkValue += tankAtk3;
                        armorValue += tankArmor3;
                        speedValue += tankSpeed3;
                    }
                    break;
                case Class.Healer:
                    if (ageValue == 2)
                    {
                        healthValue += healerHealth2;
                        atkValue += healerAtk2;
                        armorValue += healerArmor2;
                        speedValue += healerSpeed2;
                    }
                    else
                    {
                        healthValue += healerHealth3;
                        atkValue += healerAtk3;
                        armorValue += healerArmor3;
                        speedValue += healerSpeed3;
                    }
                    break;
                case Class.Mage:
                    if (ageValue == 2)
                    {
                        healthValue += mageHealth2;
                        atkValue += mageAtk2;
                        armorValue += mageArmor2;
                        speedValue += mageSpeed2;
                    }
                    else
                    {
                        healthValue += mageHealth3;
                        atkValue += mageAtk3;
                        armorValue += mageArmor3;
                        speedValue += mageSpeed3;
                    }
                    break;
                default:
                    if (ageValue == 2)
                    {
                        healthValue += rangerHealth2;
                        atkValue += rangerAtk2;
                        armorValue += rangerArmor2;
                        speedValue += rangerSpeed2;
                    }
                    else
                    {
                        healthValue += rangerHealth3;
                        atkValue += rangerAtk3;
                        armorValue += rangerArmor3;
                        speedValue += rangerSpeed3;
                    }
                    break;
            }
       }
       else
       {
            healthValue = 0;
            atkValue = 0;
            armorValue = 0;
            speedValue = 0;
            costValue = 0;
       }

    }

}
