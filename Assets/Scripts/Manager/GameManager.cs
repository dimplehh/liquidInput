using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //물 보유량
    public const int maxWaterReserves = 100;
    public int curWaterReserves;
    public int oldCurWaterReserves;
    public Image curWaterReservesImage;
    public Text curWaterReservesTxt;
    public GameObject player;

    public int currentStage; //test
    public void HomeButton()
    {
        Managers.Data.SaveData(0, player.gameObject, currentStage+1, curWaterReserves = 5);
        Debug.Log("0번째에 마지막 챕터 저장하였습니다.");
        SceneManager.LoadScene("HomeScene");
    }
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
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(Managers.Data.playerData.playerXPos,0,0);
        curWaterReserves = (int)Managers.Data.playerData.playerWaterReserves;
    }
    private void LateUpdate()
    {
        WaterReservesUI();
    }
    private void WaterReservesUI()
    {
        curWaterReservesTxt.text = curWaterReserves.ToString();

        if(oldCurWaterReserves != curWaterReserves)
        {
            curWaterReservesImage.fillAmount = (float)curWaterReserves / maxWaterReserves;
            oldCurWaterReserves = curWaterReserves;
        }
       
    }

    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }
}
