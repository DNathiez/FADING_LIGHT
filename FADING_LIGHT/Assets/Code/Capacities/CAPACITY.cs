using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class CAPACITY : ScriptableObject
{
    public string capacityName;
    public float cooldown;
    
    public float baseValue;
    public float maxRange;
    
    public ParticleSystem particleEffect;
    
    public abstract void UseCapacity(CHARACTER_CONTROLLER player, Transform target);
}
