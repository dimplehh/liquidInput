using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolumePanel : MonoBehaviour
{
    public int index;
    public GameObject keySettingPanel;

    public int maxFontSize;
    public int minFontSize;
    public Text[] selectText; //선택 텍스트
    private void OnEnable()
    {
        index = 0;
        SelectTextColorWhite();
    }

    private void Update()
    {
        SlotLoadKeyBoardClick();
    }
    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index >= 2)
            {
                index = 2;
            }
            else
            {
                SelectTextColorBlack();
                index++;
                SelectTextColorWhite();
                SoundManager.instance.SfxPlaySound(2, transform.position);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index <= 0)
            {
                index = 0;

            }
            else
            {
                SelectTextColorBlack();
                index--;
                SelectTextColorWhite();
                SoundManager.instance.SfxPlaySound(2, transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(index == 0)
            {
                SoundManager.instance.SfxPlaySound(3, transform.position);
                keySettingPanel.SetActive(true);
            }
        }
    }

    public void SelectTextColorWhite()
    {
        selectText[index].color = Color.white;
        selectText[index].fontSize = maxFontSize;
    }
    public void SelectTextColorBlack()
    {
        selectText[index].color = Color.black;
        selectText[index].fontSize = minFontSize;
    }
}
