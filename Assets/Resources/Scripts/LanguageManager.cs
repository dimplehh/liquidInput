using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
// 변환 오류 있는 것들만 처리
/// </summary>
public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    [SerializeField] private List<CustomLanguage> customLanguages;

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
    }

    public void ChangeLanguage(int index)
    {
        for(int i = 0; i<customLanguages.Count; i++)
        {
            customLanguages[i].SetLanguageText(index);
        }
    }
}
