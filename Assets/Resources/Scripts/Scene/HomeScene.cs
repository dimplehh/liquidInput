using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class HomeScene : MonoBehaviour
{
    public CGameID m_GameID;
    public AppId_t appID;
    public Slider[] volumeSlider; //볼륨 조절 슬라이더
    [SerializeField] private GameObject[] backgroundImage;

    private void Start()
    {
        if(SteamManager.Initialized)
        {
            appID = SteamUtils.GetAppID();
            m_GameID = new CGameID(SteamUtils.GetAppID());
        }
        SetVolume();
        SoundManager.instance.BgmPlaySound(0);
        SoundManager.instance.SfxStopSound3();
        SetBackImage();
    }

    public void SetVolume()
    {
        volumeSlider[0].value = SoundManager.instance.bgmAudioSource.volume;
        volumeSlider[1].value = SoundManager.instance.sfxAudioSource.volume;
    }

   

    private void SetBackImage()
    {
        for (int i = 0; i < 4; i++)
            backgroundImage[i].SetActive(false);
        var stageData = Managers.Data.GetSlotStageData(0);
        if (stageData == null || stageData.stageChapter == 0)
        {
            backgroundImage[0].SetActive(true);
        }
        else
        {
            backgroundImage[stageData.stageChapter-1].SetActive(true);
            if(stageData.stageChapter == 2) GetAndSetAchievemt("Step_One");
            if (stageData.stageChapter == 3)
            {
                if (SteamManager.Initialized)
                {
                    bool isAchieved = SteamUserStats.GetAchievement("Step_One", out bool achieved);
                    if (isAchieved && achieved) GetAndSetAchievemt("Slime_Step");
                }
            } 
        }
    }

    void GetAndSetAchievemt(string name)
    {
        if (SteamManager.Initialized)
        {
            bool isAchieved = SteamUserStats.GetAchievement(name, out bool achieved);
            if (isAchieved)
            {
                if (!achieved)
                {
                    SteamUserStats.SetAchievement(name);
                    Debug.Log("Achieve '" + name + "'");
                }
            }
        }
    }
}

