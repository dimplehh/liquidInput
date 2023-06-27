using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
             if(player && player.GetComponent<Player>().canGrab)
            {        //4가지 경우. 1.player가 object보다 왼쪽에 있고 왼쪽으로 갈 때, 2.player가 object보다 왼쪽에 있고 오른쪽으로 갈 때, 3.player가 object보다 오른쪽에 있고 왼쪽으로 갈 때, 4.player가 object보다 오른쪽에 있고 오른쪽으로 갈 때
            Debug.Log("if문 들어옴");
            if ((player.GetComponent<Player>().h < 0 && player.transform.position.x < this.transform.position.x))
                {
                    player.GetComponent<Player>().anim.SetBool("isPull", true);
                    player.GetComponent<Player>().spriteRenderer.flipX = false;
                this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
                Debug.Log(this.transform.position);
                }
                else if (player.GetComponent<Player>().h < 0 && player.transform.position.x > this.transform.position.x)
                {
                    player.GetComponent<Player>().anim.SetBool("isPush", true);
                    this.transform.position = new Vector3(this.transform.position.x - 0.005f, this.transform.position.y, this.transform.position.z);
                Debug.Log(this.transform.position);
            }
                else if ((player.GetComponent<Player>().h > 0 && player.transform.position.x < this.transform.position.x))
                {
                    player.GetComponent<Player>().anim.SetBool("isPush", true);
                    this.transform.position = new Vector3(this.transform.position.x + 0.005f, this.transform.position.y, this.transform.position.z);
                Debug.Log(this.transform.position);
            }
                else if (player.GetComponent<Player>().h > 0 && player.transform.position.x > this.transform.position.x)
                {
                    player.GetComponent<Player>().anim.SetBool("isPull", true);
                    player.GetComponent<Player>().spriteRenderer.flipX = true;
                    this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
                Debug.Log(this.transform.position);
            }
        }
    }
}
