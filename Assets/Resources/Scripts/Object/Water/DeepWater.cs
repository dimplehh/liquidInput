using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeepWater : Water
{
    [SerializeField] protected Image currentWaterReservesImage;

    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = 5;
        currentWaterReserves = maxWaterReserves;
    }
    protected override void Start()
    {
        Init();
    }
    protected virtual void LateUpdate()
    {
        currentWaterReservesImage.fillAmount = (float)currentWaterReserves / (float)maxWaterReserves;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if ((Input.GetKey(KeyCode.C)))
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

}
