using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadSlotSelect : MonoBehaviour
{
    public GameObject creat; //비어있는 슬롯을 눌렀을 때 뜨는 창
    public Text[] slotTxt;
    //public Text newSaveName;

    private bool[] saveFile = new bool[4];
    void Start()
    {
        //슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i <4; i++)
        {
            if(File.Exists(Managers.Data.path + $"{i}"))
            {
                saveFile[i] = true;
                Managers.Data.nowSlot = i;
                Managers.Data.LoadData(i);
                slotTxt[i].text = "캐릭터 위치 : " + Managers.Data.playerData.playerXPos.ToString() +
                                "\n 현재 스테이지 : " + Managers.Data.playerData.currentStage.ToString() + " Stage" +
                                "\n 물방울 보유량 : " + Managers.Data.playerData.playerWaterReserves.ToString();
            }
            else
            {
                slotTxt[i].text = "비어있음";
            }
        }
    }
    public void Slot(int num)
    {
        Managers.Data.nowSlot = num;
        //1. 저장된 데이터가 있을 때
        if (saveFile[num])
        {
            Creat();
        }
        else
        {
            return;
        }
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }
    public void GoGame()
    {
        Managers.Data.LoadData(Managers.Data.nowSlot);
        LoadingSceneController.Instance.LoadScene("GameScene");
    }
    public void NewGame() 
    {
        if (File.Exists(Managers.Data.path + $"{0}")) //0번째 슬롯에 데이터가 있으면 데이터로 가져오고 그게아니면 새로 시작한다.
        {
            GoGame();
            Debug.Log("데이터가 있으니 데이터로 가져옴");
        }
        else
        {
            Managers.Data.DefaultLoadData(); //기본정보
            LoadingSceneController.Instance.LoadScene("GameScene");
            Debug.Log("데이터가 없으니 새로 시작");
        }    
        
    }
}
