using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCombatSystem : MonoBehaviour
{
    [SerializeField] private float _movementTimeDelay;

    [Header("Attack Properties")]
    [SerializeField] private Transform _raycastTrans;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _playerNormalAtkRange;

    private bool _isAttacking = false;
    private PlayerComboScript _comboScript;
    private RaycastHit _hit;

    //Timer Variables
    private float _nextMove = 0f;

    public bool IsAttacking 
    {
        get{return _isAttacking;}
    }

    private CombatState _playerCombatState;

    private void Start() 
    {
        _comboScript = GetComponent<PlayerComboScript>();
        NotAttacking();

        PlayerInputHandler.BasicAttackEvent += BasicAttackInput;
        PlayerInputHandler.StrongAttackEvent += StrongAttackInput;
    }

    #region Input Functions
        bool _basicAtkIN, _strongAtkIN;
        void BasicAttackInput(InputActionPhase phase)
        {
            if(phase == InputActionPhase.Started)
                _basicAtkIN = true;
            else
                _basicAtkIN = false;
        }
        void StrongAttackInput(InputActionPhase phase)
        {
            if(phase == InputActionPhase.Started)
                _strongAtkIN = true;
            else
                _strongAtkIN = false;
        }
    #endregion
    
    private void Update()
    {
        Debug.Log($"Attacking :" + _isAttacking);
        if(_playerCombatState == CombatState.NotAttacking || _playerCombatState == CombatState.Attacking)
        {
            if(_basicAtkIN)
            {
                PlayerEventSystem.TriggerBasicAttack();                                                      //EVENT TRIGGERED
            }
            else if(_strongAtkIN)
            {
                PlayerEventSystem.TriggerStrongAttack();
            }
            CheckMoveable();
        }
    }

    private void CheckMoveable()
    {
        if(Time.time >= _nextMove)
            _isAttacking = false;
    }

    private void AttackLogic(int indx)
    {        
        CheckHit(10, 10f);
        // GameObject l_fx = Instantiate(l_atkFXPrefab, _atkFXParent.position, Quaternion.LookRotation(transform.forward));
        // Destroy(l_fx, 1f);
    }

    private void CheckHit(int damage, float force)
    {
        if (Physics.Raycast(_raycastTrans.position, transform.forward, out _hit, _playerNormalAtkRange, layerMask))
        {
            // var l_hitFx = Instantiate(_hitFX, _hit.point, Quaternion.LookRotation(_hit.normal));//Spawn Hit Effect
            // Destroy(l_hitFx, 1f);

            GameObject l_hitObj = _hit.transform.gameObject;

            EnemyDamageScript l_ds = l_hitObj.GetComponentInChildren<EnemyDamageScript>();
            if(l_ds != null)
            {
                PlayerEventSystem.TriggerSuccessfulHit();                                                       //EVENT TRIGGERED
                // _manaHandler.GainMana(l_ds.TakeDamage(damage, _comboScript.Combo));
            }

            Rigidbody l_rb = l_hitObj.GetComponent<Rigidbody>();
            if(l_rb != null)
            {
                l_rb.AddForce(force * transform.forward, ForceMode.Impulse);
            }
        }  
    }

    #region Animation Events
        public void Anticipation()
        {
            _playerCombatState = CombatState.Anticipation;
            PlayerEventSystem.CombatState(_playerCombatState);
            _isAttacking = true;
        }

        public void Attacking()
        {
            _playerCombatState = CombatState.Attacking;
            PlayerEventSystem.CombatState(_playerCombatState);
            PlayerEventSystem.TriggerForwardStep();                                                     //EVENT TRIGGERED
        }

        public void NotAttacking()
        {
            _playerCombatState = CombatState.NotAttacking;
            PlayerEventSystem.CombatState(_playerCombatState);
            _nextMove = Time.time + _movementTimeDelay;
        }
    #endregion
}

public enum CombatState
{
    Anticipation,
    Attacking,
    NotAttacking,
    Guarding,
    Ultimate
}