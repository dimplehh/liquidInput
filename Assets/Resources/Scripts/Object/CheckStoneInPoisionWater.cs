using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStoneInPoisionWater : MonoBehaviour
{
    [SerializeField] float yPos;
    [SerializeField] BoxCollider2D seesawCollider;
    int count = 2;

    private void Start()
    {
        if (Managers.Data.stageData.stageChapter == 2)
        {
            int waterReserves = Managers.Data.stageData.waterData.Find((x) => x.id == 9).waterReserves;
            this.gameObject.transform.position += new Vector3(0, yPos * (2 - waterReserves), 0);
            count = waterReserves;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 24 && count > 0)
        {
            SoundManager.instance.SfxPlaySound(9, this.gameObject.transform.position);
            this.gameObject.transform.position += new Vector3(0, yPos, 0);
            this.gameObject.GetComponent<Water>().currentWaterReserves--;
            count--;
            Debug.Log(count);
            collision.gameObject.SetActive(false);
            if(count > 0)
            {
                seesawCollider.enabled = false;
                seesawCollider.enabled = true;
            }
        }
    }
}
