using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSaw : MonoBehaviour
{
    public Transform stoneSpawnPoint;
    public GameObject stone;
    [SerializeField] GameObject board;

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            if(Managers.Data.stageData.stageChapter == 2)
                if(Managers.Data.stageData.waterData.Find((x) => x.id == 9).waterReserves != 0)
                {
                    isTriggered = true;
                    StartCoroutine(ActivateAndDeactivateStone());
                }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
            stone.SetActive(false);
        }
        if (collision.gameObject.layer == 24)
        {
            isTriggered = false;
            StartCoroutine(ActivateAndDeactivateStone());
        }
    }


    private IEnumerator ActivateAndDeactivateStone()
    {
        if(isTriggered)
        {
            stone.transform.localPosition = stoneSpawnPoint.localPosition;
            SoundManager.instance.SfxPlaySound2(6, this.gameObject.transform.position);
            yield return null;
            stone.SetActive(true);
        }
    }
}
