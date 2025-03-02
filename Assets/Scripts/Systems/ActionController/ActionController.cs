using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for:
/// ♦ queuing player inputs
/// ♦ Cancelling when needed
/// </summary>

public enum PlayerCombatState
{
    PLAYER_IDLE,
    PLAYER_BASIC_ATTACK,
    PLAYER_STRONG_ATTACK,
    PLAYER_GUARD,
    PLAYER_WEAPON_CYCLE,
    PLAYER_TAKE_DAMAGE
}

public class ActionController : MonoBehaviour
{
    [SerializeField, Tooltip("Time interval that will discard the next combat state of player")]
    private float timeCheck;
    
    public Action BasicAttack;
    public Action StrongAttack;
    public Action Guard;

    public Action CycleWeapon;

    public Action TakeDamage;

    public Action<float> ModifyMovement;
    public Action<float> ModifyLook;
    
    public Action ResetAction;

    public PlayerCombatState playerCurrentState = PlayerCombatState.PLAYER_IDLE;
    public PlayerCombatState playerNextState = PlayerCombatState.PLAYER_IDLE;

    private Coroutine discardTimer;

    private bool holding = false;

    //Used to check if the action controller is currently executing a state (Which is not IDLE);
    private bool running = false;

    private void OnEnable() {
        PlayerInputHandler.BasicAttackEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);

        PlayerInputHandler.GuardEvent += (phase) => SetNextStateHold(phase, PlayerCombatState.PLAYER_GUARD);

        PlayerInputHandler.CycleWeaponEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_WEAPON_CYCLE);
    }

    private void OnDisable() {
        PlayerInputHandler.BasicAttackEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);

        PlayerInputHandler.GuardEvent -= (phase) => SetNextStateHold(phase, PlayerCombatState.PLAYER_GUARD);

        PlayerInputHandler.CycleWeaponEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_WEAPON_CYCLE);
    }

    private void Update() {
        // Debug.Log($"Player Combat State: " + playerCurrentState);
    }

    /// <summary>
    /// Sets up the next state
    /// </summary>
    /// <param name="phase">Input action phase</param>
    /// <param name="nextCombatState">The next combat state</param>
    private void SetNextStateTap(InputActionPhase phase, PlayerCombatState nextCombatState = PlayerCombatState.PLAYER_IDLE)
    {
        if(running)
        {
            AssignToNextState(nextCombatState);
            return;
        }

        if(phase == InputActionPhase.Performed)
        {
            playerCurrentState = nextCombatState;
            ExecuteAction();
        }        
    }

    private void SetNextStateHold(InputActionPhase phase, PlayerCombatState nextCombatState = PlayerCombatState.PLAYER_IDLE)
    {
        if(phase == InputActionPhase.Performed)
        {
            if(playerCurrentState != PlayerCombatState.PLAYER_IDLE) return;
            
            holding = true;
            playerCurrentState = nextCombatState;
        }
        else if(phase == InputActionPhase.Canceled && holding)
        {
            holding = false;
            HandleResetAction();
        }

        ExecuteAction();
    }

    void AssignToNextState(PlayerCombatState nextCombatState)
    {
        playerNextState = nextCombatState;
        StartDiscardTimer();
    }

    /// <summary>
    /// Executes the current player state
    /// </summary>
    private void ExecuteAction(){
        running = true;
        switch (playerCurrentState)
        {
            case PlayerCombatState.PLAYER_BASIC_ATTACK:
                BasicAttack?.Invoke();
                break;

            case PlayerCombatState.PLAYER_STRONG_ATTACK:
                StrongAttack?.Invoke();
                break;
            
            case PlayerCombatState.PLAYER_GUARD:
                Guard?.Invoke();
                break;
            
            case PlayerCombatState.PLAYER_WEAPON_CYCLE:
                CycleWeapon?.Invoke();
                break;
        }
    }

    public void HandleResetAction(){
        ResetAction?.Invoke();
        running = false;
        playerCurrentState = playerNextState;
        if(playerNextState != PlayerCombatState.PLAYER_IDLE)
        {
            ExecuteAction();
            playerNextState = PlayerCombatState.PLAYER_IDLE;
        }
    }

    void StartDiscardTimer()
    {
        if(discardTimer != null)
        {
            StopCoroutine(discardTimer);
            discardTimer = null;
        }

        discardTimer = StartCoroutine(DiscardTimer());
    }

    IEnumerator DiscardTimer()
    {
        yield return new WaitForSeconds(timeCheck);
        playerNextState = PlayerCombatState.PLAYER_IDLE;
    }
}