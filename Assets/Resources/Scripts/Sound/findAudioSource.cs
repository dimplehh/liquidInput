using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class findAudioSource : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.GetComponent<VideoPlayer>().SetTargetAudioSource(0, SoundManager.instance.bgmAudioSource);
    }
}
