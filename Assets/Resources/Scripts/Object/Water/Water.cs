using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Water : MonoBehaviour
{
    public int currentWaterReserves;
<<<<<<< HEAD
    [SerializeField] protected int maxWaterReserves;
    //[SerializeField] protected Image currentWaterReservesImage;
=======
    public int maxWaterReserves;
    [SerializeField] protected Image currentWaterReservesImage;
>>>>>>> master

    protected virtual void Start()
    {
        
    }
    protected virtual void Init()
    {
        
    }
<<<<<<< HEAD
    //protected virtual void LateUpdate()
    //{
    //    currentWaterReservesImage.fillAmount = (float)currentWaterReserves / (float)maxWaterReserves;
    //}
=======
    protected virtual void LateUpdate()
    {
        currentWaterReservesImage.fillAmount = Mathf.Lerp(currentWaterReservesImage.fillAmount,(float)currentWaterReserves / (float)maxWaterReserves / 1 / 1, Time.deltaTime * 5f);
       
    }
>>>>>>> master
}
