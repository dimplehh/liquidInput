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
    public GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");
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
    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }
}
