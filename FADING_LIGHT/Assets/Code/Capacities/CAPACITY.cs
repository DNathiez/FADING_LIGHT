using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public abstract class CAPACITY : ScriptableObject
{
    public string capacityName;
    public float cooldown;

    public float maxRange;
    
    public ParticleSystem particleEffect;
    
    public abstract void UseCapacity(PLAYER_MANAGER player, Transform target);
}
