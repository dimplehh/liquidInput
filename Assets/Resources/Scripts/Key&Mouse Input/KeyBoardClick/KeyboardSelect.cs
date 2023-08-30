using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardSelect : MonoBehaviour
{
    //�̰� �ΰ��ӿ����� �ٸ��� �����ؾ��ҵ�
    [SerializeField] private GameObject[] selectButtonEvent; //���� ��ư�� ������Ʈ 
    [SerializeField] private GameObject? resumeButtonEvent; //�ΰ��� ���� ��ư 
    //���� ���� �������� �������� ����� �� ����  
    public int selectIndex = 0; //���� ��ȣ
    public Text[] selectText; //���� �ؽ�Ʈ
    

    private void OnEnable()
    {
        selectIndex = 0;
        selectText[selectIndex].color = Color.white;
        selectText[selectIndex].fontSize = 60;
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
        SelectButton();
    }
    private void SlotLoadKeyBoardClick()
    {
        for(int i=1; i<4; i++) //���ӽ��� �����ϰ� ������Ʈ�� Ȱ��ȭ �Ǿ������� �״�� ����
        {
            if (selectButtonEvent[i].activeSelf)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectIndex >= 3)
            {
                selectIndex = 3;
            }
            else
            {
                SoundManager.instance.SfxPlaySound(2, transform.position);
                selectText[selectIndex].color = Color.black;
                selectText[selectIndex].fontSize = 50;
                selectIndex++;
                selectText[selectIndex].color = Color.white;
                selectText[selectIndex].fontSize = 60;

            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectIndex <= 0)
            {
                selectIndex = 0;
            }
            else
            {
                SoundManager.instance.SfxPlaySound(2, transform.position);
                selectText[selectIndex].color = Color.black;
                selectText[selectIndex].fontSize = 50;
                selectIndex--;
                selectText[selectIndex].color = Color.white;
                selectText[selectIndex].fontSize = 60;
            }
        }
    }

    private void SelectButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!GameManager.instance)
            {
                //�ΰ����� �ƴҶ�
                SelectButtonEvent();
                SoundManager.instance.SfxPlaySound(3, transform.position);
            }  
            else
            {
                //�ΰ��� �϶�
                IngameSelectButtonEvent();
                SoundManager.instance.SfxPlaySound(3, transform.position);
            }
        }
    }
    //Ȩȭ�� �̺�Ʈ
    private void SelectButtonEvent()
    {
        for(int i=1; i<selectButtonEvent.Length; i++) //�ǳ��� �ϳ��� ���������� ����
        {
            if (selectButtonEvent[i].activeSelf)
            {
                return;
            }
        }
        if(selectIndex == 0)
        {
            selectButtonEvent[0].GetComponentInParent<LoadSlotSelect>().NewGame();        
        }
        else
        {
            selectButtonEvent[selectIndex].SetActive(true);
        }
    }
    //�ΰ��� ���� �̺�Ʈ
    private void IngameSelectButtonEvent()
    {
        if (selectIndex == 0)
        {
            resumeButtonEvent.SetActive(false);
            GameManager.instance.PlayGame();
        }
        else if (selectIndex == 1 || selectIndex == 2)
        {
            selectButtonEvent[selectIndex].SetActive(true);
        }else if (selectIndex == 3)
        {
            GameManager.instance.HomeButton();
        }
    }
}
