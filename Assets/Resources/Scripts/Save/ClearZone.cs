using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ClearZone : Zone
{
    private const string LAST_STAGE_KEY = "LAST_STAGE";
    [SerializeField] GameObject videoPanelParent;
    [SerializeField] List<GameObject> videoPanels;
    [SerializeField] GameObject panelGroup; //esc키 안 먹히도록
    public bool oneAct = false;
    public int endingIndex = 0;
    protected override void Start()
    {
        isClear = false;
    }
    private void OnEnable()
    {
        CheckVideoEnd();
    }
    private void CheckVideoEnd()
    {
        if (videoPanels.Count != 0)
        {
            for (int i = 0; i < videoPanels.Count; i++)
            {
                videoPanels[i].GetComponent<VideoPlayer>().loopPointReached += CheckOver;
            }
        }
    }

    private void SetVideoVolume()
    {
        if (videoPanels.Count != 0)
            for (int i = 0; i < videoPanels.Count; i++)
            {
                videoPanels[i].GetComponent<VideoPlayer>().SetDirectAudioVolume(0, SoundManager.instance.bgmAudioSource.volume);

            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone")) //SaveZone은 플레이어가 갖고있는것인데 헷갈려서 나중에 수정필요
        {
            if (oneAct ==false)
            {
                StageManager.instance.currentStageIndex++; //현재스테이지 올려주고
                StageManager.instance.LastStageUp(); //스테이지 체크 후 최종 스테이지 저장
                Managers.Data.isClear = true;
                Managers.Data.playerData.isFirst = true;
                if (StageManager.instance.currentStageIndex != 4)
                {
                    Managers.Data.StageClearSaveData(GameManager.instance.curWaterReserves, StageManager.instance.currentStageIndex); //현재 물보유량과 선행게이지 스테이지 인덱스만 저장한다 나머지 정보는 날림(플레이어 위치 정보 , 스테이지 모든 데이터 정보)
                    Managers.Data.StageLoadData(StageManager.instance.currentStageIndex); //저장과 동시에 불러와야해 
                    if (!Application.CanStreamedLevelBeLoaded("GameScene" + (StageManager.instance.currentStageIndex - 1).ToString()))
                    {
                        Debug.Log("이름에 맞는 씬이 없습니다!");
                        return;
                    }
                }

                SoundManager.instance.BgmStopSound();
                panelGroup.SetActive(false);

                if (StageManager.instance.currentStageIndex == 3)     //챕터 3로 넘어갈 때
                {
                    LoadingSceneController.Instance.LoadScene("GameScene2");
                }
                
                else if (StageManager.instance.currentStageIndex >= 4)                //마지막 스테이지에서는
                {
                    if (videoPanelParent != null)
                        if (videoPanelParent.activeSelf == false)
                            videoPanelParent.SetActive(true);

                    endingIndex = EndingManager.Instance.OpenEnding(GameManager.instance.successGauge, GameManager.instance.curWaterReserves);

                    videoPanels[endingIndex].SetActive(true);
                    videoPanels[endingIndex].GetComponent<VideoPlayer>().Play();

                    SetVideoVolume();
                }
                else
                {
                    if (videoPanelParent != null)
                        if (videoPanelParent.activeSelf == false)
                            videoPanelParent.SetActive(true);

                    if (videoPanels != null)
                    {
                        videoPanels[0].SetActive(true);
                        videoPanels[0].GetComponent<VideoPlayer>().Play();
                        SetVideoVolume();
                    }
                }
                oneAct = true;
            }
        }
    }

    private void NewGame()
    {
        if (File.Exists(Managers.Data.path + "PlayerData0"))
        {
            File.Delete(Managers.Data.path + "PlayerData" + 0);
            File.Delete(Managers.Data.path + "StageData" + 0);
            Managers.Data.DefaultLoadData(); //기본정보
            Managers.Data.stageData = new StageData();
        }
        StageManager.instance.currentStageIndex = 1;
        StageManager.instance.lastStageIndex = 1;
        StageManager.instance.LastStageUp();
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp) //이벤트핸들러
    {
        //마지막 스테이지면 홈으로 이동
        if(StageManager.instance.currentStageIndex == 4) //진, 히든 엔딩은 마지막 크레딧 씬으로 넘어가도록 코드 짜기
        {
            if (endingIndex <= 1) //배드, 노말엔딩
            {
                Debug.Log(StageManager.instance.currentStageIndex);
                LoadingSceneController.Instance.LoadScene("HomeScene");
            }
            else if (endingIndex == 2) //진엔딩
                LoadingSceneController.Instance.LoadScene("CreditScene");
            else if (endingIndex == 3) //히든엔딩
                LoadingSceneController.Instance.LoadScene("CreditScene2");
        }
        else
            LoadingSceneController.Instance.LoadScene("GameScene" + (StageManager.instance.currentStageIndex - 1).ToString());//해당하는 스테이지 씬으로 이동
    }

    public void SkipVideo()
    {
        if (StageManager.instance.currentStageIndex == 2)
            LoadingSceneController.Instance.LoadScene("GameScene1");
        if (StageManager.instance.currentStageIndex == 3)
            LoadingSceneController.Instance.LoadScene("GameScene2");
        if (StageManager.instance.currentStageIndex == 4) //진, 히든 엔딩은 마지막 크레딧 씬으로 넘어가도록 코드 짜기
        {
            if (endingIndex < 2) //배드, 노말엔딩
            {
                Debug.Log(StageManager.instance.currentStageIndex);
                LoadingSceneController.Instance.LoadScene("HomeScene");
            }
            else if(endingIndex ==2) //진엔딩
                LoadingSceneController.Instance.LoadScene("CreditScene");
            else if (endingIndex == 3) //히든엔딩
                LoadingSceneController.Instance.LoadScene("CreditScene2");
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // //Output the current progress
            // m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                // m_Text.text = "Press the space bar to continue";
                Debug.Log("Press the space bar to continue");
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
