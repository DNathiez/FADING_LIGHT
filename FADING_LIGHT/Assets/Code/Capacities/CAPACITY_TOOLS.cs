using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAPACITY_TOOLS : MonoBehaviour
{
    public static CAPACITY_TOOLS instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
}
