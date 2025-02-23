using UnityEngine;

public class MeleeAttack : CombatAnimation
{
    [SerializeField, Tooltip("Contains a list of all the relevant attacks")]
    private AttackData[] attackData;

    private int attackIndex = 0;
    private float nextCombo = 0f;
    private float nextComboWindow;
    private int attackDataLength;

    private bool firstStrike = true;

    protected override void Start()
    {
        base.Start();
        attackDataLength = attackData.Length;
    }

    /// <summary>
    /// Attack logic goes here
    /// </summary>
    protected void Attack()
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

        RetreiveData();  // Assuming this is necessary and doesn't need optimization

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