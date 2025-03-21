using UnityEngine;

public class MeleeAttack : CombatAnimation
{
    [SerializeField, Tooltip("Contains a list of all the relevant attacks")]
    private AttackData[] attackData;

    [SerializeField, Tooltip("Contact Points List")]
    private Transform[] contactPointsList;

    private int attackIndex = 0;
    private float nextCombo = 0f;
    private float nextComboWindow;
    private int attackDataLength;

    private bool firstStrike = true;

    protected float movementModifier;
    protected float lookModifier;
    protected float forwardStepModifier;

    private HitRegisterController hitRegController;
    private PlayerStatManager playerStatManager;
    private Hit playerAttackHit;

    protected virtual void Awake() {
        hitRegController = GetComponentInParent<HitRegisterController>();
        playerStatManager = GetComponentInParent<PlayerStatManager>();
    }

    protected override void Start()
    {
        base.Start();
        attackDataLength = attackData.Length;
    }

    protected virtual void OnEnable() {
        if(hitRegController != null)
        {
            hitRegController.RegisterHitEvent += HitFrame;
        }
    }

    protected virtual void OnDisable() {
        if(hitRegController != null)
        {
            hitRegController.RegisterHitEvent -= HitFrame;
        }
    }

    /// <summary>
    /// Attack logic goes here
    /// </summary>
    protected virtual void Attack()
    {
        // Handle combo timing logic
        if(Time.time > nextCombo)
        {
            firstStrike = true;
            attackIndex = 0;
        }

        AnimationServices.PlayAnimation(anim, attackData[attackIndex].animationStateName, crossFadeHolder);
        // Set the next combo window
        nextComboWindow = attackData[attackIndex].nextMeleeAttackTimeWindow;

        movementModifier = attackData[attackIndex].movementModifier;

        forwardStepModifier = attackData[attackIndex].forwardStep;

        //Assign Hit Probability
        playerAttackHit = playerStatManager.GetHitProbability();

        // RetreiveData();

        // Handle first strike (initial attack in combo)
        if(firstStrike)
        {
            firstStrike = false;
            attackIndex = (attackIndex + 1) % attackDataLength;  // Use modulo for wrapping
            return;
        }

        // Handle combo cycle (if within combo time window)
        if(Time.time <= nextCombo)
        {
            attackIndex = (attackIndex + 1) % attackDataLength;  // Use modulo for wrapping
        }

    }

    public void HitFrame(int contactPointIndex)
    {
        if(playerAttackHit == null) return;

        Transform hitPoint = contactPointsList[contactPointIndex];

        if(hitPoint == null)
        {
            Debug.Log($"Hit point Non Existent");
            return;
        }

        float force = attackData[attackIndex].maxHitForce * playerAttackHit.hitAmount;
        
        GameObject attackHitFx;
        if(playerAttackHit.hitLevel == HitLevel.MISS)
            attackHitFx = attackData[attackIndex].hitFxData.missFx;
        else if(playerAttackHit.hitLevel == HitLevel.LIGHT)
            attackHitFx = attackData[attackIndex].hitFxData.lightHitFx;
        else if(playerAttackHit.hitLevel == HitLevel.LIGHT)
            attackHitFx = attackData[attackIndex].hitFxData.strongHitFx;
        else
            attackHitFx = attackData[attackIndex].hitFxData.criticalHitFx;

        Collider[] hitColliders = Physics.OverlapSphere(hitPoint.position, attackData[attackIndex].checkSphereRadius, attackData[attackIndex].hitLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);

            var hitFx = Instantiate(attackHitFx, hitPoint.position, Quaternion.identity);
        }
    }

    protected override void ResetAnimationState()
    {
        nextCombo = Time.time + nextComboWindow;
        base.ResetAnimationState();
    }

    void RetreiveData()
    {
        Debug.Log(attackData[attackIndex].animationStateName + " performed. " + "Time.time = " + Time.time + " nextCombo = " + nextCombo);
    }
}