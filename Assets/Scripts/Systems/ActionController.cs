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
    }

    private void OnDisable() {
        PlayerInputHandler.BasicAttackEvent -= (phase) => SetNextState(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
    }

    private void SetNextState(InputActionPhase phase, PlayerCombatState nextCombatState = PlayerCombatState.PLAYER_IDLE)
    {
        if(phase == InputActionPhase.Performed)
            playerNextState = nextCombatState;
    }
}