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
    float width_between_back;
    [SerializeField]
    GameObject[] background = new GameObject[3];
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
       realMaxSpeed = maxSpeed;
        width_between_back = Mathf.Abs(background[0].transform.position.x - background[1].transform.position.x);
    }
    private void Update()
    {
        Turn(); //이미지 좌우전환
        Run(); //달리기 
        Jump(); //점프
    }
    private void FixedUpdate()
    {
        Move(); //플레이어 이동
        JumpCheck(); //플레이어 바닥에 닿았을 때 점프 가능한지 체크
        
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
    private void JumpCheck()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
            }
        }
        else
        {
            anim.SetBool("IsJump", false);
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump")) //점프 가능한 상태일 때 점프 기능
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
            anim.SetBool("IsRun", false);
        }
    }
    private void Turn()
    {
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject == background[2])
        {
            background[0].transform.position = new Vector3(background[2].transform.position.x + width_between_back,
                background[2].transform.position.y, background[2].transform.position.z);
            background[1].transform.position = new Vector3(background[2].transform.position.x - width_between_back,
                background[2].transform.position.y, background[2].transform.position.z);
        }
        if (collision.gameObject == background[0])
        {
            background[1].transform.position = new Vector3(background[0].transform.position.x + width_between_back,
                background[0].transform.position.y, background[0].transform.position.z);
            background[2].transform.position = new Vector3(background[0].transform.position.x - width_between_back,
                background[0].transform.position.y, background[0].transform.position.z);
        }
        if (collision.gameObject == background[1])
        {
            background[2].transform.position = new Vector3(background[1].transform.position.x + width_between_back,
                background[1].transform.position.y, background[1].transform.position.z);
            background[0].transform.position = new Vector3(background[1].transform.position.x - width_between_back,
                background[1].transform.position.y, background[1].transform.position.z);
        }
    }
}
