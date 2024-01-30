using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]private int obstacleNum = 0;
    public bool isTriggerEnter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SaveZone")
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if(!isTriggerEnter)
                {
                    isTriggerEnter = true;
                    GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
                    GameManager.instance.OpenGameOver();
                    GameManager.instance.isPlay = false;
                }
            }
        }
    }
}
