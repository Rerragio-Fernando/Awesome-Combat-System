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
    PLAYER_DODGE
}

public class ActionController : MonoBehaviour
{
    public Action Idle;
    public Action BasicAttack;
    public Action StrongAttack;
    public Action Guard;
    public Action Dodge;

    private PlayerCombatState playerCurrentState = PlayerCombatState.PLAYER_IDLE;
    private PlayerCombatState playerNextState = PlayerCombatState.PLAYER_IDLE;

    private void OnEnable() {
        PlayerInputHandler.BasicAttackEvent += (phase) => SetNextState(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent += (phase) => SetNextState(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);
    }

    private void OnDisable() {
        PlayerInputHandler.BasicAttackEvent -= (phase) => SetNextState(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent -= (phase) => SetNextState(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);
    }

    private void Update() {
        Debug.Log($"Player Combat State: " + playerCurrentState);
    }

    private void SetNextState(InputActionPhase phase, PlayerCombatState nextCombatState = PlayerCombatState.PLAYER_IDLE)
    {
        if(phase == InputActionPhase.Performed)
            playerNextState = nextCombatState;
    }

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
            
            case PlayerCombatState.PLAYER_DODGE:
                Dodge?.Invoke();
                break;
            
            default:
                Idle?.Invoke();
                break;
        }
    }

    //Called by Animation Events
    public void MoveToNextAction(){
        playerCurrentState = playerNextState;
        playerNextState = PlayerCombatState.PLAYER_IDLE;
    }
}