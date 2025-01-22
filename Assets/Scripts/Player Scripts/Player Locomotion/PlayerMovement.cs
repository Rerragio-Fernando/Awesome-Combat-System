using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _cam;

    [Header("Player Properties")]
    [SerializeField] private float _gravity;//
    [SerializeField] private float _playerMass;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _forwardStepSpeed;
    [SerializeField] private float _playerFriction;//
    [SerializeField] private float _distanceToGround;//
    [SerializeField] private LayerMask _groundLayer;//
    [SerializeField] private Transform _groundTrans;

    private float _targAngle;
    private float _moveSpeed = 0f;
    private float _nextDodge = 0f;
    private Vector3 _movDir;
    private bool _isGrounded;
    private bool _isGuarding = false;
    private bool _isAttacking = false;
    private Vector3 _velocity;
    private PlayerAnimationScript _playerAnim;
    private CharacterController _cont;
    private PlayerCombatSystem _combatSys;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        _cont = GetComponent<CharacterController>();
        _playerAnim = GetComponent<PlayerAnimationScript>();
        _combatSys = GetComponent<PlayerCombatSystem>();

        PlayerInputHandler.MoveEvent += MoveInput;

        PlayerInputHandler.SprintEvent += SprintInput;

        PlayerEventSystem.OnForwardStepEvent += ForwardStep;
    }

    #region Input Functions
        private Vector2 _movementIN;
        private bool _aimIN;
        private bool _sprintIN;

        void MoveInput(Vector2 move, InputActionPhase phase){
            if(phase == InputActionPhase.Performed)
                _movementIN = move;
            else
                _movementIN = Vector2.zero;
        }
        void SprintInput(InputActionPhase phase){
            if(phase == InputActionPhase.Started)
                _sprintIN = !_sprintIN;
        }
    #endregion

    void Guarding(bool val){
        _isGuarding = val;
    }

    private void Update() {
        Grounded();
        PlayerMovementFunction();

        _cont.Move(_velocity * Time.deltaTime);
    }

    void Grounded(){
        _isGrounded = Physics.CheckSphere(_groundTrans.position, _distanceToGround, _groundLayer);
        _playerAnim.SetGrounded(_isGrounded);
    }
    
    void PlayerMovementFunction(){
        if(!_isGuarding){
            // Calculate movement direction and target angle
            _movDir = new Vector3(_movementIN.x, 0f, _movementIN.y).normalized;
            _targAngle = Mathf.Atan2(_movDir.x, _movDir.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;

            // Handle vertical velocity
            if(!_isGrounded){
                _velocity.y += -_gravity * _playerMass * Time.deltaTime;
            }
            else{
                _velocity.y = -1f;
            }

            // Handle movement and animation states
            if(_movDir.magnitude > 0.25f && _isGrounded && !_combatSys.IsAttacking){
                if(_sprintIN && (_movDir.z > 0f && (_movDir.x < 0.25f && _movDir.x > -0.25f))){
                    _moveSpeed = _runSpeed;
                    PlayerEventSystem.CharacterRun();
                }
                else{
                    _moveSpeed = _walkSpeed;
                    PlayerEventSystem.CharacterWalk();
                }
                Vector3 movDir = Quaternion.Euler(0f, _targAngle, 0f) * Vector3.forward;
                Vector3 adjustedVelocity = movDir.normalized * _moveSpeed;
                _velocity = new Vector3(adjustedVelocity.x, _velocity.y, adjustedVelocity.z);
            }
            else{
                PlayerEventSystem.CharacterIdle();
                _sprintIN = false;
                ApplyFriction();
            }

            _playerAnim.UpdateCharacterDirection(_movementIN);
        }
        else{
            HandleGuarding();
        }
    }

    void ApplyFriction(){
        PlayerEventSystem.CharacterIdle();
        float friction = _isGrounded ? _playerFriction : 0.05f;
        _velocity = Vector3.Lerp(_velocity, new Vector3(0f, _velocity.y, 0f), friction * Time.deltaTime);
    }

    void HandleGuarding(){
        
    }

    public void ForwardStep(){
        _velocity += transform.forward * _forwardStepSpeed;
    }
}