using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShallowWater : Water
{
    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = 1;
        currentWaterReserves = maxWaterReserves;
    }
    protected override void Start()
    {
        Init();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if ((Input.GetKeyDown(KeyCode.X)))
            {
                currentWaterReserves--;
                GameManager.instance.curWaterReserves++;

            }
            if (currentWaterReserves <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        
    }
}
