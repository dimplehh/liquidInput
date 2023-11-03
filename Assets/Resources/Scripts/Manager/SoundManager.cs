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
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource bgm2AudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource sfxAudioSource2;
    public List<BGMStruct> bgmSoundList;
    public List<BGMStruct> bgm2SoundList;
    public List<BGMStruct> sfxSoundList;
    public List<BGMStruct> sfxSoundList2;
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
    public void BgmPlaySound(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void Bgm2PlaySound(int index, Vector3 pos, float volume = 1f)
    {
        transform.position = pos;
        bgm2AudioSource.clip = bgm2SoundList[index].clip;
        bgm2AudioSource.Play();
    }
    public void BgmStopSound()
    {
        bgmAudioSource.Stop();
    }
    public void Bgm2StopSound()
    {
        bgm2AudioSource.Stop();
    }

    public void SfxPlaySound(int index, Vector3 pos, float volume = 1f) //spatial blend 가 1이 가까워질 수록 3d 사운드로 변함
    {
        transform.position = pos;
        sfxAudioSource.clip = sfxSoundList[index].clip;
        sfxAudioSource.PlayOneShot(sfxAudioSource.clip, volume);
    }

    public void SfxPlaySound2(int index, Vector3 pos, float volume = 1f)
    {
        transform.position = pos;
        sfxAudioSource2.clip = sfxSoundList2[index].clip;
        sfxAudioSource2.PlayOneShot(sfxAudioSource2.clip, volume);
    }
}
