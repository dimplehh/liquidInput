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
        if (other.gameObject.CompareTag("Player") && !isSave)
        {
            isSave = true;
            anim.enabled = true;
            anim.SetBool("Active",true);
            if(id != 99)SoundManager.instance.SfxPlaySound(6, transform.position);
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.X) && isSave)
            {
                if (GameManager.instance.player.GetComponent<PlayerPush>().isBox == false)
                {
                    GameManager.instance.isNonAutoSave = true;
                    GameManager.instance.SaveAndLoadPanel.SetActive(true);
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
                GameManager.instance.isNonAutoSave = false;
        }
    }
    public void AutoSave(GameObject other)
    {
        Managers.Data.SlotSaveData(0, other, StageManager.instance.currentStageIndex ,GameManager.instance.curWaterReserves, GameManager.instance.playTime, GameManager.instance.deathCount);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.isNonAutoSave = false; 
        }
    }
}
