using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class gaze_movement : MonoBehaviour
{
    public static bool _movingIsActive = false;
    public InputActionAsset inputAction;
    public CharacterController _characterController;
    public Transform _cameraTransform;
    private InputAction _gripInputAction;

    // Start is called before the first frame update
    void Start()
    {

        _gripInputAction = inputAction.FindActionMap("XRI RightHand Locomotion").FindAction("Gaze Move");

        Debug.Log("Grip input action found");
        _gripInputAction.Enable();
        _gripInputAction.started += MoveStart;
        _gripInputAction.canceled += MoveEnd;
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!_movingIsActive)
        {
            return;
        }
        // move forward in gaze direction
        _characterController.SimpleMove(_cameraTransform.forward * 2.0f);
    }

    private void OnDestroy()
    {
        _gripInputAction.started -= MoveStart;
        _gripInputAction.canceled -= MoveEnd;
    }
    private void MoveStart(InputAction.CallbackContext context)
    {
        Debug.Log("Move Start");

        // get unit direction of gaze
        Debug.Log("Gaze direction: " + _cameraTransform.forward.ToString());
        // move forward in gaze direction
        _movingIsActive = true;

    }
    private void MoveEnd(InputAction.CallbackContext context)
    {
        Debug.Log("Move End");

        // stop moving
        _movingIsActive = false;
    }
}
