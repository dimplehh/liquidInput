using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BGMStruct
{
    public string name;
    public AudioClip clip;
}
[Serializable]
public class BGM2Struct
{
    public string name;
    public AudioClip clip;
}
[Serializable]
public class SFXStruct
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource bgm2AudioSource;
    public AudioSource sfxAudioSource;
    public List<BGMStruct> bgmSoundList;
    public List<BGM2Struct> bgm2SoundList;
    public List<SFXStruct> sfxSoundList;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void BgmPlaySound(int index)
    {
        bgmAudioSource.clip = bgmSoundList[index].clip;
        bgmAudioSource.Play();
    }

    public void Bgm2PlaySound(int index, Vector3 pos, float volume = 1f)
    {
        transform.position = pos;
        bgm2AudioSource.clip = bgm2SoundList[index].clip;
        bgm2AudioSource.Play();
    }
    public void Bgm2StopSound(int index)
    {
        bgm2AudioSource.Stop();
    }

    public void SfxPlaySound(int index, Vector3 pos, float volume = 1f) //spatial blend 가 1이 가까워질 수록 3d 사운드로 변함
    {
        transform.position = pos;
        sfxAudioSource.clip = sfxSoundList[index].clip;
        sfxAudioSource.PlayOneShot(sfxAudioSource.clip, volume);
    }

}
