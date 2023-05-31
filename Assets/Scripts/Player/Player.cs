using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    private float realMaxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
       realMaxSpeed = maxSpeed;
    }
    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
        }
        //stop speed
        if (Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x *0.5f, rigid.velocity.y);
            realMaxSpeed = maxSpeed;
            anim.SetBool("IsRun", false);
        }
        if(Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realMaxSpeed = 10f;
            anim.SetBool("IsRun", true);
        }
        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("IsWalk", false);
        else
            anim.SetBool("IsWalk", true);
    }
    private void FixedUpdate()
    {
        //Move by Key Control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
         
        if (rigid.velocity.x > realMaxSpeed)
            rigid.velocity = new Vector2(realMaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < realMaxSpeed * (-1))
            rigid.velocity = new Vector2(realMaxSpeed*(-1), rigid.velocity.y);

        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
            }
        }
    }
}
