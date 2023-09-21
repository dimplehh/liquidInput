using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeepWater : Water
{
    //[SerializeField] protected Image currentWaterReservesImage;
    float time = 0f;
    [SerializeField]
    GameObject deadZone;
    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = 5;
        currentWaterReserves = maxWaterReserves;
    }
    protected override void Start()
    {
        Init();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.player.GetComponent<Rigidbody2D>().mass = 0.6f;//�ٽ� �÷��̾� ���� ������� �ǵ���
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (currentWaterReserves <= 0)//�����ִ� ���� 0���� ���� ��
                gameObject.SetActive(false);//������ ��Ȱ��ȭ

            if (!GameManager.instance.player.GetComponent<Player>().isSlime //�������� �ƴϸ鼭
                && currentWaterReserves >= 3 && collision.transform.position.y <= deadZone.transform.position.y) //���� ���� 3 �̻��ε� �÷��̾� ��ġ�� �̷���
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("Die");//����
                GameManager.instance.curWaterReserves = 0;
            }
            if (!GameManager.instance.player.GetComponent<Player>().isSlime) //��� ������ ��
                GameManager.instance.player.GetComponent<Rigidbody2D>().mass = 1.5f; //�������
            else//������ ������ ��
            {
                if ((Input.GetKey(KeyCode.X)))//�� ����ϸ�
                {
                    time += Time.deltaTime;
                    if (time >= 0.05f)
                        if (currentWaterReserves > 0)
                        {
                            SoundManager.instance.SfxPlaySound(5, transform.position);
                            GameManager.instance.curWaterReserves += 1;
                            currentWaterReserves--;
                            this.transform.position -= new Vector3(0, 0.5f, 0);
                            time = 0f;
                        }
                }
            }
        }
    }

}
