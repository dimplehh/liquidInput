using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : Zone
{
    // Start is called before the first frame update
    protected override void Start()
    {
        isClear = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone")) //SaveZone은 플레이어가 갖고있는것인데 헷갈려서 나중에 수정필요
        {
            StageManager.instance.currentStageIndex++; //현재스테이지 올려주고
            StageManager.instance.LastStageUp(); //스테이지 체크 후 최종 스테이지 저장
            //1스테이지의 정보를 2스테이지에 담아준다. 시작할때 2스테이지의 정보가 저장되게
            Managers.Data.StageSaveData(GameManager.instance.curWaterReserves, StageManager.instance.currentStageIndex); //현재 물보유량과 스테이지 인덱스 저장
            Managers.Data.StageLoadData(StageManager.instance.currentStageIndex); //저장과 동시에 불러와야해 
            LoadingSceneController.Instance.LoadScene("GameScene"+ (StageManager.instance.currentStageIndex - 1).ToString());//해당하는 스테이지 씬으로 이동
            
        }

    }
}
