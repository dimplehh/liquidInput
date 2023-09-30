using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SaveZone")
        {
            GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
            GameManager.instance.OpenGameOver();
            GameManager.instance.isPlay = false;
        }
    }
}
