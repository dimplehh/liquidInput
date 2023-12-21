using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    float distance = 5f;
    public LayerMask boxMask;
    GameObject box;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        int right = (player.spriteRenderer.flipX) ? -1 : 1;
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(right, 0) * transform.localScale.x, distance, boxMask); //0, 2, 4
        if(Input.GetKey(KeyCode.X))
        {
            for (int i = 2; i < hits.Length; i += 2)
            {
                hits[i].collider.gameObject.GetComponent<Rigidbody2D>().mass = 2;
            }
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            for (int i = 2; i < hits.Length; i += 2)
            {
                hits[i].collider.gameObject.GetComponent<Rigidbody2D>().mass = 100;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        int right = (player.spriteRenderer.flipX) ? -1 : 1;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.3f), new Vector2(right, 0) * transform.localScale.x, distance, boxMask);
        if (player.isSlow || box != null && Input.GetKeyUp(KeyCode.X))
        {
            if (box != null)
            {
                box.GetComponent<Rigidbody2D>().mass = 100;
                box.GetComponent<FixedJoint2D>().enabled = false;
            }
            player.anim.SetBool("canGrab", false);
            player.anim.SetBool("isPush", false);
            player.anim.SetBool("isPull", false);
        }
        else if (hit.collider != null && hit.collider.gameObject.layer == 12
            && Mathf.Abs(hit.collider.gameObject.transform.position.x - this.gameObject.transform.position.x) <= 1.4f && Input.GetKey(KeyCode.X))
        {
            box = hit.collider.gameObject;
            if(box != null && !player.isSlime)
            {
                box.GetComponent<Rigidbody2D>().mass = 2;
                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            }
            player.anim.SetBool("canGrab", true); player.anim.SetBool("IsWalk", false);
            if(player.anim.GetBool("canGrab") && box != null)
            {
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
                else if(player.h == 0)
                {
                    player.anim.SetBool("isPush", false);
                    player.anim.SetBool("isPull", false);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(transform.position.x, transform.position.y - 0.5f) + Vector2.right * transform.localScale.x * 10f);
    }
}