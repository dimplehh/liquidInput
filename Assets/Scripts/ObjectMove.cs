using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(player.GetComponent<Player>().canGrab)
        {//4���� ���. 1.player�� object���� ���ʿ� �ְ� �������� �� ��, 2.player�� object���� ���ʿ� �ְ� ���������� �� ��, 3.player�� object���� �����ʿ� �ְ� �������� �� ��, 4.player�� object���� �����ʿ� �ְ� ���������� �� ��
            if((player.GetComponent<Player>().h < 0 && player.transform.position.x < this.transform.position.x))
            {
                player.GetComponent<Player>().anim.SetBool("isPull", true);
                player.GetComponent<Player>().spriteRenderer.flipX = false;
                this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
            }
            else if (player.GetComponent<Player>().h < 0 && player.transform.position.x > this.transform.position.x)
            {
                player.GetComponent<Player>().anim.SetBool("isPush", true);
                this.transform.position = new Vector3(this.transform.position.x - 0.005f, this.transform.position.y, this.transform.position.z);
            }
            else if ((player.GetComponent<Player>().h > 0 && player.transform.position.x < this.transform.position.x))
            {
                player.GetComponent<Player>().anim.SetBool("isPush", true);
                this.transform.position = new Vector3(this.transform.position.x + 0.005f, this.transform.position.y, this.transform.position.z);
            }
            else if(player.GetComponent<Player>().h > 0 && player.transform.position.x > this.transform.position.x)
            {
                player.GetComponent<Player>().anim.SetBool("isPull", true);
                player.GetComponent<Player>().spriteRenderer.flipX = true;
                this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
