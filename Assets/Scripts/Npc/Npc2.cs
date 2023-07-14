using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2 : Npc
{
    public override void Start() //��ģ���� ��ȭ�� ���� npc�̱� ������ ��ȣ�ۿ�Ƚ���� �����Ű���ʴ´�.
    {
        //interactionCount = 1;
    }
    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(SuccessMessege());
            Debug.Log("�ƹ��� ȿ���� �����ϴ�.");
        }
    }

    public override IEnumerator SuccessMessege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "�� ���� �ʿ����!";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }

    //public override IEnumerator FailMessege()
    //{
    //    yield return null;
    //}
}
