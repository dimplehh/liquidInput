using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonster : MonoBehaviour
{
    [SerializeField] GameObject hitPoint;
    [SerializeField] float baseY;
    [SerializeField] float changedY;
    private float baseX;
    private float playerPositionX;
    private float curCheckPlayerTime = 0f;
    private float maxCheckPlayerTime = 15f;
    private void Start()
    {
        baseX = hitPoint.transform.position.x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine("WakeUpPlant");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine("SleepPlant");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            curCheckPlayerTime += Time.deltaTime;
            if(curCheckPlayerTime > maxCheckPlayerTime)
            {
                playerPositionX = collision.gameObject.transform.position.x;
                hitPoint.transform.position = new Vector3(playerPositionX, 0, 0);
                curCheckPlayerTime = 0;
            }
            else if ( maxCheckPlayerTime * 2 / 5 <= curCheckPlayerTime && curCheckPlayerTime <=  maxCheckPlayerTime * 4 / 5 )
            {
                 hitPoint.transform.position = new Vector3(baseX, changedY, 0);
            }
        }
    }

    IEnumerator WakeUpPlant()
    {
        while (hitPoint.transform.position.y < changedY)
        {
            hitPoint.transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator SleepPlant()
    {
        while (hitPoint.transform.position.y > baseY)
        {
            hitPoint.transform.Translate(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
