using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 按键激活面板
/// 测试护符面板用
/// </summary>
public class TestSwitch : MonoBehaviour
{
    public KeyCode keyCode;

    public GameObject panel;

    private bool state;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        panel.GetComponent<CharmUIPanel>()?.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            state = !state;
            panel.SetActive(state);
        }
    }
}
