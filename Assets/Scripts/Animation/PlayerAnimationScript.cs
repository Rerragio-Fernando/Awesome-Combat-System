using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : AnimatorUtil
{
    [SerializeField] private float _movementLerper = .5f;

    private Animator _anim;

    private void Start() 
    {
        _anim = GetComponent<Animator>();

        PlayerEventSystem.OnSpawnInEvent += Spawn;

        PlayerEventSystem.OnCharacterIdleEvent += Idle;
        PlayerEventSystem.OnCharacterWalkEvent += Walk;
        PlayerEventSystem.OnCharacterRunEvent += Run;

        PlayerEventSystem.OnCharacterTurnEvent += Turn;

        PlayerEventSystem.OnCharacterBasicAttackTriggerEvent += TriggerBasicAttack;
        PlayerEventSystem.OnCharacterStrongAttackTriggerEvent += TriggerStrongAttack;

        PlayerEventSystem.OnDeathEvent += Death;
    }

    private void OnDisable() 
    {
        PlayerEventSystem.OnSpawnInEvent -= Spawn;

        PlayerEventSystem.OnCharacterIdleEvent -= Idle;
        PlayerEventSystem.OnCharacterWalkEvent -= Walk;
        PlayerEventSystem.OnCharacterRunEvent -= Run;

        PlayerEventSystem.OnCharacterTurnEvent += Turn;

        PlayerEventSystem.OnCharacterBasicAttackTriggerEvent -= TriggerBasicAttack;
        PlayerEventSystem.OnCharacterStrongAttackTriggerEvent -= TriggerStrongAttack;

        PlayerEventSystem.OnDeathEvent -= Death;
    }

    public void Spawn()
    {
        Debug.Log($"Spawned In");
    }

    public void Idle()
    {
        BlendTreeValue(_anim, "Movement", 0f, _movementLerper);
    }
    public void Walk()
    {
        BlendTreeValue(_anim, "Movement", 1f, _movementLerper);
    }
    public void Run()
    {
        BlendTreeValue(_anim, "Movement", 2f, _movementLerper);
    }
    public void Turn(float val)
    {
        BlendTreeValue(_anim, "Turn", val, _movementLerper);
    }
    public void TriggerBasicAttack()
    {
        AnimatorTrigger(_anim, "AttackBasicTrigger", 0.5f);
    }
    public void TriggerStrongAttack()
    {
        AnimatorTrigger(_anim, "AttackStrongTrigger", 0.5f);
    }
    public void UpdateCharacterDirection(Vector2 direction)
    {
        BlendTreeValue(_anim, "FrontBack", direction.y, _movementLerper);
        BlendTreeValue(_anim, "LeftRight", direction.x, _movementLerper);
    }

    public void Death()
    {
        AnimatorTrigger(_anim, "Death", 0.5f);
    }

    public void SetGrounded(bool val)
    {
        _anim.SetBool("Grounded", val);
    }
}