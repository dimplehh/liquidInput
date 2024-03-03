using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour
{
    private bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(isTrigger == false)
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
                GameManager.instance.OpenGameOver();
                GameManager.instance.isPlay = false;
                isTrigger = true;
            }
        }
        if(collision.gameObject.tag == "stoneTrigger")
        {
            isTrigger = true;
        }
    }
}
