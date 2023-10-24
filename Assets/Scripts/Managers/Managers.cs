using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private static Managers Instance { get { Init(); return s_instance; } }

    private DataManager _data = new DataManager();
    private InputManager _input = new InputManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEX _scene = new SceneManagerEX();
    private SoundManager _sound = new SoundManager();
    private UIManager _ui = new UIManager();
    private MoneyManager _money = new MoneyManager(); // 배경택_231013
    private GameManager _gameManager = new GameManager(); // 배경택 _231018
    private CameraManager _cameraManager = new CameraManager(); // 배경택_231023

    public static DataManager Data => Instance._data;
    public static InputManager Input => Instance._input;
    public static PoolManager Pool => Instance._pool;
    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEX Scene => Instance._scene;
    public static SoundManager Sound => Instance._sound;
    public static UIManager UI => Instance._ui;
    public static MoneyManager MONEY => Instance._money; // 배경택_231013
    public static GameManager GameManager => Instance._gameManager; // 배경택_231018
    public static CameraManager Camera => Instance._cameraManager; // 배경택_231023

    private void Start()
    {
        Init();
    }

    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();

            //s_instance._data.Init();
            //s_instance._pool.Init();
            //s_instance._sound.Init();
            
            // 게임매니저 init()
            s_instance._data.Init();        // 김민섭_231019
            s_instance._gameManager.Init(); //배경택_231018
            s_instance._sound.Init();       // 김민섭_231019
            s_instance._cameraManager.Init();
        }
    }

    private void Update()
    {
        Camera.OnUpdate();   
    }

    public static void Clear()
    {
        //Input.Clear();
        //Sound.Clear();
        //Scene.Clear();
        //UI.Clear();
        //Pool.Clear();
    }
}
