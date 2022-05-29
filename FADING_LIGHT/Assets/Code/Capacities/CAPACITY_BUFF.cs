using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capacity/Buff")]
public class CAPACITY_BUFF : CAPACITY
{
    public BuffTarget buffTarget;
    public enum BuffTarget{SELF, TARGET, GROUP}

    public bool overtimeEffect;
    public float effectTime;

    public BuffedData buffedData;
    public BuffedData[] newtest; //switch buffedData into array to apply on all

    public enum BuffedData
    {
        HEALTH, 
        SPEED,
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

    public override void UseCapacity(CHARACTER_CONTROLLER player, Transform target)
    {
        float distanceToTarget = Vector3.Distance(player.transform.position, target.position);
        
        switch (buffTarget)
        {
            case BuffTarget.SELF:
                //appliquer le buff sur soi
                target = player.transform;
                
                switch (buffedData)
                {
                    case BuffedData.HEALTH:
                        player.currentHealth += Mathf.FloorToInt(baseValue);
                        Debug.Log("player got a health boost of " + baseValue);
                        if (particleEffect != null)
                        {
                            var vfx = Instantiate(particleEffect, new Vector3(player.transform.position.x, particleEffect.gameObject.transform.position.y, player.transform.position.z), particleEffect.gameObject.transform.rotation);
                            vfx.transform.SetParent(target);
                        }
                        break;
                    
                    case BuffedData.SPEED:
                        player.currentSpeed += baseValue;

                        if (overtimeEffect)
                        {
                            
                        }
                        
                        break;
                    
                }
                
                break;
            
            case BuffTarget.TARGET:
                //si la cible est allié, alors appliqué l'effet du buff
                switch (buffedData)
                {
                    case BuffedData.HEALTH :
                        
                        if (target != null && target.gameObject.GetComponent<PNJ>())
                        {
                            PNJ allyTarget = target.gameObject.GetComponent<PNJ>();
                    
                            if (distanceToTarget < maxRange)
                            {
                                allyTarget.currentHealth += Mathf.FloorToInt(baseValue);
                                Debug.Log(target.name + " got heal by " + baseValue);
                            }
                        }
                        break;
                    
                    
                }
                
                break;
            
            case BuffTarget.GROUP:
                //appliquer l'effet à toutes les cibles dans le groupe
                
                break;
        }
    }
}
