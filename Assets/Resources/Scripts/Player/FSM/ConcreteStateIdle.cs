using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteStateIdle : State
{
    public override void DoAction(MyState state)
    {
        Debug.Log("Idle");
        StartCoroutine(HandleIdle(state));
    }

    IEnumerator HandleIdle(MyState state)
    {
        //������ �������� �ƴ����� ���� Animator ���¸� Idle or SlimeIdle ���·� ���� 
        yield return null;
    }
}
