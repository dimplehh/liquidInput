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
                Managers.Data.LoadData();
                slotTxt[i].text = "true";

            }
            else
            {
                slotTxt[i].text = "비어있음";
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
        //1. 저장된 데이터가 있을 때
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
