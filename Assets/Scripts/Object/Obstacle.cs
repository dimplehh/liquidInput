using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.tag == "Player")
        {
            if(player != null && !player.isSlime)
            {
                GameManager.instance.curWaterReserves = 0;
            }
        }
    }
}
