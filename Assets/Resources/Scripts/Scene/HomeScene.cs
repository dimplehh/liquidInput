using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeScene : MonoBehaviour
{
    public Slider[] volumeSlider; //볼륨 조절 슬라이더
    [SerializeField] private GameObject[] backgroundImage;
    private void Start()
    {
        SetVolume();
        SoundManager.instance.BgmPlaySound(0);
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
        backgroundImage[StageManager.instance.lastStageIndex - 1].SetActive(true);
    }
}
