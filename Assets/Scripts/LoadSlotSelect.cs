using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadSlotSelect : MonoBehaviour
{
    public GameObject creat; //����ִ� ������ ������ �� �ߴ� â
    public Text[] slotTxt;
    //public Text newSaveName;

    private bool[] saveFile = new bool[4];
    void Start()
    {
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i = 0; i <4; i++)
        {
            if(File.Exists(Managers.Data.path + $"{i}"))
            {
                saveFile[i] = true;
                Managers.Data.nowSlot = i;
                Managers.Data.LoadData(i);
                if (i == 0)
                    slotTxt[i].text = "���� �������� : " + Managers.Data.playerData.currentStage.ToString() + " Stage";
                else
                    slotTxt[i].text = "ĳ���� ��ġ : " + Managers.Data.playerData.playerXPos.ToString();
            }
            else
            {
                slotTxt[i].text = "�������";
            }
        }
    }
    public void Slot(int num)
    {
        Managers.Data.nowSlot = num;
        //1. ����� �����Ͱ� ���� ��
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
        Debug.Log("�� ���ӿ� ���Խ��ϴ�.");
        SceneManager.LoadScene("GameScene");
    }
}
