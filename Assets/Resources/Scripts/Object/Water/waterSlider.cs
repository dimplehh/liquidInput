using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterSlider : MonoBehaviour
{
    float height = -0.48f;
    Water2DTool.Water2D_Tool waterTool;
    GameManager gm;
    public void Start()
    {
        waterTool = this.GetComponent<Water2DTool.Water2D_Tool>();
        height = waterTool.handlesPosition[1].y;
        waterTool.handlesPosition[0] = new Vector2(0, height);
        gm = GameManager.instance;
        waterTool.handlesPosition[0] = new Vector2(0, height + (float)gm.curWaterReserves / 50);
    }
    private void LateUpdate()
    {
        if (gm.oldCurWaterReserves != gm.curWaterReserves)
        {
            waterTool.handlesPosition[0] = new Vector2(0, height + (float)gm.curWaterReserves / 50);
            gm.oldCurWaterReserves = gm.curWaterReserves;
        }
    }
}