using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;
    public static GameManager instance;
    //물 보유량
    public const int maxWaterReserves = 100;
    public int curWaterReserves;
    public int oldCurWaterReserves;
    public Image curWaterReservesImage;
    public Text curWaterReservesTxt;
    public GameObject player;

    public bool isPlay = false;

    public int currentStage;
    public void HomeButton()
    {
        Managers.Data.SaveData(0, player.gameObject, currentStage+1, curWaterReserves = 5);
        Debug.Log("0번째에 마지막 챕터 저장하였습니다.");
        SceneManager.LoadScene("HomeScene");
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        isPlay = true;
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
    }
    private void Start()
    {
        PlayGame();
        Initialized();
    }
    private void Initialized()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(Managers.Data.playerData.playerXPos,0,0);
        curWaterReserves = (int)Managers.Data.playerData.playerWaterReserves;
    }
    private void LateUpdate()
    {
        WaterReservesUI();
        if (curWaterReserves <= -2)
            StartCoroutine("OpenGameOverPanel"); 
    }
    private void WaterReservesUI()
    {
        curWaterReservesTxt.text = curWaterReserves.ToString();

        if(oldCurWaterReserves != curWaterReserves)
        {
            curWaterReservesImage.fillAmount = (float)curWaterReserves / maxWaterReserves;
            oldCurWaterReserves = curWaterReserves;
        }
    }

    public IEnumerator GameOverPanel()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        gameOverPanel.gameObject.SetActive(true);
    }
    public void OpenGameOverPanel()
    {
        //StopGame();
        StartCoroutine(GameOverPanel());
    }
    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }
}
