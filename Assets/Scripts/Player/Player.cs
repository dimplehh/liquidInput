using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�̵�")]
    private Rigidbody rigid;
    private Vector3 moveVec;
    private float hAxis;
    private float speed = 5; //�̵��ӵ� - �ӽ� ����
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
        Move();
    }
    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); //ad
    }
    private void Move()
    {
        moveVec = new Vector3(hAxis, 0, 0).normalized;
        rigid.MovePosition(transform.position + moveVec * speed * Time.deltaTime);
    }
}
