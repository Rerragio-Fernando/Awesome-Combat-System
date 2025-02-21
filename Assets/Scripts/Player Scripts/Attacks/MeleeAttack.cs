using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField, Tooltip("Contains a list of all the relevant attacks")]
    private AttackData[] attackData;

    [SerializeField, Tooltip("Name of Empty State in the Animator")]
    private string emptyAnimationStateName = "Empty State";

    private Animator anim;
    private int attackIndex = 0;
    private float nextCombo = 0f;
    private float nextComboWindow;

    private void Start() 
    {
        anim = GetComponentInParent<Animator>();
    }

    protected void Attack()
    {
        AnimationServices.PlayAnimation(anim, attackData[attackIndex].animationStateName);
        nextComboWindow = attackData[attackIndex].nextMeleeAttackTimeWindow;

        if(Time.time <= nextCombo)
        {
            attackIndex++;
            if(attackIndex > attackData.Length - 1)
                attackIndex = 0;
        }
        else
        {
            attackIndex = 0;
        }
    }

    protected void BackToEmpty()
    {
        AnimationServices.PlayAnimation(anim, emptyAnimationStateName);
        nextCombo = Time.time + nextComboWindow;
    }
}