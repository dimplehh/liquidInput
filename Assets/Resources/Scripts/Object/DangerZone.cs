using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("ShowObstacle");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("HideObstacle");
        }
    }

    IEnumerator ShowObstacle()
    {
        while(this.transform.GetChild(0).transform.position.y < -1f)
        {
            this.transform.GetChild(0).transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator HideObstacle()
    {
        while (this.transform.GetChild(0).position.y > -5f)
        {
            this.transform.GetChild(0).transform.Translate(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
