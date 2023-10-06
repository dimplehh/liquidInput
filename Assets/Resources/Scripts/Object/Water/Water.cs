using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WaterData
{
    public int id;
    public int waterReserves;
}

[Serializable]
public class Water : MonoBehaviour
{
    public int id;
    public int currentWaterReserves;
    protected int maxWaterReserves;
    //[SerializeField] protected Image currentWaterReservesImage;

    protected virtual void Start()
    {
        
    }
    protected virtual void Init()
    {
        
    }
    
    
    public virtual void UpdateWater()
    {
        
    }
}
