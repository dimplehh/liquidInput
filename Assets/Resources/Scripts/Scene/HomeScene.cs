using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeScene : MonoBehaviour
{
    public Slider[] volumeSlider; //���� ���� �����̴�

    private void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        volumeSlider[0].value = SoundManager.instance.bgmAudioSource.volume;
        volumeSlider[1].value = SoundManager.instance.sfxAudioSource.volume;
    }
}
