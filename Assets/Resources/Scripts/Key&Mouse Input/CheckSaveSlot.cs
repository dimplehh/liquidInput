using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Localization.Settings;

public class CheckSaveSlot : MonoBehaviour
{
    [SerializeField] GameObject creat; //비어있는 슬롯을 눌렀을 때 뜨는 창
    [SerializeField] GameObject reSelectPop;
    [SerializeField] private bool isCreat = false;
    public Text[] slotTxt;
    private bool[] saveFile = new bool[4];
    string lang = "ko";

    public bool IsCreat
    {
        get => isCreat;
        set => isCreat = value;
    }

    
    void OnEnable()
    {
        if (LanguageManager.Instance != null && LanguageManager.Instance.languageData != null)
            lang = (LanguageManager.Instance.languageData.index == 0) ? "en" : "ko";
        SlotSaveFileCheck();
    }

    public void SlotSaveFileCheck()//슬롯별로 저장된 데이터가 존재하는지 판단
    {
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(Managers.Data.path + "PlayerData" + i))
            {
                saveFile[i] = true;
                var currentSlotData = Managers.Data.GetSlotPlayerData(i);

                var aa = currentSlotData.currentStage + currentSlotData.saveDate + currentSlotData.playTime + currentSlotData.playerWaterReserves;

                if (i == 0)
                {
                    slotTxt[i].text = (lang == "ko") ?
                        "<자동> \n" + StageName(currentSlotData.currentStage) + "\n저장 날짜 : " + currentSlotData.saveDate
                        + "\n플레이 타임 : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\n물방울 : " + currentSlotData.playerWaterReserves
                        : "<Auto> \n" + StageName(currentSlotData.currentStage) + "\nSave Date : " + currentSlotData.saveDate
                          + "\nPlay Time : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\nWater Reverse : " + currentSlotData.playerWaterReserves;
                }
                else
                {
                    slotTxt[i].text = (lang == "ko") ?
                        StageName(currentSlotData.currentStage) + "\n저장 날짜 : " + currentSlotData.saveDate +
                        "\n플레이 타임 : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\n물방울 : " + currentSlotData.playerWaterReserves
                        : StageName(currentSlotData.currentStage) + "\nSave Date : " + currentSlotData.saveDate +
                          "\nPlay Time : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\nWater Reverse : " + currentSlotData.playerWaterReserves;
                }
            }
            else
            {
                saveFile[i] = false;
                if (i == 0)
                {
                    slotTxt[i].text = (lang == "ko") ? "<자동>" + "\n비어있음" : "<Auto>" + "\nEmpty";
                }
                else
                {
                    slotTxt[i].text = (lang == "ko") ? "비어있음" : "Empty";
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
                currentName = (lang == "ko") ? "세상의외곽" : "The Edge of the World";
                break;
            case 2:
                currentName = (lang == "ko") ? "버려진 마을" : "Abandoned Village";
                break;
            case 3:
                currentName = (lang == "ko") ? "오염의 중심부" : "The heart of Pollution";
                break;
            case 4:
                currentName = (lang == "ko") ? "오염의 중심부(공장)" : "The heart of Pollution (Factory)";
                break;
        }
        return currentName;
    }
    public void Slot(int num)
    {
        SoundManager.instance.SfxPlaySound(3, transform.position);
        Managers.Data.nowSlot = num;
        Debug.Log(saveFile[num]);
        //1. 저장된 데이터가 있을 때
        if (saveFile[num])
        {
            Debug.Log("저장된 데이터 있음");
            if (!GameManager.instance) //타이틀화면에서
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
            else if (GameManager.instance && !GameManager.instance.isNonAutoSave) //메뉴창에서는 로드되게
            {
                Creat();
            }
        }
        else //1. 저장된 데이터가 없을 때
        {
            if (!GameManager.instance) //타이틀화면에서
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
        Managers.Data.SlotSaveData(slotIndex, player.gameObject, StageManager.instance.currentStageIndex, GameManager.instance.curWaterReserves, GameManager.instance.playTime, GameManager.instance.deathCount);
        SlotSaveFileCheck(); //다시 정보 체크
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);
        isCreat = true;
    }
    public void DataDelete()
    {
        for (int i = 0; i < 4; i++)
        {
            File.Delete(Managers.Data.path + "PlayerData" + i.ToString());
            File.Delete(Managers.Data.path + "StageData" + i.ToString());
        }
        if(reSelectPop != null)
        {
            reSelectPop.SetActive(false);
            SlotSaveFileCheck();
        }
    }
}
