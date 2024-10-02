using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class rightscript : MonoBehaviour
{
    static private bool _teleportIsActive = false;
    
    public enum ControllerHand
    {
        LeftHand,
        RightHand
    }

    public InputActionAsset inputAction;

    public XRRayInteractor rayInteractor;

    public TeleportationProvider teleportationProvider;

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
        _teleportActivate.canceled -= OnTeleportActivate;
        _teleportActivate.started -= OnRayActivate;
        _teleportCancel.performed -= OnTeleportCancel;
    }
    // Update is called once per frame
    void Update()
    {
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
        if (raycastHit.collider.gameObject.layer != 0)
        {
            rayInteractor.enabled = false;
            _teleportIsActive = false;
            return;
        }
        TeleportRequest teleportRequest = new TeleportRequest();
        teleportRequest.destinationPosition = raycastHit.point;
        
        teleportationProvider.QueueTeleportRequest(teleportRequest);

        rayInteractor.enabled = false;
        _teleportIsActive = false;
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
        if (!_teleportIsActive) {
            _teleportIsActive = true;
            triggerPressed = false;
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
