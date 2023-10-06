using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isTriggerEnter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SaveZone")
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if(!isTriggerEnter)
                {
                    GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
                    GameManager.instance.OpenGameOver();
                    GameManager.instance.isPlay = false;
                    isTriggerEnter = true;
                }
            }
        }
    }
}
