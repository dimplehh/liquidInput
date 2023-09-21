using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [SerializeField] protected int soundIndex;
    float time = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            SoundManager.instance.Bgm2PlaySound(soundIndex, this.gameObject.transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            SoundManager.instance.Bgm2StopSound(soundIndex);
        }
    }
}
