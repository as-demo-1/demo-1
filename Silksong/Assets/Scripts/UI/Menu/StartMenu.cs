using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    public static StartMenu Instance { get; private set; }
    

    [Header("Screens")]
    [SerializeField] CanvasGroup InvitationScreen;
    [SerializeField] CanvasGroup MainScreen;
    [SerializeField] CanvasGroup SaveScreen;
    [SerializeField] CanvasGroup ConfigScreen;
    [SerializeField] CanvasGroup ExitConfirmScreen;

    /// <summary>
    /// �������н����List
    /// </summary>
    List<CanvasGroup> ScreenList;

    [SerializeField] CanvasGroup CurrentScreen;

    [Header("Press Any Key To Continue")]
    [SerializeField] CanvasGroup PressAnyKey;

    [Tooltip("Adjust the blink speed of \"Press Any Key To Continue\"")]
    [Range(0.0f, 10.0f)]
    [SerializeField] float BreathSpeed = 1f;


    [Header("Sound Effects")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioClip SelectSoundEffect;
    [SerializeField] AudioClip ClickSoundEffect;

    private AudioSource SEAudioSource;


    [Header("Config Resolution")]
    [SerializeField] Dropdown resolutionDropdown;
    Resolution[] availableResolutions;
    private Dictionary<string, string> KeyConfigDictionary = new Dictionary<string, string>()
    {
        { "��", "A" },
        { "��", "D" },
        { "��", "S" },
        { "��Ծ", "Space" },
        { "���", "Left Shift" },
        { "����", "J" },
        { "����", "L" },
        { "����", "Q" },
    };

    [Header("Config KeyBinding")]
    [SerializeField] GameObject KeyBindingPanel;
    [SerializeField] GameObject KeyBindingPanelContainer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ScreenList = new List<CanvasGroup>() { InvitationScreen, MainScreen, SaveScreen, ConfigScreen, ExitConfirmScreen };
        SetActiveAllScreen();
        OpenScreen(InvitationScreen); // �ر��������н��棬��Invitation����

        KeyBindingPanel.SetActive(false);

        UpdateAvailableResolutions();
    }

    void Update()
    {
        // ������ڴ���Invitation���棬���á������������������ʾ������˸
        if (CurrentScreen == InvitationScreen)
        {
            if (BreathSpeed == 0) // �����˸�ٶȣ�BreathSpeed��Ϊ0����������˸
            {
                PressAnyKey.alpha = 1;
            }
            else
            {
                PressAnyKey.alpha = Mathf.Clamp01((Mathf.Sin(Time.time * BreathSpeed) + 1) * 0.52f);
            }
            if (Input.anyKey)
            {
                // ������ڴ���Invitation���棬��������������ⰴ�����ͽ���������
                OpenScreen(MainScreen);
            }
        }

        if (CurrentScreen == ExitConfirmScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) /*|| ( some random controller key)*/)
            {
                DisableScreen(ExitConfirmScreen, true);
            }
        }
        if (CurrentScreen != InvitationScreen && CurrentScreen != MainScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) /*|| ( some random controller key)*/)
            {
                ReturnMainScreen();
            }
        }
    }

    #region �������水ť���õ�Function

    /// <summary>
    /// ��ʼ����Ϸ������ť��OnClick����
    /// </summary>
    public void NewGame()
    {

    }

    /// <summary>
    /// �򿪴浵���棬�ر���������
    /// </summary>
    public void LoadGame()
    {
        OpenScreen(SaveScreen);
        //EnableScreen(SaveScreen, true);
    }

    /// <summary>
    /// ��ѡ����棬�ر���������
    /// </summary>
    public void OpenConfigScreen()
    {
        OpenScreen(ConfigScreen);
        //EnableScreen(ConfigScreen, true);
    }

    /// <summary>
    /// �ص������棬�ر���������
    /// </summary>
    public void ReturnMainScreen()
    {
        OpenScreen(MainScreen);
        //DisableScreen(SaveScreen, true);
    }

    /// <summary>
    /// ���˳�ȷ�ϵĴ��ڣ����ر���������
    /// </summary>
    public void OpenExitConfirmScreen()
    {
        //OpenScreen(ExitConfirmScreen);
        EnableScreen(ExitConfirmScreen, true);
    }
    /// <summary>
    /// �ر��˳�ȷ�ϵĴ���
    /// </summary>
    public void CloseExitConfirmScreen()
    {
        DisableScreen(ExitConfirmScreen, true);

        CurrentScreen = MainScreen;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    #endregion

    #region ��Ч

    /// <summary>
    /// ���ѡ��ťʱ������Ч
    /// </summary>
    public void PlaySelectSoundEffect()
    {
        //Debug.Log("on select");
        if (SelectSoundEffect != null)
        {
            SEAudioSource.clip = SelectSoundEffect;
            SEAudioSource.Play();
        }

    }

    /// <summary>
    /// ��ҵ����ťʱ������Ч
    /// </summary>
    public void PlayClickSoundEffect()
    {
        //Debug.Log("on click");
        if (SelectSoundEffect != null)
        {
            SEAudioSource.clip = ClickSoundEffect;
            SEAudioSource.Play();
        }

    }

    /// <summary>
    /// ���ڵ����������������Slider����
    /// </summary>
    /// <param name="volume">��Slider���ܵ���ֵ�����ֵΪ0����СֵΪ-80��Ϊ�˷���Audio Mixer��</param>
    public void SetMasterVolume(float volume)
    {
        //audioMixer.SetFloat("MasterVolume", volume);
    }

    #endregion

    #region ����

    private void UpdateAvailableResolutions()
    {
        availableResolutions = Screen.resolutions; // ��ȡ���õķֱ���

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " * " + availableResolutions[i].height;
            options.Add(option);

            if (Screen.currentResolution.width == availableResolutions[i].width && Screen.currentResolution.height == availableResolutions[i].height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetFullScreen(true);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    /// <summary>
    /// �����Զ����λ�Ľ���
    /// </summary>
    /// <param name="toggle">Toggle�������Ĳ���</param>
    public void SetKeyBinding(bool toggle)
    {
        if (toggle)
        {
            UpdateKeyConfig();
        }

        KeyBindingPanel.SetActive(toggle);
    }

    /// <summary>
    /// ���ݼ�λ�ֵ䣬���»��Ƽ�λ��
    /// </summary>
    private void UpdateKeyConfig()
    {
        // make sure the prefab is active, but Key Item is inactive
        KeyBindingPanelContainer.transform.Find("Key Item").gameObject.SetActive(true);
        GameObject prefab = KeyBindingPanelContainer.transform.Find("Key Item").gameObject;
        KeyBindingPanelContainer.transform.Find("Key Item").gameObject.SetActive(false);

        // destory all children
        foreach (Transform child in KeyBindingPanelContainer.transform)
        {
            if (child.name == "Key Item")
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach (string key in KeyConfigDictionary.Keys)
        {
            GameObject keyItem = Instantiate(prefab, KeyBindingPanelContainer.transform);
            keyItem.SetActive(true);
            keyItem.transform.Find("Text").GetComponent<Text>().text = key;
            keyItem.transform.Find("Button").GetComponentInChildren<Text>().text = KeyConfigDictionary[key];
        }
    }


    #endregion

    #region Helper Functions

    private void OpenScreen(CanvasGroup canvasGroup, bool withAnimation = false)
    {
        foreach (CanvasGroup screen in ScreenList)
        {
            if (screen == canvasGroup)
            {
                EnableScreen(screen, withAnimation);
            }
            else if (screen.alpha == 1 && screen.interactable == true && screen.blocksRaycasts == true)
            {
                DisableScreen(screen, withAnimation);
            }
        }
        CurrentScreen = canvasGroup;
    }
    private void EnableScreen(CanvasGroup canvasGroup, bool withAnimation)
    {
        if (withAnimation)
        {
            // transition animation to be added
        }
        //canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        CurrentScreen = canvasGroup;
    }
    private void DisableScreen(CanvasGroup canvasGroup, bool withAnimation)
    {
        if (withAnimation)
        {
            // transition animation to be added
        }
        //canvasGroup.gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void SetActiveAllScreen()
    {
        foreach (CanvasGroup screen in ScreenList)
        {
            screen.gameObject.SetActive(true);
        }
    }

    #endregion
}