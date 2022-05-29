using System;
using UnityEngine;

public class ITEM_DATA : ScriptableObject
{
    [Header("Description")]
    public string itemName;
    public string itemID;
    public string itemDescription;
    public Quality quality;
    public int buyPrice;
    public int sellPrice;

    [Header("Effect")] 
    public ItemStats[] itemStats;

    public enum Quality
    {
        JUNK, 
        COMMON, 
        UNCOMMON, 
        RARE, 
        EPIC, 
        LEGENDARY
    }

    [Serializable] public struct ItemStats
    {
        public Stats stats;
        public int value;
        public enum Stats
        {
            ENDURANCE,
            ARMOR,
            STRENGTH,
            AGILITY,
            INTELLECT,
            HASTE,
            MASTERY,
            SPIRIT,
            VERSATILITY,
            CRITICAL_STRIKE
        }
    }
}
