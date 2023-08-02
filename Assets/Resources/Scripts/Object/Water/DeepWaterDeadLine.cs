using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepWaterDeadLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
                GameManager.instance.OpenGameOver();
                GameManager.instance.isPlay = false;
            }
        }
    }
}
