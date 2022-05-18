using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capacity/Buff")]
public class CAPACITY_BUFF : CAPACITY
{
    public BuffTarget buffTarget;
    public enum BuffTarget{Self, Target, Group}

    public bool overtimeEffect;
    public float effectTime;

    public BuffedData buffedData;
    public enum BuffedData{Health, Speed}

    public float value;
    
    public override void UseCapacity(PLAYER_MANAGER player, Transform target)
    {
        float distanceToTarget = Vector3.Distance(player.transform.position, target.position);
        
        switch (buffTarget)
        {
            case BuffTarget.Self:
                //appliquer le buff sur soi
                target = player.transform;
                
                switch (buffedData)
                {
                    case BuffedData.Health:
                        player.currentHealth += Mathf.FloorToInt(value);
                        Debug.Log("player got a health boost of " + value);
                        if (particleEffect != null)
                        {
                            var vfx = Instantiate(particleEffect, new Vector3(player.transform.position.x, particleEffect.gameObject.transform.position.y, player.transform.position.z), particleEffect.gameObject.transform.rotation);
                            vfx.transform.SetParent(target);
                        }
                        break;
                    
                    case BuffedData.Speed:
                        player.currentSpeed += value;

                        if (overtimeEffect)
                        {
                            
                        }
                        
                        break;
                    
                }
                
                break;
            
            case BuffTarget.Target:
                //si la cible est allié, alors appliqué l'effet du buff
                switch (buffedData)
                {
                    case BuffedData.Health :
                        if (target != null && target.gameObject.GetComponent<PNJ>())
                        {
                            PNJ allyTarget = target.gameObject.GetComponent<PNJ>();
                    
                            if (distanceToTarget < maxRange)
                            {
                                allyTarget.currentHealth += Mathf.FloorToInt(value);
                                Debug.Log(target.name + " got heal by " + value);
                            }
                        }
                        break;
                    
                    
                }
                
                break;
            
            case BuffTarget.Group:
                //appliquer l'effet à toutes les cibles dans le groupe
                
                break;
        }
    }
}
