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
        //슬라임 상태인지 아닌지에 따라 Animator 상태를 Idle or SlimeIdle 상태로 변경 
        yield return null;
    }
}
