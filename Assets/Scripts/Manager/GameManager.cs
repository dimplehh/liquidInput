using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //물 보유량
    public const int maxWaterReserves = 100;
    public int curWaterReserves;
    public Image curWaterReservesImage;
    public Text curWaterReservesTxt;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
    }
    private void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        curWaterReserves = 5;
    }
    private void LateUpdate()
    {
        WaterReservesUI();
    }
    private void WaterReservesUI()
    {
        curWaterReservesTxt.text = curWaterReserves.ToString();
        curWaterReservesImage.fillAmount = (float)curWaterReserves / maxWaterReserves;
    }
}
