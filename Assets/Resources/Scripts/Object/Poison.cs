using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SaveZone")
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("SlimeDie");
                GameManager.instance.OpenGameOver();
                GameManager.instance.isPlay = false;
            }
            else
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
                GameManager.instance.OpenGameOver();
                GameManager.instance.isPlay = false;
            }
        }
    }
}
