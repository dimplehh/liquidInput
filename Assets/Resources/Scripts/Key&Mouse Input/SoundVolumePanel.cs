using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolumePanel : MonoBehaviour
{
    public int index;
    public GameObject keySettingPanel;
    [SerializeField] Slider BGMSlider, SFXSlider;
    public int maxFontSize;
    public int minFontSize;
    public Text[] selectText; //선택 텍스트
    [SerializeField] private Color baseColor, changedColor;
    private void OnEnable()
    {
        index = 0;
        BGMSlider.value = SoundManager.instance.GetComponentsInChildren<AudioSource>()[0].volume;
        SFXSlider.value = SoundManager.instance.GetComponentsInChildren<AudioSource>()[1].volume;
        SelectTextColorChanged();
        //LanguageManager.Instance.ChangeLanguage(LanguageManager.Instance.languageData.index);
    }

    private void Update()
    {
        SlotLoadKeyBoardClick();
    }
    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index >= 3)
            {
                index = 3;
            }
            else
            {
                SelectTextColorBase();
                index++;
                SelectTextColorChanged();
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
                SelectTextColorBase();
                index--;
                SelectTextColorChanged();
                SoundManager.instance.SfxPlaySound(2, transform.position);
            }
        }
        else if(selectText.Length > 4) // 언어도 다룰 때
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (index >= 5)
                {
                    index = 5;
                }
                else if (index >= 3)
                {
                    SelectTextColorBase();
                    index++;
                    SelectTextColorChanged();
                    SoundManager.instance.SfxPlaySound(2, transform.position);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(index <= 3)
                {
                    index = 3;
                }
                if (index > 3)
                {
                    SelectTextColorBase();
                    index--;
                    SelectTextColorChanged();
                    SoundManager.instance.SfxPlaySound(2, transform.position);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (index == 0)
            {
                SoundManager.instance.SfxPlaySound(3, transform.position);
                keySettingPanel.SetActive(true);
            }
            else if(index == 4)
            {
                LanguageManager.Instance.ChangeLanguage(0);
            }
            else if (index == 5)
            {
                LanguageManager.Instance.ChangeLanguage(1);
            }
        }
    }

    public void SelectTextColorChanged()
    {
        selectText[index].color = changedColor;
        selectText[index].fontSize = maxFontSize;
    }
    public void SelectTextColorBase()
    {
        selectText[index].color = baseColor;
        selectText[index].fontSize = minFontSize;
    }
}
