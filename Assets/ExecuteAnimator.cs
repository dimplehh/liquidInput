using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteAnimator : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        Debug.Log("½ÇÇàµÊ");
        animator = this.GetComponent<Animator>();
        animator.enabled = true;
    }
}
