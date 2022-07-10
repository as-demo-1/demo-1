using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    //���˵�
    public Button[] mainButtons;

    //�����˵����
    public GameObject[] SecondPanel;

    //��ͼ
    public Map map;
    //װ��
    public Equip equip;
    //����
    public Talisman talisman;
    //ͼ��
    public Picture picture;
    //�ɾ�
    public Achievement achievement;
    //ѡ��
    public Options options;


    //��ǰѡ�����˵�����
    private int mainButtonIndex = 0;

    //��ǰѡ���Ӳ˵�����
    private int childButtonIndex = 0;

    //�Ƿ�Ϊһ���˵�
    private bool childPanelExpanded = true;

    //�Ƿ�Ϊ�����˵�
    private bool isSecond = false;

    //�Ƿ�Ϊ�����˵�
    private bool isThird = false;

    void Start()
    {
        //���˵��¼�
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].onClick.AddListener(OnMainButtonClick);
        }

        if (mainButtons.Length > 0)
        {
            mainButtons[mainButtonIndex].Select();
        }
    }

    void Update()
    {

        //�������������'1'
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (childPanelExpanded)//��һ���˵�
            {
                EnterSecond();
                 childPanelExpanded = false;
                isSecond = true;
            }
            else//�Ƕ����˵�
            {
                EnterThrid();
                isSecond = false;
                isThird = true;
            }
        }

        //�л�����������'2'
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (childPanelExpanded)//��һ���˵�
            {
                mainButtonIndex++;
                if (mainButtonIndex > mainButtons.Length - 1)
                {
                    mainButtonIndex = 0;
                }
                mainButtons[mainButtonIndex].Select();
                DisplayChildPanel(mainButtonIndex);

            }
            else if (isSecond)//�Ƕ����˵�
            {
                SecondButton();
                /*childButtonIndex++;
                childButtonIndex = childButtonIndex > childButtonGroup[mainButtonIndex].Count - 1 ? 0 : childButtonIndex;
                childButtonGroup[mainButtonIndex][childButtonIndex].Select();
                if (mainButtonIndex == 1)
                {
                    DisplayChild2Panel(childButtonIndex);
                }*/
            }
            else
            {
                ThirdButton();
            }

        }

        //���أ���������'3'
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))//getReal3dInputs.ResetButtonDown
        {
            if (isSecond)
            {
                childButtonIndex = 0;
                mainButtons[mainButtonIndex].Select();
                if(mainButtonIndex == 4)
                {
                    achievement.ScrollTarget.verticalNormalizedPosition = 1;
                    achievement.index = 0;
                }
                isSecond = false;
                childPanelExpanded = true;
            }
            else if (isThird)
            {
                ReSecond();
                isSecond = true;
                isThird = false;
            }
            //CloseChildPanel();
        }
    }

    //���˵�����¼�����
    public void OnMainButtonClick()
    {
        childButtonIndex = 0;
        EnterSecond();
    }

    //��ʾĳ���Ӳ˵����
    void DisplayChildPanel(int index)
    {
        for (int i = 0; i < SecondPanel.Length; i++)
        {
            if (i == index)
            {
                if (!SecondPanel[i].activeSelf)
                {
                    SecondPanel[i].SetActive(true);
                }
            }
            else
            {
                if (SecondPanel[i].activeSelf)
                    SecondPanel[i].SetActive(false);
            }
        }
    }

    //��ʾ�ر����в˵����
    void CloseChildPanel()
    {
        childPanelExpanded = false;
        for (int i = 0; i < SecondPanel.Length; i++)
        {
            if (SecondPanel[i].activeSelf)
                SecondPanel[i].SetActive(false);
        }
    }


    public void EnterSecond()
    {
        switch (mainButtonIndex)
        {
            case 0:
                map.btns[childButtonIndex].Select();
                break;
            case 1:
                equip.btns[childButtonIndex].Select();
                break;
            case 2:
                talisman.btns[childButtonIndex].Select();
                break;
            case 3:
                picture.btns[childButtonIndex].Select();
                break;
            case 4:
                achievement.btns[childButtonIndex].Select();
                break;
            case 5:
                options.btns[childButtonIndex].Select();
                break;
        }
    }
    public void EnterThrid()
    {
        switch (mainButtonIndex)
        {
            case 1:
                {
                    switch (childButtonIndex)
                    {
                        case 0:
                            equip.fragment.btns[0].Select();
                            break;
                    }
                }
                break;
            case 3:
                {
                    switch (childButtonIndex)
                    {
                        case 0:
                            picture.minsterPicture.btns[0].Select();
                            break;
                    }
                    
                }
                break;
        }
    }
    public void SecondButton()
    {
        switch (mainButtonIndex)
        {
            case 0:
                {
                    childButtonIndex++;
                    childButtonIndex = childButtonIndex > map.btns.Count - 1 ? 0 : childButtonIndex;
                    map.btns[childButtonIndex].Select();
                }
                break;
            case 1:
                {
                    childButtonIndex++;
                    childButtonIndex = childButtonIndex > equip.btns.Count - 1 ? 0 : childButtonIndex;
                    equip.btns[childButtonIndex].Select();
                    equip.ChildPanel(childButtonIndex);
                }
                break;
            case 2:
                {
                    childButtonIndex++;
                    childButtonIndex = childButtonIndex > talisman.btns.Count - 1 ? 0 : childButtonIndex;
                    talisman.btns[childButtonIndex].Select();
                }
                break;
            case 3:
                {
                    childButtonIndex++;
                    childButtonIndex = childButtonIndex > picture.btns.Count - 1 ? 0 : childButtonIndex;
                    picture.btns[childButtonIndex].Select();
                }
                break;
            case 4:
                {
                    childButtonIndex++;
                    childButtonIndex = childButtonIndex > achievement.btns.Count - 1 ? 0 : childButtonIndex;
                    achievement.btns[childButtonIndex].Select();
                    achievement.MoveDown();
                    
                }
                break;
            case 5:
                {
                    childButtonIndex++;
                    childButtonIndex = childButtonIndex > options.btns.Count - 1 ? 0 : childButtonIndex;
                    options.btns[childButtonIndex].Select();
                }
                break;
        }
    }

    public void ThirdButton()
    {
        switch (mainButtonIndex)
        {

            case 1:
                {
                    equip.ThirdButton(childButtonIndex);
                    
                }
                break;

            case 3:
                {
                    picture.ThirdButton(childButtonIndex);
                }
                break;
        }
    }

    public void ReSecond()
    {
        switch (mainButtonIndex)
        {
            case 0:
                {
                    map.btns[childButtonIndex].Select();
                }
                break;
            case 1:
                {
                    equip.btns[childButtonIndex].Select();
                    equip.secondIndex = 0;
                }
                break;
            case 2:
                {
                    talisman.btns[childButtonIndex].Select();
                }
                break;
            case 3:
                {
                    picture.btns[childButtonIndex].Select();
                    picture.secondIndex = 0;
                    picture.minsterPicture.ScrollTarget.verticalNormalizedPosition = 1;
                }
                break;
            case 4:
                {
                    achievement.btns[childButtonIndex].Select();
                }
                break;
            case 5:
                {
                    options.btns[childButtonIndex].Select();
                }
                break;
        }
    }

}

