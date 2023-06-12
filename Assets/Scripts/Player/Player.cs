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

    [Header("상태")]
    public bool isJump = false;
    public bool isSlime = false;

    //슬라임 시간 계산
    public const float maxSlimeTime = 10f;
    public float curSlimeTime;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        realMaxSpeed = maxSpeed;
    }
    private void Update()
    {
        Turn(); //이미지 좌우전환
        Run(); //달리기 
        Jump(); //점프
        ChangeSlime(); //슬라임 변신
        SlimeTimeCheck(); //슬라임 시간 체크
    }
    private void FixedUpdate()
    {
        Move(); //플레이어 이동
        //JumpCheck(); //플레이어 바닥에 닿았을 때 점프 가능한지 체크
        
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //플레이어 이동 속도 제어
        if (rigid.velocity.x > realMaxSpeed)
            rigid.velocity = new Vector2(realMaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < realMaxSpeed * (-1))
            rigid.velocity = new Vector2(realMaxSpeed * (-1), rigid.velocity.y);

        //걷기 애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.3) 
            anim.SetBool("IsWalk", false);
        else
            anim.SetBool("IsWalk", true);
    }
    private void Run()
    {
        //쉬프트 눌렀을 때 달리기 아니면 걷기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realMaxSpeed = 10f;
            anim.SetBool("IsRun", true);
        }
        else
        {
            realMaxSpeed = maxSpeed;
            anim.SetBool("IsRun", false);
        }
    }
    //private void JumpCheck()
    //{
    //    if (rigid.velocity.y < 0)
    //    {
    //        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
    //        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
    //        if (rayHit.collider != null)
    //        {
    //            if (rayHit.distance < 0.5f)
    //                anim.SetBool("IsJump", false);
    //        }
    //    }
    //    else
    //    {
    //        anim.SetBool("IsJump", false);
    //    }
    //}
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump) //점프 가능한 상태일 때 점프 기능
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("DoJump");
            //anim.SetBool("IsRun", false);
            isJump = true;
        }
    }
    private void Turn()
    {
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }
    
    private void ChangeSlime()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isSlime)
        {
            if (GameManager.instance.curWaterReserves <= 0)
                return;

            GameManager.instance.curWaterReserves--;
            isSlime = true;
            anim.SetBool("IsSlime", true);
            //여기서 이벤트가 발생해야함
        }
        
    }
    private void SlimeTimeCheck()
    {
        if (isSlime)
        {
            curSlimeTime += Time.deltaTime;
            if(curSlimeTime >= maxSlimeTime)
            {
                curSlimeTime = 0;
                isSlime = false;
                anim.SetBool("IsSlime", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥과 닿았는지 체크 후 점프 가능한 상태로 만들어줌
        if (collision.gameObject.CompareTag("Platform")) 
        {
            isJump = false;
        }
    }
}
