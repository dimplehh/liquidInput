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
    public AudioSource sfxAudioSource;
    public AudioSource sfxAudioSource2;
    public AudioSource sfxAudioSource3;
    public List<BGMStruct> bgmSoundList;
    public List<BGMStruct> sfxSoundList;
    public List<BGMStruct> sfxSoundList2;
    public List<BGMStruct> sfxSoundList3;
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
    public void BgmStopSound()
    {
        bgmAudioSource.Stop();
    }
    public void SfxPlaySound(int index, Vector3 pos) //spatial blend 가 1이 가까워질 수록 3d 사운드로 변함
    {
        transform.position = pos;
        sfxAudioSource.clip = sfxSoundList[index].clip;
        sfxAudioSource.PlayOneShot(sfxAudioSource.clip);
    }

    public void SfxPlaySound2(int index, Vector3 pos)
    {
        transform.position = pos;
        sfxAudioSource2.clip = sfxSoundList2[index].clip;
        sfxAudioSource2.PlayOneShot(sfxAudioSource2.clip);
    }

    public void SfxPlaySound3(int index, Vector3 pos)
    {
        transform.position = pos;
        sfxAudioSource3.clip = sfxSoundList3[index].clip;
        sfxAudioSource3.Play();
    }

    public void SfxStopSound3()
    {
        sfxAudioSource3.Stop();
    }
}
