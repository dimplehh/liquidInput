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
        if (other.gameObject.CompareTag("SaveZone")) //SaveZone�� �÷��̾ �����ִ°��ε� �򰥷��� ���߿� �����ʿ�
        {
            StageManager.instance.currentStageIndex++; //���罺������ �÷��ְ�
            StageManager.instance.LastStageUp(); //�������� üũ �� ���� �������� ����
            //1���������� ������ 2���������� ����ش�. �����Ҷ� 2���������� ������ ����ǰ�
            Managers.Data.StageSaveData(GameManager.instance.curWaterReserves, StageManager.instance.currentStageIndex); //���� ���������� �������� �ε��� ����
            Managers.Data.StageLoadData(StageManager.instance.currentStageIndex); //����� ���ÿ� �ҷ��;��� 
            LoadingSceneController.Instance.LoadScene("GameScene"+ (StageManager.instance.currentStageIndex - 1).ToString());//�ش��ϴ� �������� ������ �̵�
            
        }

    }
}
