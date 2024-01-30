using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Grid")]
    private int _row = 20;
    private int _column = 25;
    [SerializeField] private Grid sandBoxGridPrefab;
    public Vector3[,] gridPosition;
    private float marginLeft = 0;
    private float marginTop = 800;
    private float offset = 35;
    private Grid[,] _sandBoxGrids;
    public bool gameOver = false;
    [SerializeField] private GameObject gridGroup;
    [SerializeField] TextMeshProUGUI chapterName;
    [SerializeField] GameObject poisonWater;
    public bool waterDie = false;
    //------------------------------------------------------
    [SerializeField]
    public GameObject waterParticle;
    float firstWater = -1000;
    [SerializeField]
    GameObject waterSlider;
    [SerializeField]
    GameObject settingPanel;
    [SerializeField]
    GameObject gameOverPanel;
    public static GameManager instance;

    public int maxWaterReserves = 50;
    public int curWaterReserves;
    public int oldCurWaterReserves;
    public Image curWaterReservesImage;
    public Text curWaterReservesTxt;
    int waterFull = 1050;
    public GameObject player;


    public float successGauge = 0;
    public int deathCount = 0;

    public float playTime = 0;

    public bool isPlay = false;
    public bool isNonAutoSave = false;
    
    
    public GameObject SaveAndLoadPanel;
    public CheckSaveSlot checkSaveSlot;

    public EffectsPool effectsPool;

    public PlayableDirector playableDirector;
    public void IsNonAutoSave() 
    {
        isNonAutoSave = false;
    }
    public void HomeButton()
    {
        Time.timeScale = 1f;
        LoadingSceneController.Instance.LoadScene("HomeScene");
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        isPlay = true;
        gameOver = false;
        isNonAutoSave = false;
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
        playTime = Managers.Data.playerData.playTime;
        SoundManager.instance.BgmPlaySound(StageManager.instance.currentStageIndex);
        if (Managers.Data.playerData.isFirst == true)
        {
            StartCoroutine(CutScene());
            StartCoroutine(ShowChapterName(0, 1, 2));
        }
        else
        {
            PlayGame();
        }
        
        StageManager.instance.FindStageObjects();
        Initialized();
        setWaterSlider();
        SetPoisonWaterHeight();
    }
    private IEnumerator CutScene()
    {
        playableDirector.Play();
        yield return new WaitForSeconds(5.5f);
        PlayGame();
    }

    IEnumerator ShowChapterName(float startAlpha, float targetAlpha, float duration)
    {
        float currentTime = 0f;
        Color startColor = chapterName.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            chapterName.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        chapterName.color = targetColor;

        yield return new WaitForSeconds(1f); // 페이드 아웃을 위한 딜레이

        StartCoroutine(FadeOut(duration));
    }

    IEnumerator FadeOut(float duration)
    {
        float currentTime = 0f;
        Color startColor = chapterName.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            chapterName.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        chapterName.color = targetColor;
    }

    private void setWaterSlider()
    {
        curWaterReservesImage.rectTransform.localPosition = new Vector2(0, firstWater + waterFull * curWaterReserves / maxWaterReserves);
    }

    private void SetPoisonWaterHeight()
    {
        if (Managers.Data.stageData.stageChapter == 2)
        {
            if(poisonWater != null)
            {
                CheckStoneInPoisionWater checkStoneInPoisionWater = poisonWater.GetComponent<CheckStoneInPoisionWater>();
                int waterReserves = Managers.Data.stageData.waterData.Find((x) => x.id == 9).waterReserves;
                this.gameObject.transform.position += new Vector3(0, checkStoneInPoisionWater.yPos * (2 - waterReserves), 0);
                checkStoneInPoisionWater.count = waterReserves;
            }
        }
    }

    public void CreateGrid()
    {
        gridPosition = new Vector3[_row, _column];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                gridPosition[i, j] = new Vector3(marginLeft + (j + 1) * offset - Screen.width / 2, -marginTop - (Screen.height - 1920) / 2 + Screen.height / 2 - (i + 1) * offset, 0);
            }
        }

        _sandBoxGrids = new Grid[_row, _column];
        for (int i = 3; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                var instance = Instantiate(sandBoxGridPrefab.gameObject);
                instance.transform.SetParent(gridGroup.transform, false);
                instance.GetComponent<Grid>().row = i;
                instance.GetComponent<Grid>().column = j;
                instance.GetComponent<RectTransform>().localPosition = GameManager.instance.gridPosition[i, j];
                _sandBoxGrids[i, j] = instance.GetComponent<Grid>();
            }
        }
    }
    private void Initialized()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("데이터에 저장된 스테이지 챕터 "+Managers.Data.stageData.stageChapter +" 현재 스테이지 챕터 "+StageManager.instance.currentStageIndex);
        if(Managers.Data.stageData.stageChapter == StageManager.instance.currentStageIndex)
        {
            player.transform.position = new Vector3(Managers.Data.playerData.playerXPos, Managers.Data.playerData.playerYPos, 0);
            curWaterReserves = (int)Managers.Data.playerData.playerWaterReserves;
            successGauge = (int)Managers.Data.playerData.goodGauge;
            deathCount = (int)Managers.Data.playerData.deathCount;
        }
        else //홈씬말고 게임씬에서 플레이 시작하는 경우 - 모두 기본 값, 기본 위치로 시작
        {
            player.transform.position = new Vector3(-20.0f, 0, 0);
            curWaterReserves = 5;
        }
        StageManager.instance.UpdateStageData();
        //스테이지 클리어시에만 작동
        if (Managers.Data.isClear)
        {
            Managers.Data.isClear = false;
            Managers.Data.ClearStageData(Managers.Data.stageData.stageChapter);
        }
        waterParticle.GetComponent<ParticleSystem>().Stop();
    }

    private void LateUpdate()
    {
        playTime += Time.deltaTime; 
        WaterReservesUI();
        if (curWaterReserves <= -1)
            StartCoroutine("OpenGameOver");
        else if (0 <= curWaterReserves && curWaterReserves <= 10)
        {
            if (player.GetComponent<SpriteRenderer>().color.a != 0.5f + (float)(curWaterReserves / 30.0f))
                player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (0.5f + (float)curWaterReserves / 30.0f));
        }
       else if (curWaterReserves > 10)
            if (player.GetComponent<SpriteRenderer>().color.a != 1)
                player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    private void WaterReservesUI()
    {
        if (curWaterReserves > 30)
            curWaterReservesTxt.text = (30).ToString();
        else
            curWaterReservesTxt.text = curWaterReserves.ToString();

        if(oldCurWaterReserves != curWaterReserves)
        {
            if(curWaterReserves <= 35)
                curWaterReservesImage.rectTransform.localPosition = new Vector2(0, firstWater + waterFull * curWaterReserves / maxWaterReserves);
            oldCurWaterReserves = curWaterReserves;
        }
    }

    public IEnumerator GameOver()
    {
        if(gameOver == false)
        {
            SoundManager.instance.SfxPlaySound2(2, transform.position);
            SoundManager.instance.SfxPlaySound(1, transform.position);
            gameOver = true;
            yield return new WaitForSecondsRealtime(3.0f);
            Managers.Data.SlotSaveData();
            Managers.Data.SlotLoadData(0);
            LoadingSceneController.Instance.LoadScene("GameScene" + (Managers.Data.stageData.stageChapter - 1).ToString());
        }
        yield return null;
    }
    public void OpenGameOver()
    {
        StartCoroutine(GameOver());
    }

    public int CurrentStageWaterConsume(int currentStage)
    {
        int waterConsume = 0;
        switch (currentStage)
        {
            case 1:
                waterConsume = 1;
                break;
            case 2:
                waterConsume = 2;
                break;
            case 3:
                waterConsume = 3;
                break;
            case 4:
                waterConsume = 4;
                break;
            default:
                break;
        }
        return waterConsume;
    }
    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }
}
