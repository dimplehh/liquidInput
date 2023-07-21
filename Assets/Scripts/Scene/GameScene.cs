using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField]
    GameObject s;
    [SerializeField]
    GameObject[] stagePiece;
    public GameObject player;
    [SerializeField]
    int stage;
    [SerializeField]
    public int savePoint;
    void Awake()
    {
        stage = StageManager.instance.currentStageIndex; //시작시 저장되어있는 스테이지로 대입
        //stage = 1;//test용
        Init();
    }

    protected override void Init()
    {
        base.Init();
        Debug.Log("여기 옴");
        player = Managers.Game.Spawn("Player");
        if (stage != 1 && GameObject.FindWithTag("Stage").name == "BaseStage1")
        {
            Managers.Resource.Destroy(GameObject.FindWithTag("Stage"));
            Managers.Resource.Instantiate("Map/Stage/BaseStage" + stage, s.transform);
        }
        if (GameObject.FindWithTag("Stage"))
        {
            if (savePoint != 1)
            {
                GameObject.FindWithTag("Stage").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindWithTag("Stage").transform.GetChild(savePoint - 1).gameObject.SetActive(true);
                player.transform.Translate(39.5f * (savePoint - 1) ,0,0);
            }
        }
    }

    public override void Clear()
    {

    }
}
