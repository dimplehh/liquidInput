using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2 : Npc
{
    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Messege());
            Debug.Log("�ƹ��� ȿ���� �����ϴ�.");
        }
    }

    public override IEnumerator Messege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "�� ���� �ʿ����!";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }
}
