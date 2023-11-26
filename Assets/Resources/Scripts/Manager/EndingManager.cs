using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    /// <summary>
    /// BAD
    /// </summary>
    public readonly int Bad_Ending_GoodGauge = 5;
    public readonly int Bad_Ending_WaterReserves = 5;

    /// <summary>
    /// NORMAL
    /// </summary>
    public readonly int Normal_Ending_GoodGauge = 10;
    public readonly int Normal_Ending_WaterReserves = 10;

    /// <summary>
    /// JEAN
    /// </summary>
    public readonly int Jean_Ending_GoodGauge = 15;
    public readonly int Jean_Ending_WaterReserves = 15;

    /// <summary>
    /// HIDDEN
    /// </summary>
    public readonly int Hidden_Ending_GoodGauge = 20;
    public readonly int Hidden_Ending_WaterReserves = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    
    public int OpenEnding(float playerGoodGauge, float playerWaterReserves)
    {
        int endingIndex = 0;

        if(Hidden_Ending_GoodGauge <= playerGoodGauge && Hidden_Ending_GoodGauge <= playerWaterReserves)
        {
            endingIndex = 3;
        }
        else if (Jean_Ending_GoodGauge <= playerGoodGauge && Jean_Ending_WaterReserves <= playerWaterReserves)
        {
            endingIndex = 2;
        }
        else if (Normal_Ending_GoodGauge <= playerGoodGauge && Normal_Ending_WaterReserves <= playerWaterReserves)
        {
            endingIndex = 1;
        }
        else if (Bad_Ending_GoodGauge <= playerGoodGauge &&Bad_Ending_WaterReserves <= playerWaterReserves)
        {
            endingIndex = 0;
        }
        else
        {
            endingIndex = 0;
        }
                

        return endingIndex;
    }


}
