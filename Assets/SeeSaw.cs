using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSaw : MonoBehaviour
{
    public Transform stoneSpawnPoint;
    public GameObject stone;

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(ActivateAndDeactivateStone());
        }
    }

    private IEnumerator ActivateAndDeactivateStone()
    {
        while (isTriggered)
        {
            stone.transform.localPosition = stoneSpawnPoint.localPosition;
            SoundManager.instance.Bgm2PlaySoundOneTime(2, this.gameObject.transform.position);
            stone.SetActive(true);
            yield return new WaitForSeconds(20f);
            stone.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
    }
}
