using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private PlayerLookData _lookData;

    private float _horizontalRot;
    private float _verticalRot;

    private void Start() 
    {
        PlayerInputHandler.LookEvent += LookInput;

        _verticalRot = _cameraTarget.eulerAngles.x;
        _horizontalRot = transform.rotation.y;
    }

    #region Input Functions
        private Vector2 _lookIN;

        void LookInput(Vector2 look, InputActionPhase phase)
        {
            if(phase == InputActionPhase.Performed)
                _lookIN = look;
            else
                _lookIN = Vector2.zero;
        }
    #endregion

    private void Update() 
    {
        VerticalLook();
        HorizontalLook();
    }

    void VerticalLook()
    {
        // Calculate and clamp vertical rotation for the camera target
        _verticalRot = Mathf.Clamp(
            _verticalRot - (_lookIN.y * _lookData._aimSensitivity),
            _lookData._minRotation,
            _lookData._maxRotation
        );

        // Update camera target rotation
        _cameraTarget.eulerAngles = new Vector3(_verticalRot, _horizontalRot, 0f);
    }

    void HorizontalLook()
    {
        PlayerEventSystem.CharacterTurn(_lookIN.x);
        _horizontalRot += _lookIN.x * _lookData._aimSensitivity;
        transform.rotation = Quaternion.Euler(0f, _horizontalRot, 0f);
    }
}