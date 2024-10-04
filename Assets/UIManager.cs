using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public GameObject leftUIRay;
    public InputActionAsset inputAction;
    private InputAction _primaryButtonAction;

    public enum ControllerHand
    {
        LeftHand,
        RightHand
    }

    public ControllerHand _controllerHand;
    void Start()
    {
        canvas.SetActive(false);
        leftUIRay.SetActive(false);

        Debug.Log("XRI " + _controllerHand.ToString() + " Interaction");
        _primaryButtonAction = inputAction.FindActionMap("XRI " + _controllerHand.ToString() + " Interaction").FindAction("Select");
        _primaryButtonAction.Enable();
        _primaryButtonAction.performed += OnPrimaryButton;
        
    }

    private void OnDestroy()
    {
        _primaryButtonAction.performed -= OnPrimaryButton;
    }
    private void OnPrimaryButton(InputAction.CallbackContext obj)
    {
        canvas.SetActive(!canvas.activeSelf);
        leftUIRay.SetActive(!leftUIRay.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
