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
                Managers.Data.LoadData();
                slotTxt[i].text = "true";

            }
            else
            {
                slotTxt[i].text = "�������";
            }
        }
        Managers.Data.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Slot(int num)
    {
        Managers.Data.nowSlot = num;
        Creat();
        //1. ����� �����Ͱ� ���� ��
        if (saveFile[num])
        {
            Managers.Data.LoadData();
            
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
        if (!saveFile[Managers.Data.nowSlot])
        {
            Managers.Data.playerData.dataName = "true";
            Managers.Data.SaveData();
        }
        SceneManager.LoadScene("GameScene");
    }
}
