using UnityEngine;

public class LookModifier : MonoBehaviour
{
    [SerializeField, Tooltip("Default Movement Modifier")]
    private float defaultLookModifier = 1f;
    
    public float lookModifier;

    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            controller.ModifyLook += ModifyLook;
            controller.ResetAction += ResetMovementModifier;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.ModifyLook -= ModifyLook;
            controller.ResetAction -= ResetMovementModifier;
        }
    }

    protected void ModifyLook(float val)
    {
        lookModifier = val;
    }

    protected void ResetMovementModifier()
    {
        controller.ModifyLook(defaultLookModifier);
    }
}