using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotMoveStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SaveZone"))
        {
            if(GameManager.instance.player.GetComponent<Player>().isSlime)
                this.GetComponent<Rigidbody2D>().mass = 1000f;
            else
                this.GetComponent<Rigidbody2D>().mass = 2f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SaveZone"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
                this.GetComponent<Rigidbody2D>().mass = 1000f;
            else
                this.GetComponent<Rigidbody2D>().mass = 2f;
        }
    }
}
