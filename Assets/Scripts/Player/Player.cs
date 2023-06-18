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

    [Header("����")]
    public bool isJump = false;
    public bool isSlime = false;
    public bool canGrab = false;
    public bool pressX = false;
    public float h;

    //������ �ð� ���
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
        Turn(); //�̹��� �¿���ȯ
        Run(); //�޸��� 
        Jump(); //����
        Grab();//���
        Climb();//������
        ChangeSlime(); //������ ����
        SlimeTimeCheck(); //������ �ð� üũ
    }
    private void FixedUpdate()
    {
        Move(); //�÷��̾� �̵�
                //JumpCheck(); //�÷��̾� �ٴڿ� ����� �� ���� �������� üũ

    }
    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //�÷��̾� �̵� �ӵ� ����
        if (rigid.velocity.x > realMaxSpeed)
            rigid.velocity = new Vector2(realMaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < realMaxSpeed * (-1))
            rigid.velocity = new Vector2(realMaxSpeed * (-1), rigid.velocity.y);

        //�ȱ� �ִϸ��̼�
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("IsWalk", false);
        else
            anim.SetBool("IsWalk", true);
    }
    private void Run()
    {
        //����Ʈ ������ �� �޸��� �ƴϸ� �ȱ�
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
        if (Input.GetButtonDown("Jump") && !isJump) //���� ������ ������ �� ���� ���
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
            if (GameManager.instance.curWaterReserves <= 0)
                return;

            GameManager.instance.curWaterReserves--;
            isSlime = true;
            anim.SetBool("IsSlime", true);
            //���⼭ �̺�Ʈ�� �߻��ؾ���
           
            
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        //�ٴڰ� ��Ҵ��� üũ �� ���� ������ ���·� �������
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJump = false;
        }
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
    }
    void LadderOut()
    {
        this.rigid.gravityScale = 3.0f;
        this.gameObject.tag = "Player";
        anim.SetBool("inLadder", false);
        anim.SetBool("isClimb", false);
    }
}
