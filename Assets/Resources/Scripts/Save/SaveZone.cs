using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : Zone
{
    public Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Start()
    {
        isSave = true;
        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            anim.enabled = true;
            SoundManager.instance.SfxPlaySound(6, transform.position);
            AutoSave(other.gameObject);
            GameManager.instance.loadSlotSelect.SlotSaveFileCheck();

            
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone")) //player �ȿ� �ڽĿ�����Ʈ�� saveZone�̶�� �±׸� ���� �浹ü ����
        {
            if (Input.GetKeyDown(KeyCode.X) && isSave)
            {
                GameManager.instance.isNonAutoSave = true;
                GameManager.instance.SaveAndLoadPanel.SetActive(true);
            }
        }
    }
    public void AutoSave(GameObject other)
    {
        Managers.Data.SlotSaveData(0, other, StageManager.instance.currentStageIndex ,GameManager.instance.curWaterReserves, GameManager.instance.playTime);
        Debug.Log(0 + "�ڵ� ���̺�");
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            isSave = true;
            GameManager.instance.isNonAutoSave = false; 
            Debug.Log("player�� SaveZone���� �������ϴ�.");
        }
    }
}
