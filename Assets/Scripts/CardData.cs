using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardData : ScriptableObject
{
    public string type;

    public int healthMin;
    public int healthMax;
    public int atkMin;
    public int atkMax;
    public int armorMin;
    public int armorMax;
    public int speedMin;
    public int speedMax;
    public int ageMin;
    public int ageMax;

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
}
