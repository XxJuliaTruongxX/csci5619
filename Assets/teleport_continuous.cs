using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class teleport_continuous : MonoBehaviour
{
    static private bool _teleportIsActive = false;
    
    public enum ControllerHand
    {
        LeftHand,
        RightHand
    }
    public InputActionAsset inputAction;
    public CharacterController _characterController;

    public XRRayInteractor rayInteractor;

    private float progress = 0.0f;
    private float teleportationSpeed = 1.0f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private InputAction _triggerInputAction;

    private InputAction _teleportActivate;

    private InputAction _teleportCancel;

    private bool triggerPressed;
    public ControllerHand targetController;

    void Start()
    {
        rayInteractor.enabled = false;
        triggerPressed = false;

        // Debug.Log("XRI " + targetController.ToString());
        _teleportActivate = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Activate");
        _teleportActivate.Enable();
        _teleportActivate.started += OnRayActivate;
        _teleportActivate.canceled += OnTeleportActivate;

        _teleportCancel = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Cancel");
        _teleportCancel.Enable();
        _teleportCancel.performed += OnTeleportCancel;

        _triggerInputAction = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Grab Move");
        _triggerInputAction.Enable();

        // if controller is lifted, increase projectile velocity
    }

    private void OnDestroy()
    {
        _teleportActivate.performed -= OnTeleportActivate;
        _teleportCancel.performed -= OnTeleportCancel;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Progress: " + progress.ToString());

        if (!_teleportIsActive)
        {
            return;
        }
        if (!rayInteractor.enabled)
        {
            return;
        }
        if (triggerPressed)
        {
            return;
        }
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit))
        {
            rayInteractor.enabled = false;
            _teleportIsActive = false;
            return;
        }
        // continuous teleportation
        progress += Time.deltaTime * teleportationSpeed;
        _characterController.transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        

        if (progress >= 1.0f)
        {
            rayInteractor.enabled = false;
            _teleportIsActive = false;
            progress = 0.0f;
        }
    }

    private void OnRayActivate(InputAction.CallbackContext context)
    {
        if (!rayInteractor.enabled) {
            rayInteractor.enabled = true;
            triggerPressed = true;
        }
    }
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if (!_teleportIsActive && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit)) {
            _teleportIsActive = true;
            triggerPressed = false;

            startPosition = _characterController.transform.position;
            endPosition = raycastHit.point;
            Debug.Log("Start: " + startPosition.ToString());
            Debug.Log("End: " + endPosition.ToString());
        }
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        if (_teleportIsActive && rayInteractor.enabled == true) {
            rayInteractor.enabled = false;
            _teleportIsActive = false;
        }
    }
}
