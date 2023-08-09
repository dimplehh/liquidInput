using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeepWater : Water
{
    //[SerializeField] protected Image currentWaterReservesImage;
    float time = 0f;
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
    //protected virtual void LateUpdate()
    //{
    //    currentWaterReservesImage.fillAmount = Mathf.Lerp(currentWaterReservesImage.fillAmount, (float)currentWaterReserves / (float)maxWaterReserves / 1 / 1, Time.deltaTime * 5f);

    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime) //슬라임 형태가 아닐때 
                GameManager.instance.curWaterReserves = 0; //사망
            else 
            {
                if ((Input.GetKeyDown(KeyCode.C)))
                {
                    time += Time.deltaTime;
                    if (time >= 0.05f)
                    {
                        if (currentWaterReserves > 0)
                        {
                            GameManager.instance.curWaterReserves += 1;
                            currentWaterReserves--;
                            this.transform.position -=  new Vector3(0, 0.5f, 0);
                            time = 0f;
                        }
                        else
                        {
                            this.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

}
