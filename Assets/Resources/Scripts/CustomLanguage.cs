using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomLanguage : MonoBehaviour
{
    [SerializeField] private string _koreanLanguage;
    [SerializeField] private string _englishLanguage;

    [SerializeField] private Text _languageText;


    public void SetLanguageText(int index)
    {
        switch (index)
        {
            case 0:
                _languageText.text = _englishLanguage;
                break;
            case 1:
                _languageText.text = _koreanLanguage;
                break;
        }
           
    }
}
