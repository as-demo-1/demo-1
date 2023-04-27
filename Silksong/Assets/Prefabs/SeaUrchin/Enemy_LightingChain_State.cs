using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ҵ��뷨�ǣ������굽��Զλ�ã�Ȼ��ų�����,Ȼ��ʼ��ת
/// </summary>
public class Enemy_LightingChain_State : EnemyFSMBaseState
{
    [LabelText("������뾶")]
    public float radius;
    [LabelText("����Ԥ����")]
    public GameObject lightningChain_Pre;
    [LabelText("������")]
    [InfoBox("���̫�̵Ļ������ܱ�һֱ��")]
    public float lightningInterval;
    [LabelText("�����ٶ�")]
    public float moveSpeed;
    [LabelText("��ת�ٶ�")]
    public float rotateSpeed;
    public LayerMask player;
    GameObject lightningChain;
    Transform parent;
    bool ifLightning;
    float t;
    public override void InitState(EnemyFSMManager enemyFSM)
    {
        base.InitState(enemyFSM);
        lightningChain=GameObject.Instantiate(lightningChain_Pre,enemyFSM.transform);
        lightningChain.transform.localPosition = Vector3.zero;
        lightningChain.transform.up = enemyFSM.transform.up;
        parent = enemyFSM.transform.parent;
        lightningChain.SetActive(false);
    }
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        base.EnterState(enemyFSM);
        enemyFSM.transform.up =    enemyFSM.transform.position- parent.transform.position;
        lightningChain.transform.up = enemyFSM.transform.up;
        ifLightning = false;
    }
    public override void FixAct_State(EnemyFSMManager enemyFSM)
    {
        base.FixAct_State(enemyFSM);
        //Debug.Log(Vector2.Distance(enemyFSM.transform.position, parent.position));
        if (Vector2.Distance(enemyFSM.transform.position, parent.position) < radius)
        {
            enemyFSM.rigidbody2d.velocity = enemyFSM.transform.up * moveSpeed;
        }
        else
        {
            enemyFSM.rigidbody2d.velocity = enemyFSM.transform.right * moveSpeed;
            enemyFSM.transform.up = enemyFSM.transform.position - parent.transform.position;
            lightningChain.transform.up = enemyFSM.transform.up;
            lightningChain.SetActive(true);
        }//
        if(t<lightningInterval)
            t+=Time.fixedDeltaTime;
        else 
        {
            Vector2 dir = parent.transform.position - enemyFSM.transform.position;
            RaycastHit2D hit2d = Physics2D.Raycast(enemyFSM.transform.position, dir, 100, player);
            if (hit2d.collider != null)
            {
                t = 0;
            }
        }
    }
}
