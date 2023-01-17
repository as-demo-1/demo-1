using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_Chase_State :EnemyFSMBaseState
{
    public float chaseSpeed;
    public bool isFaceWithSpeed;
    public bool isFlying = false;
    public bool lock_x_move = false;
    public bool lock_y_move = false;
    public bool isMoveWithCurve=false;
    public float curveCycle;
    public AnimationCurve curve;
    private Vector3 v;
    public override void InitState(EnemyFSMManager enemyFSM)
    {
        base.InitState(enemyFSM);
        
    }
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        Debug.Log("进入追击模式");
        base.EnterState(enemyFSM);
        if (isFlying)
        {
            enemyFSM.rigidbody2d.gravityScale = 0;
        }
    }
    public override void Act_State(EnemyFSMManager fSM_Manager)
    {
        Debug.Log("执行");

        

        v = fSM_Manager.getTargetDir(true);
        v=v.normalized;
        if (!isFlying)
        {
            if (v.x > 0)
                v = new Vector3(1, 0, 0);
            else
                v = new Vector3(-1, 0, 0);
        }
        if (lock_x_move)
            v.x = 0;
        if (lock_y_move)
            v.y = 0;
        if (isMoveWithCurve)
        {
            var vv = Vector3.Lerp(Vector3.zero, v , curve.Evaluate((Time.time / (curveCycle + 0.000001f)) % 1.0f));
            fSM_Manager.rigidbody2d.velocity = chaseSpeed * vv;
        }
        else
            fSM_Manager.rigidbody2d.velocity = chaseSpeed*v;
        if (isFaceWithSpeed)
            fSM_Manager.faceWithSpeed();
        
        Debug.Log("敌人当前速度："+v);
    }
}
