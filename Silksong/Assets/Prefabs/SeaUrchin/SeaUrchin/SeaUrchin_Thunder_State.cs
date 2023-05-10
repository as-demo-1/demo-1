using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ȼ��󺣵��ڶ��׶ηŵ��ʱ��ÿ��x���������������
/// ���׶�������Զ�ε����Ƶ�ʺ��������Ƶ����ͬ
/// </summary>
public class SeaUrchin_Thunder_State : EnemyFSMBaseState
{
    SeaUrchin seaUrchin;
    public string prickState;
    public override void InitState(EnemyFSMManager enemyFSM)
    {
        base.InitState(enemyFSM);
        seaUrchin=enemyFSM as SeaUrchin;
    }
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        base.EnterState(enemyFSM);
        if (seaUrchin.ifInWater)
        {
            foreach (var prick in seaUrchin.pricks)
            {
                prick.ChangeState(prickState);
            }
        }
    }
    public override void ExitState(EnemyFSMManager enemyFSM)
    {
        base.ExitState(enemyFSM);
    }
}
