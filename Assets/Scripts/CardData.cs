using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Card_Class { Tank, Healer, Wizard, Ranger };

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardData : ScriptableObject
{
    
    public Card_Class type;

    public int healthMin;
    public int healthMax;
    public int atkMin;
    public int atkMax;
    public int armorMin;
    public int armorMax;
    public int speedMin;
    public int speedMax;
    private int ageMin = 2;
    private int ageMax = 3;

    public int cost;

    public int xpMax;
    private int xp = 0;

    
    /// <summary>
    /// Return the amount of xp of the card.
    /// </summary>
    /// <returns></returns>
    public int getXp()
    {
        return xp;
    }

    public int getAgeMin()
    {
        return ageMin;
    }

    public int getAgeMax()
    {
        return ageMax;
    }
}
