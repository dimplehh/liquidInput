using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadKeyBoardClick : MonoBehaviour
{
    public int loadindex; //챕터는 1부터 시작이고 이거는 0부터 시작하는거 통일성있게 가야함
    [SerializeField] private LoadSlotSelect? loadSlotSelect; //수동 불러오기
    [SerializeField] private RectTransform?[] selectPos; //선택이미지 위치
    public Text[] selectText; //선택 텍스트
    private void OnEnable()
    {
        loadindex = 0;
        SelectTextColorWhite();
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
        ExitButton();
    }
    private void ExitButton()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
                for (int i = 0; i < selectText.Length; i++)
                {
                    selectText[i].color = Color.black;
                }
            }
        }
    }
    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (loadindex >= 3)
            {
                loadindex = 3;
            }
            else
            {
                SelectTextColorBlack();
                loadindex++;
                SelectTextColorWhite();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (loadindex <= 0)
            {
                loadindex = 0;
                
            }
            else
            {
                SelectTextColorBlack();
                loadindex--;
                SelectTextColorWhite();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadSlotSelect.Slot(loadindex);
        }
    }
    public void SelectTextColorWhite()
    {
        selectText[loadindex].color = Color.white;
        selectText[loadindex].fontSize = 40;
    }
    public void SelectTextColorBlack()
    {
        selectText[loadindex].color = Color.black;
        selectText[loadindex].fontSize = 30;
    }
}
