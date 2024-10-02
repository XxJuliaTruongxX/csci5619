using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class point_movement : MonoBehaviour
{
    public static bool _pointWalkIsActive = false;
    public enum ControllerHand
    {
        LeftHand,
        RightHand
    }
    public ControllerHand _controllerHand;
    public CharacterController _characterController;
    public InputActionAsset inputAction;

    public Transform _controllerTransform;
    private InputAction _primaryButtonAction;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("XRI " + _controllerHand.ToString() + " Locomotion");
        _primaryButtonAction = inputAction.FindActionMap("XRI " + _controllerHand.ToString() + " Locomotion").FindAction("Select");
        _primaryButtonAction.Enable();
        _primaryButtonAction.started += PointWalkActivate;
        _primaryButtonAction.canceled += PointWalkDeactivate;

    }

    // Update is called once per frame
    void Update()
    {
        if (!_pointWalkIsActive)
        {
            return;
        }
        // move forward in gaze direction
        _characterController.SimpleMove(_controllerTransform.forward * 2.0f);
    }

    private void OnDestroy()
    {
        _primaryButtonAction.performed -= PointWalkActivate;
        _primaryButtonAction.performed -= PointWalkDeactivate;
    }

    private void PointWalkActivate(InputAction.CallbackContext obj)
    {
        Debug.Log(_controllerTransform.forward.ToString());
        _pointWalkIsActive = true;

    }

    private void PointWalkDeactivate(InputAction.CallbackContext obj)
    {
        Debug.Log("Point Walk Deactivated");
        _pointWalkIsActive = false;
    }
}
