using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISaveSlot : MonoBehaviour, ISelectHandler
{
    public UISaveView uiSaveView;
    public int slot;

    public Image image;
    public Text Name;
    public Text time;
    public Text allTime;

    // Start is called before the first frame update

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {


	}  
    
    public void Init()
    {
        image.gameObject.SetActive(true);

		Name.text = "���ս���";
        time.text = "����ʱ��";
        time.gameObject.SetActive(true);
        allTime.text = " ����ʱ��";
        allTime.gameObject.SetActive(true);
    }

	public void OnSelect(BaseEventData eventData)
	{
		//uiSaveView.index = slot;
	}
}
