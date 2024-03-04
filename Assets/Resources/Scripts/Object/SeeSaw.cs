using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSaw : MonoBehaviour
{
    public Transform stoneSpawnPoint;
    public GameObject stone;
    [SerializeField] GameObject board;
    [SerializeField] GameObject director;

    private bool isTriggered = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (director.activeSelf == false && collision.CompareTag("Player") && !isTriggered)
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
            StartCoroutine(ActivateAndDeactivateStone());
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
            stone.SetActive(false);
            stone.SetActive(true);
            stone.transform.localPosition = stoneSpawnPoint.localPosition;
            SoundManager.instance.SfxPlaySound2(6, this.gameObject.transform.position);
            yield return null;
        }
    }
}
