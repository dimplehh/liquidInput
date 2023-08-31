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
        if (other.gameObject.CompareTag("SaveZone")) //player 안에 자식오브젝트로 saveZone이라는 태그를 가진 충돌체 생성
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
        Debug.Log(0 + "자동 세이브");
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            isSave = true;
            GameManager.instance.isNonAutoSave = false; 
            Debug.Log("player가 SaveZone에서 나갔습니다.");
        }
    }
}
