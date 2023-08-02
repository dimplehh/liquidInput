using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Water : MonoBehaviour
{
    public int currentWaterReserves;
    public int maxWaterReserves;
    //[SerializeField] protected Image currentWaterReservesImage;

    protected virtual void Start()
    {
        
    }
    protected virtual void Init()
    {
        
    }
    //protected virtual void LateUpdate()
    //{
    //    currentWaterReservesImage.fillAmount = Mathf.Lerp(currentWaterReservesImage.fillAmount,(float)currentWaterReserves / (float)maxWaterReserves / 1 / 1, Time.deltaTime * 5f);
       
    //}
}
