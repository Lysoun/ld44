using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

//public enum Card_Class { Tank, Healer, Wizard, Ranger };

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

    /// <summary>
    /// Image reference for hourglass emptying part
    /// </summary>
    public Image time_up;

    /// <summary>
    /// Image reference for hourglass filling part
    /// </summary>
    public Image time_down;

    public GameObject graphicsChildren;
    ///////////////////////////////////////////////////////////////////
    /// Values of the different stats.
    /// 

    /// <summary>
    /// Type of the card.
    /// </summary>
    private Card_Class typeValue;

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

    private bool block = false;

    // Initialize the content of the card prefab.
    void Start()
    {
        typeValue = card.type;
        
        ageValue = Random.Range(card.getAgeMin(), card.getAgeMax() + 1);

        healthValue = Random.Range(card.healthMin, card.healthMax + 1);
        atkValue = Random.Range(card.atkMin, card.atkMax + 1);
        armorValue = Random.Range(card.armorMin, card.armorMax + 1);
        speedValue = Random.Range(card.speedMin, card.speedMax + 1);
   
        costValue = card.cost;
        
        xpToUpValue = card.xpMax;
        xpValue = card.getXp();

        typeText.SetText(Card_ClassToString(typeValue));

        healthText.SetText(healthValue.ToString());
        atkText.SetText(atkValue.ToString());
        armorText.SetText(armorValue.ToString());
        speedText.SetText(speedValue.ToString());
        SetAgeText(ageValue);

        costText.SetText(costValue.ToString());
        
        switch (typeValue)
        {
            case Card_Class.Tank:
                switch (ageValue)
                {
                    case 1:
                        artworkImage = Resources.Load("Characters/WarriorYoung") as Texture;
                        break;
                    case 2:
                        artworkImage = Resources.Load("Characters/WarriorAdult") as Texture;
                        break;
                    case 3:
                        artworkImage = Resources.Load("Characters/WarriorOld") as Texture;
                        break;
                    default:
                        Debug.Log("T'es mauvais Jack !!!");
                        break;
                }
                break;
            case Card_Class.Healer:
                switch (ageValue)
                {
                    case 1:
                        artworkImage = Resources.Load("Characters/HealerYoung") as Texture;
                        break;
                    case 2:
                        artworkImage = Resources.Load("Characters/HealerAdult") as Texture;
                        break;
                    case 3:
                        artworkImage = Resources.Load("Characters/HealerOld") as Texture;
                        break;
                    default:
                        Debug.Log("T'es mauvais Jack !!!");
                        break;
                }
                break;
            case Card_Class.Wizard:
                switch (ageValue)
                {
                    case 1:
                        artworkImage = Resources.Load("Characters/WizardYoung") as Texture;
                        break;
                    case 2:
                        artworkImage = Resources.Load("Characters/WizardAdult") as Texture;
                        break;
                    case 3:
                        artworkImage = Resources.Load("Characters/WizardOld") as Texture;
                        break;
                    default:
                        Debug.Log("T'es mauvais Jack !!!");
                        break;
                }
                break;
            case Card_Class.Ranger:
                switch (ageValue)
                {
                    case 1:
                        artworkImage = Resources.Load("Characters/RangerYoung") as Texture;
                        break;
                    case 2:
                        artworkImage = Resources.Load("Characters/RangerAdult") as Texture;
                        break;
                    case 3:
                        artworkImage = Resources.Load("Characters/RangerOld") as Texture;
                        break;
                    default:
                        Debug.Log("T'es mauvais Jack !!!");
                        break;
                }
                break;
            default:
                Debug.Log("T'es mauvais Jack !!!");
                break;
        }
        
        artwork.texture = artworkImage;
    }


    //Update the texts of the card
    public void Update()
    {
        if (healthValue <= 0)
        {
            healthValue = 0;
            atkValue = 0;
            armorValue = 0;
            speedValue = 0;
            artworkImage = Resources.Load("Characters/Cadaver") as Texture;
            artwork.texture = artworkImage;
        }

        if (atkValue < 0)
        {
            atkValue = 0;
        }

        if ((armorValue < 0) && (!block))
        {
            armorValue = 0;
        }

        if (speedValue < 0)
        {
            speedValue = 0;
        }

        atkText.SetText(atkValue.ToString());
        if (!block)
        {
            armorText.SetText(armorValue.ToString());
        }
        else
        {
            armorText.SetText("∞");
        }
        
        speedText.SetText(speedValue.ToString());
        healthText.SetText(healthValue.ToString());
        time_down.fillAmount = (Time.time%5)/5;
        time_up.fillAmount = 1 - (Time.time % 5) / 5;

    }

    public void Hide()
    {
        graphicsChildren.SetActive(false);
    }

    public void Display()
    {
        graphicsChildren.SetActive(true);
    }

    public void RandomizeValues()
    {
        typeValue = card.type;
        Debug.Log("Min = " + card.healthMin.ToString() + " / Max = " + card.healthMax.ToString());
        healthValue = Random.Range(card.healthMin, card.healthMax + 1);
        atkValue = Random.Range(card.atkMin, card.atkMax + 1);
        armorValue = Random.Range(card.armorMin, card.armorMax + 1);
        speedValue = Random.Range(card.speedMin, card.speedMax + 1);
        costValue = card.cost;
        //artworkImage = card.artwork;  card.artwork n'existe plus.

        typeText.SetText(Card_ClassToString(typeValue));

        healthText.SetText(healthValue.ToString());
        atkText.SetText(atkValue.ToString());
        armorText.SetText(armorValue.ToString());
        speedText.SetText(speedValue.ToString());

        costText.SetText(costValue.ToString());

        //artwork.texture = artworkImage;
    }

    public void Copy_Values(Card other_card)
    {
        this.healthValue = other_card.healthValue;
        this.atkValue = other_card.atkValue;
        this.armorValue = other_card.armorValue;
        this.speedValue = other_card.speedValue;
        this.costValue = other_card.costValue;
        this.typeValue = other_card.typeValue;

        typeText.SetText(Card_ClassToString(typeValue));

        healthText.SetText(healthValue.ToString());
        atkText.SetText(atkValue.ToString());
        armorText.SetText(armorValue.ToString());
        speedText.SetText(speedValue.ToString());

        costText.SetText(costValue.ToString());
    }

    ///////////////////////////////////////////////////////////////////
    /// Getter of all the statistics.
    /// 

    /// <summary>
    /// Return the type value of the card.
    /// </summary>
    /// <returns></returns>
    public Card_Class getTypeValue()
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

    /// <summary>
    /// True if its a cadaver that can block.
    /// Else false.
    /// </summary>
    /// <returns></returns>
    public bool canBlock()
    {
        return block;
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
                UpdateStats();
            }

            if (ageValue < 4)
            {
                xpValue = (xpValue + value) % xpToUpValue;
            }
            else
            {
                xpValue = 0;
            }

            
        }
    }

    /// <summary>
    /// Update the statistics of the card during a level up.
    /// </summary>
    public void UpdateStats()
    {
       if (ageValue < 4)
       {
            switch (typeValue)
            {
                case Card_Class.Tank:
                    if (ageValue == 2)
                    {
                        healthValue += tankHealth2;
                        atkValue += tankAtk2;
                        armorValue += tankArmor2;
                        speedValue += tankSpeed2;
                        artworkImage = Resources.Load("Characters/WarriorAdult") as Texture;
                    }
                    else
                    {
                        healthValue += tankHealth3;
                        atkValue += tankAtk3;
                        armorValue += tankArmor3;
                        speedValue += tankSpeed3;
                        artworkImage = Resources.Load("Characters/WarriorOld") as Texture;
                    }
                    break;
                case Card_Class.Healer:
                    if (ageValue == 2)
                    {
                        healthValue += healerHealth2;
                        atkValue += healerAtk2;
                        armorValue += healerArmor2;
                        speedValue += healerSpeed2;
                        artworkImage = Resources.Load("Characters/HealerAdult") as Texture;
                    }
                    else
                    {
                        healthValue += healerHealth3;
                        atkValue += healerAtk3;
                        armorValue += healerArmor3;
                        speedValue += healerSpeed3;
                        artworkImage = Resources.Load("Characters/HealerOld") as Texture;
                    }
                    break;
                case Card_Class.Wizard:
                    if (ageValue == 2)
                    {
                        healthValue += mageHealth2;
                        atkValue += mageAtk2;
                        armorValue += mageArmor2;
                        speedValue += mageSpeed2;
                        artworkImage = Resources.Load("Characters/WizardAdult") as Texture;
                    }
                    else
                    {
                        healthValue += mageHealth3;
                        atkValue += mageAtk3;
                        armorValue += mageArmor3;
                        speedValue += mageSpeed3;
                        artworkImage = Resources.Load("Characters/WizardOld") as Texture;
                    }
                    break;
                case Card_Class.Ranger:
                    if (ageValue == 2)
                    {
                        healthValue += rangerHealth2;
                        atkValue += rangerAtk2;
                        armorValue += rangerArmor2;
                        speedValue += rangerSpeed2;
                        artworkImage = Resources.Load("Characters/RangerAdult") as Texture;
                    }
                    else
                    {
                        healthValue += rangerHealth3;
                        atkValue += rangerAtk3;
                        armorValue += rangerArmor3;
                        speedValue += rangerSpeed3;
                        artworkImage = Resources.Load("Characters/RangerOld") as Texture;
                    }
                    break;
                default:
                    Debug.Log("T'es mauvais Jack !!!");
                    break;
            }

            if (healthValue <= 0)
            {
                healthValue = 1;
            }

            if (atkValue <= 0)
            {
                atkValue = 0;
            }

            if (armorValue <= 0)
            {
                armorValue = 0;
            }

            if (speedValue <= 0)
            {
                speedValue = 0;
            }
        }
       else
       {
            healthValue = 1;
            atkValue = 0;
            armorValue = 0;
            speedValue = 0;
            costValue = 0;
            costText.SetText(costValue.ToString());
            block = true;
            artworkImage = Resources.Load("Characters/Cadaver") as Texture;
        }

        artwork.texture = artworkImage;
    }

    /// <summary>
    /// Convert the enum Card_Class into its string form.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string Card_ClassToString(Card_Class type)
    {
        switch (type)
        {
            case Card_Class.Tank:
                return "Tank";
            case Card_Class.Healer:
                return "Healer";
            case Card_Class.Wizard:
                return "Wizard";
            case Card_Class.Ranger:
                return "Ranger";
            default:
                Debug.Log("T'es mauvais Jack !!!");
                return "Ah";
        }
    }
}
