using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardSelect : MonoBehaviour
{
    //이건 인게임에서는 다르게 적용해야할듯
    [SerializeField] private GameObject[] selectButton; //선택 버튼 
    //위에 제외 나머지는 공용으로 사용할 수 있음  
    [SerializeField] private int selectIndex = 0; //선택 번호
    [SerializeField] Text[] selectText; //선택 텍스트
    

    private void OnEnable()
    {
        selectIndex = 0;
        selectText[selectIndex].color = Color.white;
        selectText[selectIndex].fontSize = 70;
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
        SelectButton();
    }
    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectIndex >= 3)
            {
                selectIndex = 3;
            }
            else
            {
                selectText[selectIndex].color = Color.black;
                selectText[selectIndex].fontSize = 50;
                selectIndex++;
                selectText[selectIndex].color = Color.white;
                selectText[selectIndex].fontSize = 70;

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
                selectText[selectIndex].color = Color.black;
                selectText[selectIndex].fontSize = 50;
                selectIndex--;
                selectText[selectIndex].color = Color.white;
                selectText[selectIndex].fontSize = 70;
            }
        }
    }

    private void SelectButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectButtonEvent();
        }
    }
    private void SelectButtonEvent()
    {
        if(selectIndex == 0)
        {
            selectButton[0].GetComponentInParent<LoadSlotSelect>().NewGame();        
        }
        else
        {
            selectButton[selectIndex].SetActive(true);
        }
    }
}
