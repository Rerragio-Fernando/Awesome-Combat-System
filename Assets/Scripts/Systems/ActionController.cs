using System;
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
    PLAYER_TAKE_DAMAGE
}

public class ActionController : MonoBehaviour
{
    public Action BasicAttack;
    public Action StrongAttack;
    public Action Guard;

    public Action TakeDamage;
    
    public Action ResetAction;

    private PlayerCombatState playerCurrentState = PlayerCombatState.PLAYER_IDLE;
    private PlayerCombatState playerNextState = PlayerCombatState.PLAYER_IDLE;

    private bool holding = false;

    private void OnEnable() {
        PlayerInputHandler.BasicAttackEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);

        PlayerInputHandler.GuardEvent += (phase) => SetNextStateHold(phase, PlayerCombatState.PLAYER_GUARD);
    }

    private void OnDisable() {
        PlayerInputHandler.BasicAttackEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);

        PlayerInputHandler.GuardEvent -= (phase) => SetNextStateHold(phase, PlayerCombatState.PLAYER_GUARD);
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
        if(playerCurrentState != PlayerCombatState.PLAYER_IDLE) return;

        if(phase == InputActionPhase.Performed)
            playerCurrentState = nextCombatState;
        
        ExecuteAction();
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

    /// <summary>
    /// Executes the current player state
    /// </summary>
    private void ExecuteAction(){
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
        }
    }

    public void HandleResetAction(){
        ResetAction?.Invoke();
        playerCurrentState = PlayerCombatState.PLAYER_IDLE;
    }
}