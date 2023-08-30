using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text selectText; //현재 텍스트
    [SerializeField] private int maxFontSize;
    [SerializeField] private int minFontSize;
    public int mouseIndex; //마우스 인덱스

    public KeyboardSelect? keyboardSelect; //키보드 셀렉트
    public LoadKeyBoardClick? loadKeyBoardClick; //로드키보드 클릭
    private void Start()
    {
        selectText = GetComponentInChildren<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectText.fontSize = maxFontSize;
        selectText.color = Color.white;

        if (keyboardSelect)
        {
            keyboardSelect.selectText[keyboardSelect.selectIndex].color = Color.black;
            keyboardSelect.selectText[keyboardSelect.selectIndex].fontSize = minFontSize;
            keyboardSelect.selectIndex = mouseIndex;
            keyboardSelect.selectText[keyboardSelect.selectIndex].color = Color.white;
            keyboardSelect.selectText[keyboardSelect.selectIndex].fontSize = maxFontSize;
        }
        else if (loadKeyBoardClick)
        {
            loadKeyBoardClick.SelectTextColorBlack();
            loadKeyBoardClick.loadindex = mouseIndex;
            loadKeyBoardClick.SelectTextColorWhite();
        }
        

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selectText.fontSize = minFontSize;
        selectText.color = Color.black;

        if (keyboardSelect)
        {
            keyboardSelect.selectText[keyboardSelect.selectIndex].color = Color.white;
            keyboardSelect.selectText[keyboardSelect.selectIndex].fontSize = maxFontSize;
        }
        else if (loadKeyBoardClick)
        {
            //loadKeyBoardClick.SelectTextColorBlack();
            loadKeyBoardClick.SelectTextColorWhite();
        }
    }
}
