using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;
/// <summary>
/// ״̬���ж�Trigger�����á�����дInitTrigger��IsTriggerReach������
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
[Serializable]
public  class FSMBaseTrigger<T1,T2>
{
#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public T2 triggerType;
    public string targetState;
    public FSMBaseTrigger()
    {
    }

    public virtual void InitTrigger(FSMManager<T1,T2> fSMManager) { }
#region Update 
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,��Update����ѯ
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInUpdate(FSMManager<T1,T2> fsm_Manager) { return false; }

    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,��FixUpdate����ѯ
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInFixUpdate(FSMManager<T1, T2> fsm_Manager) { return false; }
#endregion
#region collider2D Event
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnTriggerEnterʱ���á�
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerEnter(FSMManager<T1, T2> fsm_Manager,Collider2D collision) { return false; }

    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnTriggerStayʱ���á�
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerStay(FSMManager<T1, T2> fsm_Manager,Collider2D collision) { return false; }

    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnTriggerExitʱ���á�
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerExit(FSMManager<T1, T2> fsm_Manager, Collider2D collision) { return false; }

    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnColliderEnterʱ���á�
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionEnter(FSMManager<T1, T2> fsm_Manager, Collision2D collision) { return false; }

    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnColliderStayʱ��ѯ��
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionStay(FSMManager<T1, T2> fsm_Manager, Collision2D collision) { return false; }

    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnColliderExitʱ���á�
    /// </summary>
    /// <param name="fsm_Manager">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionExit(FSMManager<T1, T2> fsm_Manager, Collision2D collision) { return false; }

    #endregion
}

[Serializable]
public class EnemyFSMBaseTrigger : FSMBaseTrigger<EnemyStates, EnemyTriggers> 
{
    public EnemyFSMBaseTrigger() { }

#region Update override
    public override bool IsTriggerReachInUpdate(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager)
    {
       return IsTriggerReachInUpdate(fsm_Manager as EnemyFSMManager);

    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,��Update����ѯ
    /// </summary>
    /// <param name="enemyFSM">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInUpdate(EnemyFSMManager enemyFSM){ return false; }

    public override bool IsTriggerReachInFixUpdate(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager)
    {
        return IsTriggerReachInFixUpdate(fsm_Manager as EnemyFSMManager);

    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,��FixUpdate����ѯ
    /// </summary>
    /// <param name="enemyFSM">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInFixUpdate(EnemyFSMManager enemyFSM) { return false; }
    #endregion
    #region Colider2D override
    public override bool IsTriggerReachOnCollisionEnter(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionEnter(fsm_Manager as EnemyFSMManager , collision);
    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnColliderEnterʱ���á�
    /// </summary>
    /// <param name="enemyFSM">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionEnter(EnemyFSMManager enemyFSM,Collision2D collision) { return false; }

    public override bool IsTriggerReachOnCollisionExit(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionExit(fsm_Manager as EnemyFSMManager, collision);
    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnColliderExitʱ���á�
    /// </summary>
    /// <param name="enemyFSM">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionExit(EnemyFSMManager enemyFSM, Collision2D collision) { return false;}

    public override bool IsTriggerReachOnCollisionStay(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionStay(fsm_Manager as EnemyFSMManager, collision);
    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnColliderStayʱ��ѯ��
    /// </summary>
    /// <param name="enemyFSM">������Ӧ״̬���fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionStay(EnemyFSMManager enemyFSM, Collision2D collision) { return false; }

    public override bool IsTriggerReachOnTriggerEnter(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerEnter(fsm_Manager as EnemyFSMManager, collision);
    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnTriggerEnterʱ���á�
    /// </summary>
    /// <param name="enemyFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerEnter(EnemyFSMManager enemyFSM,Collider2D collision) { return false; }

    public override bool IsTriggerReachOnTriggerExit(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerExit(fsm_Manager as EnemyFSMManager, collision);
    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnTriggerExitʱ���á�
    /// </summary>
    /// <param name="enemyFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerExit(EnemyFSMManager enemyFSM, Collider2D collision) { return false; }

    public override bool IsTriggerReachOnTriggerStay(FSMManager<EnemyStates, EnemyTriggers> fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerStay(fsm_Manager as EnemyFSMManager, collision);
    }
    /// <summary>
    /// �Ƿ�ﵽ���������жϷ���,OnTriggerStayʱ���á�
    /// </summary>
    /// <param name="enemyFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerStay(EnemyFSMManager enemyFSM, Collider2D collision) { return false; }

#endregion
    public override void InitTrigger(FSMManager<EnemyStates, EnemyTriggers> fSMManager)
    {
        base.InitTrigger(fSMManager);
        InitTrigger(fSMManager as EnemyFSMManager);
    }
    public virtual void InitTrigger(EnemyFSMManager enemyFSM) {  }
}

public class NPCFSMBaseTrigger : FSMBaseTrigger<NPCStates, NPCTriggers>
{
    public NPCFSMBaseTrigger() { }
}
//public class PlayerFSMBaseTrigger : FSMBaseTrigger<PlayerStates, PlayerTriggers>
//{
//    public PlayerFSMBaseTrigger(PlayerStates targetState) : base(targetState) { }
//    public PlayerFSMBaseTrigger() { }
//}