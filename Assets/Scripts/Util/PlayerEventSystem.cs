using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventSystem : GameCharacterEventSystem
{
    //Event Declaration
    public static event Action OnCharacterIdleEvent;
    public static event Action OnCharacterWalkEvent;
    public static event Action OnCharacterRunEvent;
    public static event Action<float> OnCharacterTurnEvent;

    public static event Action OnCharacterBasicAttackTriggerEvent;
    public static event Action OnCharacterStrongAttackTriggerEvent;
    public static event Action<CombatState> CombatStateEvent;
    public static event Action OnSuccessfulHitEvent;
    public static event Action OnResetComboEvent;

    public static event Action OnForwardStepEvent;

    public static event Action OnAnimationJumpForceEvent;

    //Event Method

    #region Locomotion
    public static void CharacterIdle(){
        OnCharacterIdleEvent?.Invoke();
    }
    public static void CharacterWalk(){
        OnCharacterWalkEvent?.Invoke();
    }
    public static void CharacterRun(){
        OnCharacterRunEvent?.Invoke();
    }
    public static void CharacterTurn(float val){
        OnCharacterTurnEvent?.Invoke(val);
    }
    #endregion

    #region Attacks
    public static void TriggerBasicAttack(){
        OnCharacterBasicAttackTriggerEvent?.Invoke();
    }
    public static void TriggerStrongAttack(){
        OnCharacterStrongAttackTriggerEvent?.Invoke();
    }
    public static void CombatState(CombatState cs){
        CombatStateEvent?.Invoke(cs);
    }
    public static void TriggerSuccessfulHit(){
        OnSuccessfulHitEvent?.Invoke();
    }
    public static void TriggerResetCombo(){
        OnResetComboEvent?.Invoke();
    }
    #endregion

    #region MISC
    public static void TriggerForwardStep(){
        OnForwardStepEvent?.Invoke();
    }
    #endregion
}