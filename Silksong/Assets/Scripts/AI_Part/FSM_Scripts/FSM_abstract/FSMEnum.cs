using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Statesʱ��ؼ�����š�
/// </summary>
public enum EnemyStates
{
    StateCombinationNode=99,
    EnemySubFSMManager = 10,

    Enemy_Any_State =0,
    Enemy_Idle_State=1,
    Enemy_Patrol_State=2,
    Enemy_Hitted_State=3,
    Enemy_Attack_State=4,
    Enemy_Climb_State=5,
    Enemy_Pursuit_State=6,
    Enemy_Wander_State=7,
    Enemy_Shoot_State=8,
    Enemy_Bump_State=9,

    Enemy_Chase_State=11,
    Enemy_Smash_Down_State=12,
    Boss_Shoot_State=13,
    Enemy_Turn_State = 14,
    Enemy_Meet_State = 15,
    Enemy_Circle_State = 16,
    Enemy_Die_State=17,
    LittleMonster_Die=18,
    BigMonster_Die=19
}
/// <summary>
/// ����Triggerʱ��ؼ�����š�
/// </summary>
public enum EnemyTriggers
{
    WaitTimeTrigger=0,
    HitWallTrigger=1,
    PlayerDistanceTrigger=2,
    AnimationPlayOverTrigger=3,
    OnHittedTrigger=4,
    SelfHPTrigger=5,
    NearPlatformBorderTrigger=6,
    TargetTurnTrigger=7,
    RandomTrigger = 8,
    TouchLayerTrigger=9,
}




public enum NPCStates
{
    NPC_Idle_State,
    NPC_Run_State
}

public enum NPCTriggers
{
    WaitTimeTrigger,
    HitWallTrigger,
    PlayerDistanceTrigger,
}

public enum PlayerStates
{
    Player_Idle_State,
    Player_Run_State
}

public enum PlayerTriggers
{
    W_Key_Down,
    A_Key_Down,
    S_Key_Down,
    D_Key_Down
}
