using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderChainController : MonoBehaviour
{
    //lineRenderer�������˵㣬��ʼ����յ��˳��û�й�ϵ
    public Vector3 startPos;
    public Vector3 endPos;
    //ThunderChainLevel������������ǿ�ȵȼ�����Ϊ0��1��2��3����0���ǿյġ�
    private int m_thunderChainLevel;
    public int ThunderChainLevel
    {
        get
        {
            return m_thunderChainLevel;
        }
        set
        {
            m_thunderChainLevel = value;
            updateHigh();
        }
    }


    [SerializeField]float[] high = new float[5];
    



    //û��B�õ�������
    float[] sinXModify = new float[5] { 1, 2, 3, 4, 5 };
    float resetTime = 0.3f;
    int vertexCount = 25;
    float sinModify = 40;
    int lineNumber;
    LineRenderer[] lineRendererList= new LineRenderer[5];
    Vector3[] posList = new Vector3[30];
    float xModify;
    float[] yModify = new float[30];
    Vector3[] vector3Modify = new Vector3[30];
    float resetPro;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0;i<transform.childCount;i++)
        {
            lineRendererList[i] = transform.GetChild(i).GetComponent<LineRenderer>();
            lineRendererList[i].positionCount = vertexCount + 2;
        }

        resetPro = 0;

        updateHigh();

    }

    // Update is called once per frame
    void Update()
    {

        //��һ�����ѭ����ÿ����ѭ��һ��
        for(int j = 0; j < lineNumber; j++)
        {

            //Ŀǰ��˼·�ǣ��ȴӿ�ʼλ�õ�����λ����һ��ֱ�ߣ�Ȼ����ֱ���Ͼ��ȵ�ȡ�����㡣�����һ������Ҫ�����ߣ��������������ϵ��yֵ��Ȼ���ԭ��ֱ���ϵĵ���з�����ļӼ�


            for (int i = 0; i < vertexCount; i++)
            {
                posList[i] = new Vector3((endPos.x - startPos.x) / (vertexCount + 1) * (i + 1) + startPos.x, (endPos.y - startPos.y) / (vertexCount + 1) * (i + 1) + startPos.y, 0);

            }

            float lineLength = Vector3.Distance(startPos, endPos);

            //��ȡһ������y=-��x-startPos)(x-endPos)
            for (int i = 0; i < vertexCount; i++)
            {
                xModify = (i + 1f) / (vertexCount + 1f) * lineLength;
                //xModify * (xModify - lineLength) +
                yModify[i] = Mathf.Sin(Time.timeSinceLevelLoad+xModify+sinXModify[j]*resetPro)*sinModify;
                //�Ȱ����y������ֵ��һ������ֱ���ߵķ���Ȼ������ϵĵ���мӺ͡�
                yModify[i] = Mathf.Sin(Time.timeSinceLevelLoad*5+xModify+sinXModify[j])*sinModify;
                posList[i] += new Vector3(endPos.y - startPos.y, startPos.x - endPos.x, 0).normalized * yModify[i]*high[j]*resetPro+new Vector3(Random.Range(0f,0.5f), Random.Range(0f, 0.5f),0);


            }


            lineRendererList[j].SetPosition(0, startPos);
            for (int i = 1; i < vertexCount+1; i++)
            {
                lineRendererList[j].SetPosition(i, posList[i - 1]);
            }
            lineRendererList[j].SetPosition(vertexCount + 1, endPos);
        }


        //ÿ��ѭ����ʱ��һ���͹�0
        resetPro += Time.deltaTime;
        if(resetPro > resetTime)
        {
            resetPro = 0;
        }


    }




    void updateHigh()
    {
        //����ѡ��ĵ�λ�������õ�linerenderer�����͸߶�
        switch (ThunderChainLevel)
        {
            case 0:
                {
                    lineNumber = 0;
                    break;
                }

            case 1:
                {
                    lineNumber = 2;
                    high = new float[5] { 0.07f, 0.02f, 0, 0, 0 };

                    break;
                }

            case 2:
                {
                    lineNumber = 3;
                    high = new float[5] { 0.1f, 0.04f, 0.08f, 0.01f, 0 };


                    break;
                }

            case 3:
                {
                    lineNumber = 5;
                    high = new float[5] { 0.14f, 0.06f, 0.1f, 0.03f, 0.01f };


                    break;
                }

        }


        for (int i = 0; i < 5; i++)
        {
            lineRendererList[i].enabled = true;
        }


        for (int i= lineNumber;i< 5; i++)
        {
            lineRendererList[i].enabled = false;
        }





    }
}
