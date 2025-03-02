using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct AttackType
{
    public bool attack;
    public int attackType;//1-basic, 2-strong, 3-AOE
}

public class PlayerInputHandler : SingletonBehaviour<PlayerInputHandler> 
{
    private IA_Player _playerInputActions;
    private bool _playerAlive;

    // Movement Events
    public static event Action<Vector2, InputActionPhase> MoveEvent;
    public static event Action<Vector2, InputActionPhase> LookEvent;
    
    public static event Action<InputActionPhase> SprintEvent;
    public static event Action<InputActionPhase> GuardEvent;

    public static event Action<InputActionPhase> BasicAttackEvent;
    public static event Action<InputActionPhase> StrongAttackEvent;

    public static event Action<InputActionPhase> CycleWeaponEvent;

    protected override void Awake()
    {
        base.Awake();
        _playerInputActions = new IA_Player();
        _playerAlive = true;
    }

    private void OnEnable() 
    {
        _playerInputActions.Enable();
    
        // Player input bindings
        OnEnable(_playerInputActions.Player.Movement, inputContext => IfPlayerAlive(() => MoveEvent?.Invoke(inputContext.ReadValue<Vector2>(), inputContext.phase)));
        OnEnable(_playerInputActions.Player.Look, inputContext => IfPlayerAlive(() => LookEvent?.Invoke(inputContext.ReadValue<Vector2>(), inputContext.phase)));

        OnEnable(_playerInputActions.Player.Sprint, inputContext => IfPlayerAlive(() => SprintEvent?.Invoke(inputContext.phase)));

        OnEnable(_playerInputActions.Player.Guard, inputContext => IfPlayerAlive(() => GuardEvent?.Invoke(inputContext.phase)));

        OnEnable(_playerInputActions.Player.BasicAttack, inputContext => IfPlayerAlive(() => BasicAttackEvent?.Invoke(inputContext.phase)));
        OnEnable(_playerInputActions.Player.StrongAttack, inputContext => IfPlayerAlive(() => StrongAttackEvent?.Invoke(inputContext.phase)));

        OnEnable(_playerInputActions.Player.CycleWeapon, inputContext => IfPlayerAlive(() => CycleWeaponEvent?.Invoke(inputContext.phase)));
    }

    private void OnDisable() 
    {
        _playerInputActions.Disable();

        OnDisable(_playerInputActions.Player.Movement, inputContext => IfPlayerAlive(() => MoveEvent?.Invoke(inputContext.ReadValue<Vector2>(), inputContext.phase)));
        OnDisable(_playerInputActions.Player.Look, inputContext => IfPlayerAlive(() => LookEvent?.Invoke(inputContext.ReadValue<Vector2>(), inputContext.phase)));

        OnDisable(_playerInputActions.Player.Sprint, inputContext => IfPlayerAlive(() => SprintEvent?.Invoke(inputContext.phase)));

        OnDisable(_playerInputActions.Player.Guard, inputContext => IfPlayerAlive(() => GuardEvent?.Invoke(inputContext.phase)));

        OnDisable(_playerInputActions.Player.BasicAttack, inputContext => IfPlayerAlive(() => BasicAttackEvent?.Invoke(inputContext.phase)));
        OnDisable(_playerInputActions.Player.StrongAttack, inputContext => IfPlayerAlive(() => StrongAttackEvent?.Invoke(inputContext.phase)));

        OnDisable(_playerInputActions.Player.CycleWeapon, inputContext => IfPlayerAlive(() => CycleWeaponEvent?.Invoke(inputContext.phase)));
    }

    private void IfPlayerAlive(Action action)
    
    {
        if (_playerAlive) action?.Invoke();
    }

    void UnalivePlayer()
    {
        _playerAlive = false;
    }

    private void OnEnable(InputAction inputAction, Action<InputAction.CallbackContext> callback)
    
    {
        inputAction.started += callback;
        inputAction.performed += callback;
        inputAction.canceled += callback;
    }

    private void OnDisable(InputAction inputAction, Action<InputAction.CallbackContext> callback)
    
    {
        inputAction.started -= callback;
        inputAction.performed -= callback;
        inputAction.canceled -= callback;
    }
}