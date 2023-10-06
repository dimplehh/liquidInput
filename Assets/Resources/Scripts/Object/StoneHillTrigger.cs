using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHillTrigger : MonoBehaviour
{
    [SerializeField] GameObject stone;
    float time = 10f;
    bool playerInTrigger = false;
    [SerializeField] Vector3 firstStoneExist = new Vector3(-0.43f, 8.35f, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInTrigger = false;
    }

    private void Update()
    {
        if (playerInTrigger == false)
            return;
        else
        {
            stone.SetActive(true);
            time -= Time.deltaTime;
            if(time <= 0)
            {
                stone.transform.localPosition = firstStoneExist;
                time = 10.0f;
            }
        }
    }
}
