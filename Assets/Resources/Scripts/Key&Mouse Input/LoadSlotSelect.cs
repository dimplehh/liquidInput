using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
public class LoadSlotSelect : MonoBehaviour
{
    public GameObject creat; //비어있는 슬롯을 눌렀을 때 뜨는 창
    public Text[] slotTxt;
   
    private bool[] saveFile = new bool[4];
    
    private void Start()
    {
        SlotSaveFileCheck();
        
    }
   
    public void SlotSaveFileCheck()//슬롯별로 저장된 데이터가 존재하는지 판단
    {
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(Managers.Data.path + $"{i}"))
            {
                saveFile[i] = true;
                Managers.Data.nowSlot = i;
                Managers.Data.SlotLoadData(i);

                if(i == 0)
                {
                    slotTxt[i].text = 
                                "<자동> \n" + 
                                StageName(Managers.Data.playerData.currentStage) +
                                "\n저장 날짜 : " + Managers.Data.playerData.saveDate +
                                "\n플레이 타임 : " + TimeSpan.FromSeconds(Managers.Data.playerData.playTime).ToString(@"mm\:ss") +
                                "\n물방울 : " + Managers.Data.playerData.playerWaterReserves.ToString();
                }
                else
                {
                    slotTxt[i].text = 
                                StageName(Managers.Data.playerData.currentStage) +
                                "\n저장 날짜 : " + Managers.Data.playerData.saveDate +
                                "\n플레이 타임 : " + TimeSpan.FromSeconds(Managers.Data.playerData.playTime).ToString(@"mm\:ss") +
                                "\n물방울 : " + Managers.Data.playerData.playerWaterReserves.ToString();
                }
            }
            else
            {
                if(i == 0)
                {
                    slotTxt[i].text = "<자동>" +
                        "\n비어있음";
                }
                else
                {
                    slotTxt[i].text = "비어있음";
                }
                
            }
        }
    }
    //스테이지 이름
    public string StageName(int currentStage)
    {
        string currentName = "";
        switch (currentStage)
        {
            case 1:
                currentName = "세상의 외곽";
                break;
            case 2:
                currentName = "버려진 마을";
                break;
            case 3:
                currentName = "지하 속";
                break;
            case 4:
                currentName = "오염의 중심부(공장)";
                break;
        }
        return currentName;
    }
    public void Slot(int num)
    {
        Managers.Data.nowSlot = num;

        Debug.Log(num + "으로 이동");
        //1. 저장된 데이터가 있을 때
        if (saveFile[num])
        {
            if(!GameManager.instance) //타이틀화면에서
                Creat();

            //인게임에서
            if (GameManager.instance && GameManager.instance.isNonAutoSave) //세이브 존에서는 세이브만 되게 덮어씌워준다.
            {
                if (num != 0) //0번슬롯은 저장못함
                {
                    Save(num);
                    return;
                }
                
            }
            else if(GameManager.instance && !GameManager.instance.isNonAutoSave) //메뉴창에서는 로드되게
                Creat();
        }
        else //1. 저장된 데이터가 없을 때
        {
            if(!GameManager.instance) //타이틀화면에서
                return;
            else //인게임에서
            {
                if (GameManager.instance.isNonAutoSave)
                {
                    if (num != 0) //0번슬롯은 저장못함
                    {
                        Save(num);
                        return;
                    }
                }   
            }
            
        }
        
    }
    public void Save(int slotIndex)
    {
        GameObject player = GameObject.FindWithTag("Player");
        Managers.Data.SlotSaveData(slotIndex, player.gameObject, StageManager.instance.currentStageIndex, GameManager.instance.curWaterReserves, GameManager.instance.playTime);
        Debug.Log(slotIndex + "수동 세이브");
        SlotSaveFileCheck(); //다시 정보 체크
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }
    public void GoGame()
    {
        Managers.Data.SlotLoadData(Managers.Data.nowSlot);
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
