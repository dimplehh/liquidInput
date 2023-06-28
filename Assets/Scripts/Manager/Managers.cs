using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { init(); return s_instance; } }
    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    DataManager _data = new DataManager();
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data { get { return Instance._data; } }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    static void init()//int함수 시행 되는거..?
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
