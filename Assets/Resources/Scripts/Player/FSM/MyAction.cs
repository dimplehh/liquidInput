using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyState
{
    STATE_IDLE,
    STATE_JUMP,
    STATE_MOVE,
    STATE_RUN,
    STATE_TURN,
    STATE_DIE
}

public class MyAction : MonoBehaviour
{
    private MyState state;
    //Concrete클래스들의 접근점(추상클래스)
    private State myState;
    public void setActionType(MyState state)
    {
        this.state = state;
        Component c = gameObject.GetComponent<State>() as Component;
        
        if(c != null)
        {
            Destroy(c);
        }

        switch(state)
        {
            case MyState.STATE_IDLE:
                //myState = gameObject.AddComponent<ConcreteStateIdle>();
                myState.DoAction(state);
                break;
            case MyState.STATE_JUMP:
                //myState = gameObject.AddComponent<ConcreteStateJump>();
                myState.DoAction(state);
                break;
            case MyState.STATE_DIE:
                //myState = gameObject.AddComponent<ConcreteStateDie>();
                myState.DoAction(state);
                break;
            default:
                break;
        }
    }

    void Start()
    {
        setActionType(MyState.STATE_IDLE);
    }

    void Update()
    {
        switch(state)
        {
            case MyState.STATE_IDLE:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    setActionType(MyState.STATE_JUMP);
                }
                break;
            default:
                break;
        }
    }
}
