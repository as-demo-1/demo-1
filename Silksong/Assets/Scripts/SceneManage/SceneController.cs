using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �������л� ����
/// </summary>
public class SceneController : MonoBehaviour
{
    public static SceneController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<SceneController>();

            if (instance != null)
                return instance;

            GameObject sceneControllerGameObject = new GameObject("SceneManager");
            instance = sceneControllerGameObject.AddComponent<SceneController>();

            return instance;
        }
    }//����

    protected static SceneController instance;

    public bool m_Transitioning;
    private string transitioningScene;

    private Dictionary<string, AsyncOperation> preLoads=new Dictionary<string, AsyncOperation>();

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }


    /// <summary>
    /// ����Ϸ�������ڵ���һ������
    /// </summary>
    /// <param name="transitionPoint"></param>
    public static void TransitionToScene(SceneTransitionPoint transitionPoint,bool withPreLoad=false)
    {
        CameraController.Instance.BeforeChangeScene();

        if(withPreLoad==false)
        Instance.StartCoroutine(Instance.Transition(transitionPoint.newSceneName, transitionPoint, transitionPoint.resetInputValuesOnTransition));
        else Instance.StartCoroutine(Instance.TransitionWithPreLoad(transitionPoint.newSceneName, transitionPoint, transitionPoint.resetInputValuesOnTransition));
    }

     public static void TransitionToScene(string SceneName)//�Ӳ˵�����Ϸ������ �ݲ���
     {
         Instance.StartCoroutine(Instance.Transition(SceneName));
     }
    private void Update()
    {
        // print(PlayerAnimatorParamsMapping.HaveControl());
       // print(preLoads.Count);
    }

    public void PreLoadScenes()
    {
        preLoads.Clear();
        SceneTransitionPoint[] points = FindObjectsOfType<SceneTransitionPoint>();
        foreach(var p in points)
        {
          StartCoroutine (preLoadIE(p.newSceneName, preLoads));
        }
    }


    protected IEnumerator Transition(string newSceneName, SceneTransitionPoint destination, bool resetInputValues)
    {
        if (m_Transitioning && newSceneName==transitioningScene)
        {
            print("transitioning to the same scene");
            yield break;
        }
        m_Transitioning = true;
        transitioningScene = newSceneName;
        //  print("to true");
        PlayerAnimatorParamsMapping.SetControl(false);
        stopPlayer();

        yield return StartCoroutine(ScreenFader.Instance.FadeSceneOut(ScreenFader.SceneFadeOutTime));

        AsyncOperation ao= SceneManager.LoadSceneAsync(newSceneName);
        //ao.allowSceneActivation = false;

        yield return ao;

        GameObjectTeleporter.Instance.playerEnterSceneFromTransitionPoint(destination);//��ҵ�������� 
        setPlayerAction();
        yield return StartCoroutine(ScreenFader.Instance.FadeSceneIn(ScreenFader.SceneFadeInTime));

        PlayerAnimatorParamsMapping.SetControl(true);
        m_Transitioning = false;
        //print("to false");
    }

    protected IEnumerator Transition(string newSceneName)
    {
        if (m_Transitioning && newSceneName==transitioningScene)
        {
            print("transitioning to the same scene");
            yield break;
        }
        m_Transitioning = true;
        transitioningScene = newSceneName;
        //  print("to true");
        PlayerAnimatorParamsMapping.SetControl(false);
        stopPlayer();

        yield return StartCoroutine(ScreenFader.Instance.FadeSceneOut(ScreenFader.SceneFadeOutTime));

        AsyncOperation ao= SceneManager.LoadSceneAsync(newSceneName);
        ao.allowSceneActivation = false;

        yield return ao;

        GameObjectTeleporter.Instance.playerEnterSceneEntance(SceneEntrance.EntranceTag.A, Vector3.zero);//��ҵ�������� 
        setPlayerAction();
        yield return StartCoroutine(ScreenFader.Instance.FadeSceneIn(ScreenFader.SceneFadeInTime));

        PlayerAnimatorParamsMapping.SetControl(true);
        m_Transitioning = false;
        
    }

    private void stopPlayer()
    {
  
       /*if (PlayerController.Instance.playerAnimatorStatesControl.CurrentPlayerState==EPlayerState.Jump)
        {
            PlayerController.Instance.setRigidVelocity(Vector2.zero);
        }*/

    }

    private void setPlayerAction()
    {
        
        PlayerController playerController = PlayerController.Instance;

        if (playerController.playerAnimatorStatesControl.CurrentPlayerState == EPlayerState.Plunge) return;

        playerController.setRigidVelocity(Vector2.zero);
        if (playerController.playerToCat.IsCat)
        {
            playerController.PlayerAnimator.Play("Cat_Idle");
        }
        else
        {
            if(playerController.IsUnderWater)
            {
                playerController.PlayerAnimator.Play("WaterIdle");
            }
            else 
            {
                playerController.PlayerAnimator.Play("Idle");
            }
        }
        
    }


    protected IEnumerator TransitionWithPreLoad(string newSceneName, SceneTransitionPoint destination, bool resetInputValues)
    {
        if (m_Transitioning && newSceneName == transitioningScene)
        {
            print("transitioning to the same scene");
            yield break;
        }
        m_Transitioning = true;
        transitioningScene = newSceneName;
        //  print("to true");
        PlayerAnimatorParamsMapping.SetControl(false);
        stopPlayer();

        yield return StartCoroutine(ScreenFader.Instance.FadeSceneOut(ScreenFader.SceneFadeOutTime));



        if (preLoads.ContainsKey(newSceneName))
        {
            Debug.Log("load pre");
            foreach (var x in preLoads)
            {
                Debug.Log(x.Key+ " "+x.Value.progress);
            }
            preLoads[newSceneName].allowSceneActivation = true;

            while(preLoads[newSceneName].isDone==false)
            {
                Debug.Log(preLoads[newSceneName].progress);
                yield return null;

            }
            yield return preLoads[newSceneName];
        }
        else
        {
            Debug.LogError("no this sceneName");
        }
        Debug.Log("load over");

        GameObjectTeleporter.Instance.playerEnterSceneFromTransitionPoint(destination);//��ҵ�������� 
        setPlayerAction();
        yield return StartCoroutine(ScreenFader.Instance.FadeSceneIn(ScreenFader.SceneFadeInTime));

        PlayerAnimatorParamsMapping.SetControl(true);
        m_Transitioning = false;
        //print("to false");
    }

    protected IEnumerator preLoadIE(string newScene,Dictionary<string, AsyncOperation> dic)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        AsyncOperation ao = SceneManager.LoadSceneAsync(newScene);
        ao.allowSceneActivation = false;
        dic.Add(newScene, ao);
       // yield return ao;
    }
}
