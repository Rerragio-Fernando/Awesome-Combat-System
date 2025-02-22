using UnityEngine;

public class MeleeAttack : CombatAnimation
{
    [SerializeField, Tooltip("Contains a list of all the relevant attacks")]
    private AttackData[] attackData;

    private int attackIndex = 0;
    private float nextCombo = 0f;
    private float nextComboWindow;
    private bool firstAttack = true;

    /// <summary>
    /// Attack logic goes here
    /// </summary>
    protected void Attack()
    {
        AnimationServices.PlayAnimation(anim, attackData[attackIndex].animationStateName, crossFadeHolder);
        nextComboWindow = attackData[attackIndex].nextMeleeAttackTimeWindow;

        // RetreiveData();

        if(Time.time <= nextCombo)
        {
            //Holds the crossfade value for the previous attack
            crossFadeHolder = attackData[attackIndex].crossFade;

            attackIndex++;
            if(attackIndex > attackData.Length - 1)
                attackIndex = 0;
        }
        else
        {
            attackIndex = 0;
            firstAttack = true;
            crossFadeHolder = defaultCrossFade;
        }
    }

    protected override void ResetAnimationState()
    {
        base.ResetAnimationState();

        nextCombo = Time.time + nextComboWindow;

        if(firstAttack)
        {
            attackIndex++;
            firstAttack = false;
        }
    }

    void RetreiveData()
    {
        Debug.Log($"Attack " + attackIndex + " performed. " + "Time.time = " + Time.time + " nextCombo = " + nextCombo);
    }
}