using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_GrappleRope : MonoBehaviour
{
    [Header("General Refernces:")]
    public Tutorial_GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int percision = 40;//��ȷ�� linerender������� ����Խ�ྫȷ��Խ��
    [Range(0, 20)][SerializeField] private float straightenLineSpeed = 5;//��������ֱ�ߵ��ٶ�

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)][SerializeField] private float StartWaveSize = 2;//���˵Ĵ�С
    [HideInInspector]public float waveSize = 0;

    [Header("Rope Progression:")]//����ǰ��
    public AnimationCurve ropeProgressionCurve;
    [SerializeField][Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool strightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        isGrappling=false;
        m_lineRenderer.positionCount = percision;
        waveSize = StartWaveSize;
        strightLine = false;//Ŀǰ�Ƿ�ֱ��(�����ʱ�������ߣ�����ȥ��ʱ����ֱ��)
        LinePointsToFirePoint();//��ʼ�����еĵ�
        m_lineRenderer.enabled = true;//��lineRender
    }

    private void OnDisable()
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }
    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < percision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);//�����е㶼���õ�����㴦
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;//��ʱ�����ӵ�moveTime
        DrawRope();
    }

    void DrawRope()
    {
        if (!strightLine)//�����׶� ���������ȥ
        {
            //������һ�����Ѿ�����ץ���ĵ��ˣ�������ȥ
            //Debug.Log(Vector2.Distance(m_lineRenderer.GetPosition(percision), grapplingGun.grapplePoint));
            if (Vector2.Distance(m_lineRenderer.GetPosition(percision - 1),grapplingGun.grapplePoint)<1f)
            {
                strightLine = true;
            }
            else
            {
                DrawRopeWaves();//������ǻ������ߵĵط�
            }
        }
        else
        {
            //Debug.Log("catch");
            if (!isGrappling)//ץ��Ŀ��û
            {
                grapplingGun.Grapple();//��������������ǰ�Ŀ������ȥ
                isGrappling = true;
            }
            if (waveSize > 0)//���˷����𽥱�С
            {
                //Debug.Log(waveSize);
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                //Ȼ��Ϳ���ֱ�ӿ�ʼ��ֱ����
                waveSize = 0;

                if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    /// <summary>
    /// DrawRopeWaves�����ص㣬��λ���һ������
    /// </summary>
    void DrawRopeWaves()
    {
        for (int i = 0; i < percision; i++)//����ÿ���㻭��
        {
            float delta = (float)i / ((float)percision - 1f);
            //ǰ��һ���Ƿ���������˼�����������Ĵ�ֱ����������ų�����*��Ӧ��ֵ����tm�Ǹ���ֵ
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            //�����������������Ͼ��ȷֲ�����˼��
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            //���ӵ�ǰ���ٶ� ����һ���ٶȽ��еĹ���
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);
            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }
    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}
