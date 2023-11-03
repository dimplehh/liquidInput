using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHillTrigger : MonoBehaviour
{
    [SerializeField] GameObject stone;
    [SerializeField] GameObject hill;
    float time = 10f;
    bool playerInTrigger = false;
    [SerializeField] bool onlyOneTime = false;
    [SerializeField] Vector3 firstStoneExist = new Vector3(-0.43f, 8.35f, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.Bgm2PlaySoundOneTime(2, this.gameObject.transform.position);
            if (playerInTrigger == false)
            {
                this.GetComponent<BoxCollider2D>().size = new Vector2(40f, this.GetComponent<BoxCollider2D>().size.y);
            }
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = false;
        }
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
            if(onlyOneTime)
            {
                StartCoroutine("hillTurnOff");
            }
        }
    }
    private IEnumerator hillTurnOff()
    {
        yield return new WaitForSeconds(2.0f);
        if (hill != null)
            hill.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
