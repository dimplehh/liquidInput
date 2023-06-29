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
                if (i == 0)
                    slotTxt[i].text = "현재 스테이지 : " + Managers.Data.playerData.currentStage.ToString() + " Stage";
                else
                    slotTxt[i].text = "캐릭터 위치 : " + Managers.Data.playerData.playerXPos.ToString();
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

        if(Managers.Data.nowSlot == 0)
        {
            Initialized();
        }     
        SceneManager.LoadScene("GameScene");
    }
    private void Initialized()
    {
        Managers.Data.playerData.playerXPos = 0;
        Managers.Data.playerData.playerWaterReserves = 5;
        Managers.Data.playerData.currentStage = 1;
    }
    public void NewGame()
    {
        Initialized();
        Debug.Log("새 게임에 들어왔습니다.");
        SceneManager.LoadScene("GameScene");
    }
}
