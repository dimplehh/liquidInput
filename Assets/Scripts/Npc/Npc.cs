using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Npc : MonoBehaviour
{
    public Animator anim;
    public GameObject messegeImage;
    public Text messegeTxt;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        
    }
}
