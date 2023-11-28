using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.Localization.Settings;
public class LoadSlotSelect : MonoBehaviour
{
    //public GameObject creat; //비어있는 슬롯을 눌렀을 때 뜨는 창
    [SerializeField] GameObject videoPanel;
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] private List<GameObject> _reSelectPop;
    [SerializeField] GameObject noDataPopup;
    private void OnEnable()
    {
        CheckVideoEnd();
    }
    private void CheckVideoEnd()
    {
        if(videoPanel != null)
            videoPanel.GetComponent<VideoPlayer>().loopPointReached += CheckOver;
    }

    public void GoGame()
    {
        if (File.Exists(Managers.Data.path + "PlayerData0"))
        {
            Managers.Data.SlotLoadData(Managers.Data.nowSlot);
            LoadingSceneController.Instance.LoadScene("GameScene" + (Managers.Data.stageData.stageChapter - 1).ToString());
        }
        else
            noDataPopup.SetActive(true);

    }
    public void NewGame() 
    {
        // if (File.Exists(Managers.Data.path + $"{0}")) //0번째 슬롯에 데이터가 있으면 데이터로 가져오고 그게아니면 새로 시작한다.
        // {
        //     GoGame();
        //     Debug.Log("데이터가 있으니 데이터로 가져옴");
        // }
        // else
        // {
        //     Managers.Data.DefaultLoadData(); //기본정보
        //     videoPanel.SetActive(true);
        //     buttonCanvas.SetActive(false);
        //     SoundManager.instance.BgmStopSound();
        //     videoPanel.GetComponent<VideoPlayer>().Play();
        // }
        
        if (File.Exists(Managers.Data.path + "PlayerData0")) //0번째 슬롯에 데이터가 있으면 0번 슬롯을 데이터를 삭제하고 시작한다.
        {
            File.Delete(Managers.Data.path + "PlayerData" + 0);
            File.Delete(Managers.Data.path + "StageData" + 0);
            
            Managers.Data.DefaultLoadData(); //기본정보
            videoPanel.SetActive(true);
            buttonCanvas.SetActive(false);
            SoundManager.instance.BgmStopSound();
            videoPanel.GetComponent<VideoPlayer>().Play();
        }
        else
        {
            Managers.Data.DefaultLoadData(); //기본정보
            videoPanel.SetActive(true);
            buttonCanvas.SetActive(false);
            SoundManager.instance.BgmStopSound();
            videoPanel.GetComponent<VideoPlayer>().Play();
        }
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp) //이벤트핸들러
    {
        LoadingSceneController.Instance.LoadScene("GameScene0");
        Debug.Log("데이터가 없으니 새로 시작");
    }

    public void SkipVideo()
    {
        LoadingSceneController.Instance.LoadScene("GameScene0");
        Debug.Log("데이터가 없으니 새로 시작");
    }
}
