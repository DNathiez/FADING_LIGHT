using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/New Class")]
public class CLASS : ScriptableObject
{
    [Header("Description")] 
    public string className;
    public string description;

    [Header("Base Stats")] 
    public int endurance;
    public int armor;
    public int strength;
    public int agility;
    public int intellect;
    public int haste;
    public int mastery;
    public int spirit;
    public int versatility;
    public int criticalStrike;

    [Header("Stats Factor")] 
    [Range(0, 1)] public float apFactor;
    [Range(0, 1)] public float mpFactor;
    [Range(0, 1)] public float physicResistFactor;
    [Range(0, 1)] public float magicResistFactor;
    [Range(0, 1)] public float cooldownReduceFactor;
    
    
    [Header("Limitation")]
    public WeaponUsable[] weaponsUsable;
    
    [Serializable] public struct WeaponUsable
    {
        public ITEM_WEAPON.WeaponType weaponType;
        public ITEM_WEAPON.HandType handType;
    }
}
