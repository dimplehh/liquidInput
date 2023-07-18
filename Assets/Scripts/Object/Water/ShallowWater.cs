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
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                StartCoroutine("AbsorbWater");
            }
        }
        
    }
    IEnumerator AbsorbWater()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameManager.instance.curWaterReserves += 2;
        currentWaterReserves--;
        if (currentWaterReserves == 0)
            this.gameObject.SetActive(false);
    }
}
