using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShallowWater : Water
{
    [SerializeField]
    Slider slider;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if(Input.GetKey(KeyCode.C))
                {
                    time += Time.deltaTime;
                    if (time >= 0.05f)
                    {
                        GameManager.instance.curWaterReserves += 1;
                        currentWaterReserves--;
                        slider.value = currentWaterReserves;
                        if (currentWaterReserves == 0)
                            this.gameObject.SetActive(false);
                        time = 0f;
                    }
                }
            }
            //StartCoroutine("AbsorbWater");
            }
        }
        
    //IEnumerator AbsorbWater()
    //{
    //    this.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.instance.curWaterReserves += 2;
    //    currentWaterReserves--;
    //    if (currentWaterReserves == 0)
    //        this.gameObject.SetActive(false);
    //}
}
