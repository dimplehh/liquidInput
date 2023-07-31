using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Npc : MonoBehaviour
{
    protected Animator anim;
    public GameObject messegeImage;
    public Text messegeTxt;
    [SerializeField] protected int interactionCount;
    [SerializeField] protected Transform target; //플레이어
    [SerializeField] protected bool isRange = false; //사운드 거리가능한지 체크
    [SerializeField] protected float npcDistance; //각 npc 거리
    [SerializeField] protected float _distance; //플레이어와 npc 사이 거리
    //사운드 시간
    [SerializeField] protected float soundTime;
    [SerializeField] protected float initSoundTime = 2;

    //성공 게이지 
    protected float successGauge;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public virtual void Start()
    {
        
    }
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {

    }
    public virtual IEnumerator SuccessMessege()
    {
        yield return null;
    }
    public virtual IEnumerator FailMessege()
    {
        yield return null;
    }

    public virtual void Update()
    {
        DistanceCheak(); //거리체크
        //해당 거리안에 없으면 리턴
        if (!isRange)
            return;
        if (!anim.GetBool("IsSuccess")) //성공하지 않았으면 계속 호출한다.
        {
            if (soundTime > 0)
            {
                soundTime -= Time.deltaTime;
                if (soundTime <= 0)
                {
                    soundTime = initSoundTime;
                    SoundManager.instance.SfxPlaySound(0, transform.position);
                }
            }
        }

    }

    public bool DistanceCheak()
    {
        Vector2 thisPos = transform.position;
        Vector2 targetPos = target.position;

        thisPos.y = 0;
        targetPos.y = 0;

        _distance = Vector2.Distance(thisPos, targetPos); // 나중에 플레이어와 npc 거리측정 

        if (_distance < npcDistance) // 만약 플레이어가 npc 사운드 사거리 내로 들어왔다면,
        {
            return isRange = true;
        }
        else
        {
            return isRange = false;
        }
    }
}
