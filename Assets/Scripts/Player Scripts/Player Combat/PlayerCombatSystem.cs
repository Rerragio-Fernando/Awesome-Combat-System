using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCombatSystem : MonoBehaviour
{
    [SerializeField] private float movementTimeDelay;

    [Header("Attack Properties")]
    [SerializeField] private Transform raycastTrans;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float playerNormalAtkRange;

    private bool isAttacking = false;
    private PlayerComboScript comboScript;
    private RaycastHit hit;

    //Timer Variables
    private float nextMove = 0f;

    public bool IsAttacking 
    {
        get{return isAttacking;}
    }

    private CombatState playerCombatState;

    private void Start() 
    {
        comboScript = GetComponent<PlayerComboScript>();
        NotAttacking();

        PlayerInputHandler.BasicAttackEvent += BasicAttackInput;
        PlayerInputHandler.StrongAttackEvent += StrongAttackInput;
    }

    #region Input Functions
        bool basicAtkIN, strongAtkIN;
        bool guardIN;
        void BasicAttackInput(InputActionPhase phase)
        {
            if(phase == InputActionPhase.Started)
                basicAtkIN = true;
            else
                basicAtkIN = false;
        }
        void StrongAttackInput(InputActionPhase phase)
        {
            if(phase == InputActionPhase.Started)
                strongAtkIN = true;
            else
                strongAtkIN = false;
        }
    #endregion
    
    private void Update()
    {
        Debug.Log($"Attacking :" + isAttacking);
        if(playerCombatState == CombatState.NotAttacking || playerCombatState == CombatState.Attacking)
        {
            if(basicAtkIN)
            {
                PlayerEventSystem.TriggerBasicAttack();                                                      //EVENT TRIGGERED
            }
            else if(strongAtkIN)
            {
                PlayerEventSystem.TriggerStrongAttack();
            }
            CheckMoveable();
        }
    }

    private void CheckMoveable()
    {
        if(Time.time >= nextMove)
            isAttacking = false;
    }

    private void AttackLogic(int indx)
    {        
        CheckHit(10, 10f);
        // GameObject lfx = Instantiate(latkFXPrefab, atkFXParent.position, Quaternion.LookRotation(transform.forward));
        // Destroy(lfx, 1f);
    }

    private void CheckHit(int damage, float force)
    {
        if (Physics.Raycast(raycastTrans.position, transform.forward, out hit, playerNormalAtkRange, layerMask))
        {
            GameObject lhitObj = hit.transform.gameObject;

            CharacterHealth lds = lhitObj.GetComponentInChildren<CharacterHealth>();
            if(lds != null)
            {
                PlayerEventSystem.TriggerSuccessfulHit();                                                       //EVENT TRIGGERED
            }

            Rigidbody lrb = lhitObj.GetComponent<Rigidbody>();
            if(lrb != null)
            {
                lrb.AddForce(force * transform.forward, ForceMode.Impulse);
            }
        }  
    }

    #region Animation Events
        public void Anticipation()
        {
            playerCombatState = CombatState.Anticipation;
            PlayerEventSystem.CombatState(playerCombatState);
            isAttacking = true;
        }

        public void Attacking()
        {
            playerCombatState = CombatState.Attacking;
            PlayerEventSystem.CombatState(playerCombatState);
            PlayerEventSystem.TriggerForwardStep();                                                     //EVENT TRIGGERED
        }

        public void Guarding()
        {
            playerCombatState = CombatState.Attacking;
            PlayerEventSystem.CombatState(playerCombatState);
            PlayerEventSystem.TriggerForwardStep();                                                     //EVENT TRIGGERED
        }

        public void NotAttacking()
        {
            playerCombatState = CombatState.NotAttacking;
            PlayerEventSystem.CombatState(playerCombatState);
            nextMove = Time.time + movementTimeDelay;
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