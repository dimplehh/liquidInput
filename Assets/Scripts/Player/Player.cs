using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    private float realMaxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    private HingeJoint2D hj;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    [Header("����")]
    public bool isGround = false;
    public bool isSlime = false;
    public bool canGrab = false;
    public bool canStepup = false;
    Vector3 sVec; 
    public bool pressX = false;
    public float h;
    public float pushForce;
    public bool attached = false;
    public Transform attachedTo;

    //������ �ð� ���
    public const float maxSlimeTime = 10f;
    public float curSlimeTime;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        hj = gameObject.GetComponent<HingeJoint2D>();
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
        Climb();//��ٸ�������
        Stepup();//��������
        ChangeSlime(); //������ ����
        SlimeTimeCheck(); //������ �ð� üũ
        Die();//���ӿ��� üũ
        Swing();//�ٹݵ�
    }
    private void FixedUpdate()
    {
        Move(); //�÷��̾� �̵�
                //JumpCheck(); //�÷��̾� �ٴڿ� ����� �� ���� �������� üũ

    }
    private void Move()
    {
        if(!anim.GetBool("inLadder"))
        {
            h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
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

    private void Jump()
    {
        if(isGround && !isSlime)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                if (isSlime)
                    anim.SetTrigger("DoSlimeJump");
                else
                    anim.SetTrigger("DoJump");
            }
        }
    }
    private void Turn()
    {
        if (Input.GetButton("Horizontal") && !canGrab && rigid.gravityScale != 0)
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

    private void Climb()//��ٸ� ������
    {
        if (this.tag == "inLadder" && Input.GetKeyDown(KeyCode.X))
            anim.SetBool("inLadder", true);
        else if(anim.GetBool("inLadder"))
        {
            float k = Input.GetAxisRaw("Vertical");
            Ladder(k);
        }
        if(!(this.tag == "inLadder" || this.tag == "inSafetyZone"))
            LadderOut();
    }

    private void Ladder(float k)
    {
        if (this.tag == "inLadder")
        {
            if (k != 0)
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
        }
        else if (this.tag == "inSafetyZone")
        {
            anim.SetBool("isClimb", false);
            if (k < 0)
                this.transform.Translate(0, -5 * Time.deltaTime, 0);
        }
    }

    private void ChangeSlime()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isSlime)
        {
            GameManager.instance.curWaterReserves--;
            if (GameManager.instance.curWaterReserves <= -2) //���� ���� �� �������� 0���� ���õǾ� �־ �׽�Ʈ��
                return;
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

    private void Die()
    {
        if(GameManager.instance.curWaterReserves <= -2) //���� ���� ���������� 0�̾ test��         //if (GameManager.instance.curWaterReserves <= 0)
        {
            anim.SetBool("isDie", true);
        }
    }

    void Swing()
    {
        if (attached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Detach();
            else if (Input.GetKeyDown(KeyCode.S))
                rigid.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            else if (Input.GetKeyDown(KeyCode.D))
                rigid.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
        }
    }
    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }
    void Detach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
        StartCoroutine("ChangeAttachedTo");
    }

    IEnumerator ChangeAttachedTo()
    {
        yield return new WaitForSeconds(1.5f);
        attachedTo = null;
    }

        private void OnCollisionStay2D(Collision2D collision)
    {
        //�ٴڰ� ��Ҵ��� üũ �� ���� ������ ���·� �������
        if (collision.gameObject.CompareTag("Platform") && !isSlime)
        {
            isGround = true;
        }
        //Object�� ���� �� �ִ� �������� üũ
        if (collision.gameObject.tag == "Object")
        {
            if (pressX && !anim.GetBool("IsWalk"))
            {
                canGrab = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !isSlime)
            isGround = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!attached)
        {
            if(other.gameObject.tag == "Rope")
            {
                if(attachedTo != other.gameObject.transform.parent)
                    Attach(other.gameObject.GetComponent<Rigidbody2D>());
            }
        }
        if (other.gameObject.layer == 7)//��ٸ��� ����� ��
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
            this.gameObject.tag = "Player";
        }
        else if (other.gameObject.layer == 10)
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
        anim.SetBool("inLadder", false);
        anim.SetBool("isClimb", false);
    }
}
