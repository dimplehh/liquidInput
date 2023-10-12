using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables; //�ƽ�
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
    //------------------------------------------------------
    [SerializeField]
    public GameObject waterParticle;
    float firstWater;
    [SerializeField]
    GameObject waterSlider;
    [SerializeField]
    GameObject settingPanel;
    [SerializeField]
    GameObject gameOverPanel;
    public static GameManager instance;
    //�� ������
    public int maxWaterReserves = 50;
    public int curWaterReserves;
    public int oldCurWaterReserves;
    public Image curWaterReservesImage;
    public Text curWaterReservesTxt;
    int waterFull = 800;
    public GameObject player;

    //���������
    public float successGauge = 0;
    //�÷��� Ÿ��
    public float playTime = 0;

    public bool isPlay = false;
    public bool isNonAutoSave = false; //�޴�â������ �ε常 �ǰ� ���̺��������� ���常�ǰ�
    
    
    public GameObject SaveAndLoadPanel; //�ҷ�����â
    public LoadSlotSelect loadSlotSelect; //�ӽ�

    //����Ʈ Ǯ
    public EffectsPool effectsPool;

    //�ƽ�
    public PlayableDirector playableDirector;
    public void IsNonAutoSave() 
    {
        isNonAutoSave = false;
    }
    public void HomeButton()
    {
        LoadingSceneController.Instance.LoadScene("HomeScene");
    }
    public void SettingButton()
    {
        if (!settingPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                settingPanel.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                settingPanel.SetActive(false);
            }
        }
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
        SoundManager.instance.BgmPlaySound(1);
        if(Managers.Data.playerData.isFirst == true)
        {
            StartCoroutine(CutScene()); //�ƽ����� �÷��̰���
        }
        else
        {
            PlayGame();
        }
        
        StageManager.instance.FindStageObjects();
        Initialized();
        //CreateGrid(); 
        setWaterSlider();

    }
    private IEnumerator CutScene()
    {
        playableDirector.Play();
        yield return new WaitForSeconds(5.5f);
        PlayGame();
    }
    
    private void setWaterSlider()
    {
        firstWater = curWaterReservesImage.rectTransform.localPosition.y;
        curWaterReservesImage.rectTransform.localPosition = new Vector2(0, firstWater + waterFull * curWaterReserves / maxWaterReserves);
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
        player.transform.position = new Vector3(Managers.Data.playerData.playerXPos, Managers.Data.playerData.playerYPos, 0);
        StageManager.instance.UpdateStageData();
        curWaterReserves = (int)Managers.Data.playerData.playerWaterReserves;
        waterParticle.GetComponent<ParticleSystem>().Stop();
        
    }

    private void LateUpdate()
    {
        //�÷��� Ÿ�� ����
        playTime += Time.deltaTime; 
        pushZ();
        WaterReservesUI();
        if (curWaterReserves <= -2)
            StartCoroutine("OpenGameOver");
        //escŰ ������ �� ���� Ȱ��ȭ
        SettingButton();
    }
    private void pushZ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(settingPanel.activeSelf == false)
                settingPanel.SetActive(true);
            else
                settingPanel.SetActive(false);
        }
    }

    private void WaterReservesUI()
    {
        curWaterReservesTxt.text = curWaterReserves.ToString();

        if(oldCurWaterReserves != curWaterReserves)
        {
            curWaterReservesImage.rectTransform.localPosition = new Vector2(0, firstWater + waterFull * curWaterReserves / maxWaterReserves);
            oldCurWaterReserves = curWaterReserves;
        }
    }

    public IEnumerator GameOver()
    {
        SoundManager.instance.SfxPlaySound2(2, transform.position);
        SoundManager.instance.SfxPlaySound(1, transform.position);
        yield return new WaitForSecondsRealtime(3.0f);
        gameOver = true;
        Managers.Data.SlotLoadData(0);
        LoadingSceneController.Instance.LoadScene("GameScene");
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
                waterConsume = 4;
                break;
            case 4:
                waterConsume = 5;
                break;
            default:
                break;
        }
        return waterConsume;
    } //���������� ���� ����� �Ҹ�
    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }
}
