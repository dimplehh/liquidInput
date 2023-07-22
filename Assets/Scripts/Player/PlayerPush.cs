using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 0.5f;
    public LayerMask boxMask;
    GameObject box;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        int right = (player.spriteRenderer.flipX) ? -1 : 1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(right, 0) * transform.localScale.x, distance, boxMask);
        if (hit.collider != null && hit.collider.gameObject.layer == 12 && player.canGrab)
        {
            //player.canGrab = true;
            box = hit.collider.gameObject;
            box.GetComponent<Rigidbody2D>().mass = 1;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            if (player.h < 0 && this.gameObject.transform.position.x < box.transform.position.x)
            {
                player.anim.SetBool("isPush", false);
                player.anim.SetBool("isPull", true);
                player.spriteRenderer.flipX = false;
            }
            else if (player.h < 0 && this.gameObject.transform.position.x > box.transform.position.x)
            {
                player.anim.SetBool("isPull", false);
                player.anim.SetBool("isPush", true);
            }
            else if (player.h > 0 && this.gameObject.transform.position.x < box.transform.position.x)
            {
                player.anim.SetBool("isPull", false);
                player.anim.SetBool("isPush", true);
            }
            else if (player.h > 0 && this.gameObject.transform.position.x > box.transform.position.x)
            {
                player.anim.SetBool("isPush", false);
                player.anim.SetBool("isPull", true);
                player.spriteRenderer.flipX = true;
            }
        }
        else if (box != null && !player.canGrab)
        {
            box.GetComponent<Rigidbody2D>().mass = 100;
            box.GetComponent<FixedJoint2D>().enabled = false;
            //player.canGrab = false;
        }
    }
}