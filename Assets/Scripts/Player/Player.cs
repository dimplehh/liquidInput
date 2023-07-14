using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float realMaxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    private HingeJoint2D hj;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Vector3 tempVec;

    [Header("����")]
    public bool isGround = false;
    public bool isSlime = false;
    public bool canGrab = false;
    public bool canStepup = false;
    public bool isSlow = false;
    private bool isFlicker = false; //������ ���¿��� 9�ʰ� �Ǿ��� �� ��¦�̸鼭 �������� �ΰ����� ���ư��ٴ� ǥ��
    public Vector3 sVec;
    Vector3 ladderPosition;
    public bool pressX = true;
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
        if (!GameManager.instance.isPlay)
            return;
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
        if (!GameManager.instance.isPlay)
            return;
        Move(); //�÷��̾� �̵�
                //JumpCheck(); //�÷��̾� �ٴڿ� ����� �� ���� �������� üũ
    }
    private void Move()
    {
        if (!anim.GetBool("inLadder"))
            h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (isSlow)
        {
            realMaxSpeed = maxSpeed / 3;
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
        if (Input.GetButton("Horizontal") && !canGrab)
        {
             if (anim.GetBool("inLadder"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
            }
             else
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    private void Grab()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (canGrab)
            {
                realMaxSpeed = 1f;
                anim.SetBool("canGrab", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
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
        if (canStepup  && Input.GetKey(KeyCode.X))
            pressX = true;
        if(canStepup && pressX)
        {
            if(pressX)
                StartCoroutine("SteppingUp");
        }
    }

    IEnumerator SteppingUp()
    {
        anim.SetBool("canStepup", true);
        rigid.gravityScale = 0;
        rigid.velocity = Vector3.zero;
        tempVec = sVec + new Vector3(-0.3f, 2.4f, 0);
        yield return new WaitForSeconds(0.8f);
        pressX = false;
        this.transform.position = sVec + new Vector3(-0.3f, 2.4f, 0);
        rigid.gravityScale = 3;
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
        //Debug.Log(ladderPosition.x + ", " + this.transform.position.x);
        if(anim.GetBool("inLadder") && Input.GetKeyDown(KeyCode.A) && ladderPosition.x < this.transform.position.x)
            this.transform.position+= new Vector3(-0.5f,0,0);
        if (anim.GetBool("inLadder") && Input.GetKeyDown(KeyCode.D) && ladderPosition.x > this.transform.position.x)
            this.transform.position += new Vector3(0.5f, 0, 0);
        if ((anim.GetBool("inLadder") || this.tag == "inSafetyZone") && (Input.GetKeyDown(KeyCode.Space)))
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

    private void ChangeSlime() //������ ������ ���� ������ �� ü���������� Ű ������ ������ �ð� 10�� �߰� �������� ���� �� �鼭 ������ �϶��� �Ұ�
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!isSlime && !isFlicker)
            {
                GameManager.instance.curWaterReserves -= GameManager.instance.CurrentStageWaterConsume(GameManager.instance.currentStage); //�ڵ尡 ������... �ʹ� ���̻�..
                isSlime = true;
                anim.SetBool("IsSlime", true);
            }
            else if (isSlime && isFlicker)
            {
                GameManager.instance.curWaterReserves -= GameManager.instance.CurrentStageWaterConsume(GameManager.instance.currentStage);
                isFlicker = false;
                curSlimeTime = 0;
            }

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
                isFlicker = false;
                anim.SetBool("IsSlime", false);
            }
            
            if(curSlimeTime >= maxSlimeTime * 0.9) //�ΰ����� ���ư��� 1����
            {
                Debug.Log("��¦��");
                isFlicker = true;
            }
        }
    }

    private void Die()
    {
        if(GameManager.instance.curWaterReserves <= 0 && !isSlime) //���� ���� ���������� 0�̾ test��         //if (GameManager.instance.curWaterReserves <= 0)
        {
            GameManager.instance.OpenGameOver();
            GameManager.instance.isPlay = false;
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
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
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
            if (Input.GetKey(KeyCode.X) && !anim.GetBool("IsWalk"))
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
        if (other.gameObject.layer == 7)//��ٸ��� ����� ��
        {
            this.gameObject.tag = "inLadder";
            ladderPosition =other.gameObject.transform.position;
        }
        if (other.gameObject.layer == 10)
        {
            this.gameObject.tag = "inSafetyZone";
        }
        if (other.gameObject.layer == 11)
        {
            this.gameObject.tag = "inStepupZone";
            sVec = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, 0);
        }

        if (other.gameObject.CompareTag("SandSwamp"))
        {
            isSlow = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!attached && Input.GetKeyDown(KeyCode.X))
        {
            if (other.gameObject.tag == "Rope")
            {
                if (attachedTo != other.gameObject.transform.parent)
                    Attach(other.gameObject.GetComponent<Rigidbody2D>());
            }
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
        if (other.gameObject.CompareTag("SandSwamp"))
        {
            isSlow = false;
        }
    }
    void LadderOut()
    {
        this.rigid.gravityScale = 3.0f;
        anim.SetBool("inLadder", false);
        anim.SetBool("isClimb", false);
    }
}
