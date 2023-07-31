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
    [SerializeField] protected Transform target; //�÷��̾�
    [SerializeField] protected bool isRange = false; //���� �Ÿ��������� üũ
    [SerializeField] protected float npcDistance; //�� npc �Ÿ�
    [SerializeField] protected float _distance; //�÷��̾�� npc ���� �Ÿ�
    //���� �ð�
    [SerializeField] protected float soundTime;
    [SerializeField] protected float initSoundTime = 2;

    //���� ������ 
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
        DistanceCheak(); //�Ÿ�üũ
        //�ش� �Ÿ��ȿ� ������ ����
        if (!isRange)
            return;
        if (!anim.GetBool("IsSuccess")) //�������� �ʾ����� ��� ȣ���Ѵ�.
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

        _distance = Vector2.Distance(thisPos, targetPos); // ���߿� �÷��̾�� npc �Ÿ����� 

        if (_distance < npcDistance) // ���� �÷��̾ npc ���� ��Ÿ� ���� ���Դٸ�,
        {
            return isRange = true;
        }
        else
        {
            return isRange = false;
        }
    }
}
