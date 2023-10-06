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
    [SerializeField] private Color baseColor, changedColor;
    public int mouseIndex; //마우스 인덱스

    public KeyboardSelect? keyboardSelect; 
    public LoadKeyBoardClick? loadKeyBoardClick; 
    public SoundVolumePanel? soundVolumePanel; 
    private void Start()
    {
        selectText = GetComponentInChildren<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectText.fontSize = maxFontSize;
        selectText.color = changedColor;
        SoundManager.instance.SfxPlaySound(2, transform.position);
        if (keyboardSelect)
        {
            keyboardSelect.selectText[keyboardSelect.selectIndex].color = baseColor;
            keyboardSelect.selectText[keyboardSelect.selectIndex].fontSize = minFontSize;
            keyboardSelect.selectIndex = mouseIndex;
            keyboardSelect.selectText[keyboardSelect.selectIndex].color = changedColor;
            keyboardSelect.selectText[keyboardSelect.selectIndex].fontSize = maxFontSize;
        }
        else if (loadKeyBoardClick)
        {
            loadKeyBoardClick.SelectTextColorBlack();
            loadKeyBoardClick.loadindex = mouseIndex;
            loadKeyBoardClick.SelectTextColorWhite();
        }
        else if (soundVolumePanel)
        {
            soundVolumePanel.SelectTextColorBase();
            soundVolumePanel.index = mouseIndex;
            soundVolumePanel.SelectTextColorChanged();
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selectText.fontSize = minFontSize;
        selectText.color = baseColor;

        if (keyboardSelect)
        {
            keyboardSelect.selectText[keyboardSelect.selectIndex].color = changedColor;
            keyboardSelect.selectText[keyboardSelect.selectIndex].fontSize = maxFontSize;
        }
        else if (loadKeyBoardClick)
        {
            loadKeyBoardClick.SelectTextColorWhite();
        }
        else if (soundVolumePanel)
        {
            soundVolumePanel.SelectTextColorChanged();
        }
    }
}
