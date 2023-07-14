using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Water : MonoBehaviour
{
    [SerializeField] protected int currentWaterReserves;
    [SerializeField] protected int maxWaterReserves;
    [SerializeField] protected Image currentWaterReservesImage;

    protected virtual void Start()
    {
        
    }
    protected virtual void Init()
    {
        
    }
    protected virtual void LateUpdate()
    {
        currentWaterReservesImage.fillAmount = (float)currentWaterReserves / (float)maxWaterReserves;
    }
}
