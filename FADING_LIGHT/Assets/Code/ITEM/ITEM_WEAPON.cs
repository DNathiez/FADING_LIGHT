using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class ITEM_WEAPON : ITEM_DATA
{
    [Header("Weapon Data")]
    public int minAttackDamage;
    public int maxAttackDamage;
    public float attackSpeed; //less is faster
    public HandType handType;
    public WeaponType weaponType;
    
    public enum HandType
    {
        ONE_HAND,
        TWO_HAND,
        LEFT_HAND,
        RIGHT_HAND,
        DISTANCE
    }

    public enum WeaponType
    {
        AXE,
        SWORD,
        HAMMER,
        STAFF,
        SHIELD,
        BOW,
        WAND
    }
}
