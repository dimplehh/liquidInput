using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    private float realMaxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    [Header("상태")]
    public bool isJump = false;
    public bool isSlime = false;
    public bool canGrab = false;
    public bool canStepup = false;
    Vector3 sVec; 
    public bool pressX = false;
    public float h;

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
        Grab();//잡기
        Climb();//사다리오르기
        Stepup();//잡고오르기
        ChangeSlime(); //슬라임 변신
        SlimeTimeCheck(); //슬라임 시간 체크
        Die();//게임오버 체크
    }
    private void FixedUpdate()
    {
        Move(); //플레이어 이동
                //JumpCheck(); //플레이어 바닥에 닿았을 때 점프 가능한지 체크

    }
    private void Move()
    {
        if(rigid.gravityScale != 0)
        {
            h = Input.GetAxisRaw("Horizontal");
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
            isJump = true;
            if (isSlime)
                anim.SetTrigger("DoSlimeJump");
            else
                anim.SetTrigger("DoJump");
        }
    }
    private void Turn()
    {
        if (Input.GetButton("Horizontal") && !canGrab)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }

    private void Grab()
    {
        if (Input.GetKey(KeyCode.X))
        {
            pressX = true;
            if (canGrab)
            {
                realMaxSpeed = 1f;
                anim.SetBool("canGrab", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            pressX = false;
            canGrab = false;
            realMaxSpeed = maxSpeed;
            anim.SetBool("canGrab", false);
            anim.SetBool("isPull", false);
            anim.SetBool("isPush", false);
        }
    }

    private void Stepup()
    {
        if (this.tag == "inStepupZone")
        {   canStepup = true;   }
        else
        {   canStepup = false;  }
        if(canStepup && pressX)
        {
            StartCoroutine("SteppingUp");
        }
    }

    IEnumerator SteppingUp()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector3.zero;
        anim.SetBool("canStepup", true);
        yield return new WaitForSeconds(0.8f);
        this.transform.position = sVec + new Vector3(-0.25f, 2.0f, 0);
        rigid.gravityScale = 3;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("canStepup", false);
    }

    private void Climb()
    {
        float k = Input.GetAxisRaw("Vertical");
        if (k != 0)
        {
            Ladder(k);
        }
        else
        {
            if(this.tag == "inLadder")
            {
                anim.SetBool("isClimb", false);
                anim.SetBool("inLadder", true);
            }
        }
    }

    private void Ladder(float k)
    {
        if (this.tag == "inLadder")
        {
            rigid.gravityScale = 0;
            if (k > 0)
            {
                anim.SetBool("isClimb", true);
                this.transform.Translate(0, 5 * Time.deltaTime, 0);
            }
            else if (k < 0)
            {
                anim.SetBool("isClimb", true);
                this.transform.Translate(0, -5 * Time.deltaTime, 0);
            }
        }
        else if(this.tag == "inSafetyZone")
        {
            rigid.gravityScale = 0;
            if (k < 0)
            {
                this.transform.Translate(0, -10 * Time.deltaTime, 0);
            }
        }
        else
        {
            anim.SetBool("inLadder", false);
        }
    }

    private void ChangeSlime()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isSlime)
        {
            GameManager.instance.curWaterReserves--;
            if (GameManager.instance.curWaterReserves <= -2) //현재 시작 물 보유량이 0으로 세팅되어 있어서 테스트용
                return;
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
            if (curSlimeTime >= maxSlimeTime)
            {
                curSlimeTime = 0;
                isSlime = false;
                anim.SetBool("IsSlime", false);
            }
        }
    }

    private void Die()
    {
        if(GameManager.instance.curWaterReserves <= -2) //현재 시작 물보유량이 0이어서 test용
        {
            anim.SetBool("isDie", true);
        }
        //if (GameManager.instance.curWaterReserves <= 0)
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //바닥과 닿았는지 체크 후 점프 가능한 상태로 만들어줌
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJump = false;
        }
        //Object를 잡을 수 있는 상태인지 체크
        if (collision.gameObject.tag == "Object")
        {
            if (pressX && !anim.GetBool("IsWalk"))
            {
                canGrab = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            this.gameObject.tag = "inLadder";
        }
        if (other.gameObject.layer == 10)
        {
            this.gameObject.tag = "inSafetyZone";
        }
        if (other.gameObject.layer == 11)
        {
            this.gameObject.tag = "inStepupZone";
            sVec = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            LadderOut();
        }
        if (other.gameObject.layer == 10)
        {
            this.gameObject.tag = "inLadder";
        }
        if (other.gameObject.layer == 11)
        {
            this.gameObject.tag = "Player";
        }
    }
    void LadderOut()
    {
        this.rigid.gravityScale = 3.0f;
        this.gameObject.tag = "Player";
        anim.SetBool("inLadder", false);
        anim.SetBool("isClimb", false);
    }
}
