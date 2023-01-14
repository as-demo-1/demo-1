using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameManager>();

            if (instance != null)
                return instance;

            GameObject sceneControllerGameObject = new GameObject("GameManager");
            instance = sceneControllerGameObject.AddComponent<GameManager>();

            return instance;
        }
    }//单例

    protected static GameManager instance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject mCamera;

    public AudioManager audioManager;

    public GameObject gamingUI;

    public GameObject mapPack;

    public GameObject eventSystem;

    public SaveSystem saveSystem;


    void Awake()
    {

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        GameInitialize();

        //以下代码代表玩家从菜单进入游戏场景的初始化，临时使用
        CreateCamera();

        // 临时初始化UI
        UIManager.Instance.ShowGameUI();

        creatPlayer();
        GameObjectTeleporter.Instance.playerEnterSceneEntance(SceneEntrance.EntranceTag.A,Vector3.zero);

        eventSystem = Instantiate(eventSystem);
        DontDestroyOnLoad(eventSystem);
        uint bankid;
        AkSoundEngine.LoadBank("General",out bankid);

    }

    /// <summary>
    /// 进入游戏场景时生成玩家
    /// </summary>
    public void creatPlayer()
    {
        player = Instantiate(player.gameObject, transform.position, Quaternion.identity);
    }

    public void CreateCamera()
    {
        GameObject tempCam = GameObject.Find("TempCamera");
        if (tempCam != null)
        {
            GameObject.Destroy(tempCam);
        }
        GameObject cam = Instantiate(mCamera.gameObject);
        cam.name = "CameraPack";
        DontDestroyOnLoad(cam);
    }

    public void GameInitialize()
    {
        Application.targetFrameRate = 120;
        audioManager = Instantiate(audioManager);
    }
}