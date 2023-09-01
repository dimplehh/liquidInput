using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveShader : MonoBehaviour
{
    public Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
    protected void Start()
    {
        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            anim.enabled = true;
        }
    }
}
