using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float pullSpeed = 0.02f;
    [SerializeField]
    float pushSpeed = 0.01f;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().canGrab)
        {
            player = collision.gameObject;
            if ((player.GetComponent<Player>().h < 0 && player.transform.position.x < this.transform.position.x))
            {
                player.GetComponent<Player>().anim.SetBool("isPull", true);
                player.GetComponent<Player>().spriteRenderer.flipX = false;
                this.transform.position = new Vector3(this.transform.position.x - pullSpeed, this.transform.position.y, this.transform.position.z);
            }
            else if (player.GetComponent<Player>().h < 0 && player.transform.position.x > this.transform.position.x)
            {
                player.GetComponent<Player>().anim.SetBool("isPush", true);
                this.transform.position = new Vector3(this.transform.position.x - pushSpeed, this.transform.position.y, this.transform.position.z);
            }
            else if ((player.GetComponent<Player>().h > 0 && player.transform.position.x < this.transform.position.x))
            {
                player.GetComponent<Player>().anim.SetBool("isPush", true);
                this.transform.position = new Vector3(this.transform.position.x + pushSpeed, this.transform.position.y, this.transform.position.z);
            }
            else if (player.GetComponent<Player>().h > 0 && player.transform.position.x > this.transform.position.x)
            {
                player.GetComponent<Player>().anim.SetBool("isPull", true);
                player.GetComponent<Player>().spriteRenderer.flipX = true;
                this.transform.position = new Vector3(this.transform.position.x + pullSpeed, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
