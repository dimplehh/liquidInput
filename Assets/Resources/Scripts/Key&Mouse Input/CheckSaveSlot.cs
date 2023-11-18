using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Localization.Settings;

public class CheckSaveSlot : MonoBehaviour
{
    [SerializeField] GameObject creat; //����ִ� ������ ������ �� �ߴ� â
    public Text[] slotTxt;
    private bool[] saveFile = new bool[4];
    string lang = "ko";

    private void OnEnable()
    {
        lang = (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0]) ? "en" : "ko";
        SlotSaveFileCheck();
    }

    public void SlotSaveFileCheck()//���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
    {
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(Managers.Data.path + "PlayerData" + i))
            {
                saveFile[i] = true;
                Managers.Data.nowSlot = i;
                var currentSlotData = Managers.Data.GetSlotPlayerData(i);

                var aa = currentSlotData.currentStage + currentSlotData.saveDate + currentSlotData.playTime + currentSlotData.playerWaterReserves;

                if (i == 0)
                {
                    slotTxt[i].text = (lang == "ko") ?
                        "<�ڵ�> \n" + StageName(currentSlotData.currentStage) + "\n���� ��¥ : " + currentSlotData.saveDate
                        + "\n�÷��� Ÿ�� : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\n����� : " + currentSlotData.playerWaterReserves
                        : "<Auto> \n" + StageName(currentSlotData.currentStage) + "\nSave Date : " + currentSlotData.saveDate
                          + "\nPlay Time : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\nWater Reverse : " + currentSlotData.playerWaterReserves;
                }
                else
                {
                    slotTxt[i].text = (lang == "ko") ?
                        StageName(currentSlotData.currentStage) + "\n���� ��¥ : " + currentSlotData.saveDate +
                        "\n�÷��� Ÿ�� : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\n����� : " + currentSlotData.playerWaterReserves
                        : StageName(currentSlotData.currentStage) + "\nSave Date : " + currentSlotData.saveDate +
                          "\nPlay Time : " + TimeSpan.FromSeconds(currentSlotData.playTime).ToString(@"mm\:ss") + "\nWater Reverse : " + currentSlotData.playerWaterReserves;
                }
            }
            else
            {
                if (i == 0)
                {
                    slotTxt[i].text = (lang == "ko") ? "<�ڵ�>" + "\n�������" : "<Auto>" + "\nEmpty";
                }
                else
                {
                    slotTxt[i].text = (lang == "ko") ? "�������" : "Empty";
                }

            }
        }
    }
    //�������� �̸�
    public string StageName(int currentStage)
    {
        string currentName = "";
        switch (currentStage)
        {
            case 1:
                currentName = (lang == "ko") ? "�����ǿܰ�" : "The Edge of the World";
                break;
            case 2:
                currentName = (lang == "ko") ? "������ ����" : "Abandoned Village";
                break;
            case 3:
                currentName = (lang == "ko") ? "���� ��" : "Underground";
                break;
            case 4:
                currentName = (lang == "ko") ? "������ �߽ɺ�(����)" : "The heart of Pollution (Factory)";
                break;
        }
        return currentName;
    }
    public void Slot(int num)
    {
        Managers.Data.nowSlot = num;

        Debug.Log(num + "���� �̵�");
        //1. ����� �����Ͱ� ���� ��
        if (saveFile[num])
        {
            if (!GameManager.instance) //Ÿ��Ʋȭ�鿡��
                Creat();

            //�ΰ��ӿ���
            if (GameManager.instance && GameManager.instance.isNonAutoSave) //���̺� �������� ���̺길 �ǰ� ������ش�.
            {
                if (num != 0) //0�������� �������
                {
                    Save(num);
                    return;
                }

            }
            else if (GameManager.instance && !GameManager.instance.isNonAutoSave) //�޴�â������ �ε�ǰ�
                Creat();
        }
        else //1. ����� �����Ͱ� ���� ��
        {
            if (!GameManager.instance) //Ÿ��Ʋȭ�鿡��
                return;
            else //�ΰ��ӿ���
            {
                if (GameManager.instance.isNonAutoSave)
                {
                    if (num != 0) //0�������� �������
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
        Debug.Log(slotIndex + "���� ���̺�");
        SlotSaveFileCheck(); //�ٽ� ���� üũ
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }
}
