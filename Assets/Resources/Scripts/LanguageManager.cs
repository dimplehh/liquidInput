using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Newtonsoft.Json;

/// <summary>
// 변환 오류 있는 것들만 처리
/// </summary>
///

public class LanguageData
{
    //초기값 설정//
    public int index = 0; //language index 값
}
public class LanguageManager : MonoBehaviour
{
    public LanguageData languageData = new LanguageData();
    public static LanguageManager Instance;
    [SerializeField] private List<CustomLanguage> customLanguages;
    [SerializeField] private CustomLanguagePro customLanguagePro;
    [SerializeField] Font[] font;
    [SerializeField] Image titleImage;
    [SerializeField] Sprite[] titleImageSources;
    [SerializeField] Text[] loadLanguages;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        LoadLanguage();
        ChangeLanguage(languageData.index);
    }

    private void LoadLanguage()
    {
        if (File.Exists(Managers.Data.path + "Language"))
        {
            string langData = File.ReadAllText(Managers.Data.path + "Language");
            languageData = JsonUtility.FromJson<LanguageData>(langData);
        }
    }

    public void ChangeLanguage(int index)
    {
        if (customLanguagePro != null)
        {
            customLanguagePro.SetLanguageText(index);
        }
        if (loadLanguages != null)
        {
            for (int i = 0; i < loadLanguages.Length; i++)
            {
                loadLanguages[i].font = font[index];
            }
        }

        for (int i = 0; i < customLanguages.Count; i++)
        {
            customLanguages[i].SetLanguageText(index);
            customLanguages[i]._languageText.font = font[index];
        }
        if (titleImage != null)
            titleImage.sprite = titleImageSources[index];
        SaveLanguage(index);
    }

    private void SaveLanguage(int index)
    {
        languageData.index = index;
        string langData = JsonUtility.ToJson(languageData);
        File.WriteAllText(Managers.Data.path + "Language", langData);
    }
}
