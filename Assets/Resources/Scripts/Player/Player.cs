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
    [SerializeField]
    GameObject shadow;

    [Header("상태")]
    public bool isGround = false;
    public bool isSlime = false;
    public bool canStepup = false;
    public bool isSlow = false;
    private bool isFlicker = false; //슬라임 형태에서 9초가 되었을 때 반짝이면서 유저에게 인간에게 돌아간다는 표현
    private bool isHill = false; //언덕 오르기 
    public Vector3 sVec;
    Vector3 ladderPosition;
    public bool pressX = true;
    public float h;
    public float pushForce;
    public bool attached = false;
    public Transform attachedTo;
    public float playerHeight;
    //public bool isParticleActive = false; 
    public bool isSandPs = true; //
    //슬라임 시간 계산
    public const float maxSlimeTime = 10f;
    public float curSlimeTime;
    //모래바람 파티클 생성시간
    public const float maxSandPsTime = 0.5f;
    public float curSandPsTime;

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
        Turn(); //이미지 좌우전환
        Run(); //달리기 
        Jump(); //점프
        Climb();//사다리오르기
        Stepup();//잡고오르기
        Swing();//줄반동
        ChangeSlime(); //슬라임 변신
        SlimeTimeCheck(); //슬라임 시간 체크
        Die();//게임오버 체크
        SandPsTime(); //모래바람 파티클 생성시간
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isPlay)
            return;
        Move(); //플레이어 이동
                //JumpCheck(); //플레이어 바닥에 닿았을 때 점프 가능한지 체크
    }
    private void SandPsTime() //모래바람 파티클 생성시간
    {
        if (!isSandPs)
        {
            curSandPsTime += Time.deltaTime;
            if(curSandPsTime >= maxSandPsTime)
            {
                curSandPsTime = 0;
                isSandPs = true;
            }
        }
    }
    private void Move()
    {
        if (!anim.GetBool("inLadder"))
        {
            h = Input.GetAxisRaw("Horizontal");
            if(isHill)
                rigid.AddForce(Vector2.right * h * 0.9f, ForceMode2D.Impulse);
            else
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
        if (isSlow) realMaxSpeed = maxSpeed / 2.5f;

        //플레이어 이동 속도 제어
        if (rigid.velocity.x > realMaxSpeed)
            rigid.velocity = new Vector2(realMaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < realMaxSpeed * (-1))
            rigid.velocity = new Vector2(realMaxSpeed * (-1), rigid.velocity.y);

        //걷기 애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("IsWalk", false);
        else if(!attached)
            anim.SetBool("IsWalk", true);


        if (rigid.velocity != Vector2.zero && isGround && isSandPs)
        {
            SoundManager.instance.SfxPlaySound(7, transform.position); //이따가 함수 만들어서 구분지어서 sand,water,mud 나오게 할듯
            if (!isHill) //언덕에 올라가지 않았을 때
            {
                isSandPs = false;
                GameManager.instance.effectsPool.Get(0, this.transform);
                
            }
            else
            {
                isSandPs = false;
                GameManager.instance.effectsPool.Get(2, this.transform);
            }
            
        }
    }
    private void Run()
    {
        //쉬프트 눌렀을 때 달리기 아니면 걷기
        if (Input.GetKey(KeyCode.LeftShift) && !isSlow)
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
        if(isGround && !isSlime && !isSlow && !anim.GetBool("canGrab"))
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
        if (Input.GetButton("Horizontal") && !anim.GetBool("canGrab") && !attached)
        {
             if (anim.GetBool("inLadder") && (this.tag =="inLadder" || this.tag == "inSafetyZone"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
            }
             else
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
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

    private void Climb()//사다리 오르기
    {
        if (this.tag == "inLadder" && Input.GetKeyDown(KeyCode.X) && !anim.GetBool("inLadder"))
        {
            float alpha;
            alpha = (ladderPosition.x <= this.transform.position.x) ? 0.3f : -0.3f;
            this.transform.position = new Vector3(ladderPosition.x + alpha, transform.position.y, transform.position.z);
            anim.SetBool("inLadder", true);
            spriteRenderer.flipX = (ladderPosition.x <= this.transform.position.x);
        }
        else if(anim.GetBool("inLadder"))
        {
            float k = Input.GetAxisRaw("Vertical");
            Ladder(k);
            if (Input.GetKeyDown(KeyCode.A) && ladderPosition.x < this.transform.position.x)
                this.transform.position = new Vector3(ladderPosition.x - 0.3f, transform.position.y, transform.position.z);
            else if (Input.GetKeyDown(KeyCode.D) && ladderPosition.x > this.transform.position.x)
                this.transform.position = new Vector3(ladderPosition.x + 0.3f, transform.position.y, transform.position.z);
        }
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

    private void ChangeSlime() //슬라임 형태일 때는 깜빡일 때 체인지슬라임 키 누르면 슬라임 시간 10초 추가 깜빡이지 않을 때 면서 슬라임 일때는 불가
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isSlime && !isFlicker && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                SoundManager.instance.SfxPlaySound(4, transform.position);
                isSlime = true;
                anim.SetBool("IsSlime", true);
                GameManager.instance.waterParticle.GetComponent<ParticleSystem>().Play(); // 이거 말고 GameManager에 curWaterReverse 줄어들 때 함수 따로 만드는게 좋을듯
                GameManager.instance.curWaterReserves -= GameManager.instance.CurrentStageWaterConsume(StageManager.instance.currentStageIndex); //코드가 별로임... 너무 안이뻐..
                this.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.75f);
                this.GetComponent<CapsuleCollider2D>().size= new Vector2(0.81f, 0.94f);
            }
            else if (isSlime && isFlicker)
            {
                GameManager.instance.curWaterReserves -= GameManager.instance.CurrentStageWaterConsume(StageManager.instance.currentStageIndex);
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
                this.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.04f);
                this.GetComponent<CapsuleCollider2D>().size = new Vector2(0.81f, 2.37f); //player collider 크기 수정
                isSlime = false;
                isFlicker = false;
                anim.SetBool("IsSlime", false);
            }
            
            if(curSlimeTime >= maxSlimeTime * 0.9) //인간으로 돌아가기 1초전
            {
                isFlicker = true;
                Flickering(curSlimeTime - 9);
            }
        }
    }

    private void Flickering(float time)
    {
        if (time < 0.2f || 0.4f <= time && time < 0.6f || 0.8f <= time)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    private void Die()
    {
        if(GameManager.instance.curWaterReserves <= 0) //현재 시작 물보유량이 0이어서 test용         //if (GameManager.instance.curWaterReserves <= 0)
        {
            //if (isSlime)
            //    anim.Play("SlimeDie");
            if(!isSlime)
            {
                Debug.Log("이제 죽을거야");
                anim.Play("ThirstyDie");
                
                GameManager.instance.OpenGameOver();
                GameManager.instance.isPlay = false;
            }
        }
    }

    void Swing()
    {
        if (attached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Detach();
            else if (Input.GetKey(KeyCode.UpArrow))
                Slide(1);
            else if (Input.GetKey(KeyCode.DownArrow))
                Slide(-1);
        }
    }
    public void Attach(Rigidbody2D ropeBone)
    {
        attached = true;
        anim.SetBool("inRope", true);
        anim.Play("RopeIdle");
        hj.anchor = (spriteRenderer.flipX) ? new Vector2(-0.3f, 0.5f) : new Vector2(0.3f, 0.5f);
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attachedTo = ropeBone.gameObject.transform.parent;
        rigid.constraints = RigidbodyConstraints2D.None;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Detach()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        hj.anchor = new Vector2(0, 0);
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
        anim.SetBool("inRope", false);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        StartCoroutine("ChangeAttachedTo");
    }

    void Slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if(direction > 0)
        {
            if(myConnection.connectedAbove != null)
            {
                if(myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }
        }
        else
        {
            if(myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }
        if(newSeg != null)
        {
            //transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
            anim.SetTrigger("isRopeClimb");
        }
    }

    IEnumerator ChangeAttachedTo()
    {
        yield return new WaitForSeconds(1.5f);
        attachedTo = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //바닥과 닿았는지 체크 후 점프 가능한 상태로 만들어줌
        if ((collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Hill")) || collision.gameObject.layer == 8 && !isSlime)
        {
            isGround = true;
            if(!collision.gameObject.CompareTag("Hill"))
                shadow.SetActive(true);
            if (this.playerHeight > gameObject.transform.position.y + 7.0f)//추락사 (일단 5.0f) 차후 수정 필요,
            {
                if(!isSlime)anim.Play("Die");
                GameManager.instance.OpenGameOver();
                GameManager.instance.isPlay = false;
            }
        }
        if((collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Hill")))
            this.playerHeight = gameObject.transform.position.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("착지 이펙트 생성");
            GameManager.instance.effectsPool.Get(1, this.transform );
        }
    }
     
private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Hill")) && !isSlime)
        {
            isGround = false;
            shadow.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Platform")) && isSlow)
        {
            isSlow = false;
        }
        if (other.gameObject.layer == 7)//사다리에 닿았을 때
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
        if (other.gameObject.CompareTag("Hill"))
        {
            isHill = true;

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SandSwamp"))
        {
            isSlow = true;
            
        }
        if (!attached && !isSlime && Input.GetKeyDown(KeyCode.X))
        {
            if (other.gameObject.tag == "Rope")
            {
                if (attachedTo != other.gameObject.transform.parent)
                {
                    Attach(other.gameObject.GetComponent<Rigidbody2D>());
                }
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
        if (other.gameObject.CompareTag("Hill"))
        {
            isHill = false;
        }
        if (other.gameObject.CompareTag("SandSwamp"))
        {
            isSlow = false;
            Debug.Log("늪 나왔다");
        }
    }
    void LadderOut()
    {
        this.rigid.gravityScale = 3.0f;
        anim.SetBool("inLadder", false);
        anim.SetBool("isClimb", false);
    }
}
