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
                slotTxt[i].text = "ĳ���� ��ġ : " + Managers.Data.playerData.playerXPos.ToString() +
                                "\n ���� �������� : " + Managers.Data.playerData.currentStage.ToString() + " Stage" +
                                "\n ����� ������ : " + Managers.Data.playerData.playerWaterReserves.ToString();
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
        LoadingSceneController.Instance.LoadScene("GameScene");
    }
    public void NewGame() 
    {
        if (File.Exists(Managers.Data.path + $"{0}")) //0��° ���Կ� �����Ͱ� ������ �����ͷ� �������� �װԾƴϸ� ���� �����Ѵ�.
        {
            GoGame();
            Debug.Log("�����Ͱ� ������ �����ͷ� ������");
        }
        else
        {
            Managers.Data.DefaultLoadData(); //�⺻����
            LoadingSceneController.Instance.LoadScene("GameScene");
            Debug.Log("�����Ͱ� ������ ���� ����");
        }    
        
    }
}
