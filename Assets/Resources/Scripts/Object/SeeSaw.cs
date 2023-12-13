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
            isTriggered = true;
            StartCoroutine(ActivateAndDeactivateStone());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
            stone.SetActive(false);
        }
    }


    private IEnumerator ActivateAndDeactivateStone()
    {
        if(isTriggered)
        {
            stone.transform.localPosition = stoneSpawnPoint.localPosition;
            SoundManager.instance.SfxPlaySound2(2, this.gameObject.transform.position);
            yield return null;
            stone.SetActive(true);
        }
    }
}
