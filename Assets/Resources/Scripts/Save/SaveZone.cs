using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveZoneData
{
    public int id;
    public bool isSave;
}

[Serializable]
public class SaveZone : Zone
{
    public int id;
    public Animator anim;
    public Sprite saveSprite;
    public SpriteRenderer saveRenderer;
    public SpriteRenderer saveOtherRenderer;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        isSave = false;
        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && !isSave)
        {

            isSave = true;
            anim.enabled = true;
            anim.SetBool("Active",true);
            SoundManager.instance.SfxPlaySound(6, transform.position);
            //StageManager.instance.UpdateSave(this);
            AutoSave(other.gameObject);
            GameManager.instance.checkSaveSlot.SlotSaveFileCheck();
        }
    }

    public void DeSave()
    {
        isSave = false;
        anim.SetBool("Active",false);
    }
    public void Save()
    {
        saveRenderer.sprite = saveSprite;
        saveOtherRenderer.sprite = saveSprite;
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
            GameManager.instance.isNonAutoSave = false; 
            Debug.Log("player가 SaveZone에서 나갔습니다.");
        }
    }
}
