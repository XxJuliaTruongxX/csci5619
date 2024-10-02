using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;
public class LocomotionManager : MonoBehaviour
{
    public GameObject rightRayTeleport;

    private TeleportationProvider _teleportationProvider;
    private teleport_continuous _teleportContinuous;
    private ContinuousMoveProviderBase _continuousMoveProvider;
    private gaze_movement _gazeMovement;
    private point_movement _pointMovement;
    // Start is called before the first frame update
    void Start()
    {
        _teleportationProvider = GetComponent<TeleportationProvider>();
        _teleportContinuous = GetComponent<teleport_continuous>();
        _continuousMoveProvider = GetComponent<ContinuousMoveProviderBase>();
        _gazeMovement = GetComponent<gaze_movement>();
        _pointMovement = GetComponent<point_movement>();
    }

    public void SwitchLocomotion(int locomotionValue)
    {
        if(locomotionValue == 0)
        {
            DisableContinuous();
            DisableContinuousTeleport();
            DisableGaze();
            DisablePoint();
            EnableTeleport();
        }
        else if (locomotionValue == 1)
        {
            DisableTeleport();
            DisableContinuousTeleport();
            DisableGaze();
            DisablePoint();
            EnableContinuous();
        }
        else if (locomotionValue == 2)
        {
            DisableTeleport();
            DisableContinuous();
            DisableGaze();
            DisablePoint();
            EnableContinuousTeleport();
        }
        else if (locomotionValue == 3)
        {
            DisableTeleport();
            DisableContinuous();
            DisableContinuousTeleport();
            DisablePoint();
            EnableGaze();
        }
        else if (locomotionValue == 4)
        {
            DisableTeleport();
            DisableContinuous();
            DisableContinuousTeleport();
            DisableGaze();
            EnablePoint();
        }
    }

    private void DisableTeleport()
    {
        rightRayTeleport.SetActive(false);
        _teleportationProvider.enabled = false;
    }

    private void EnableTeleport()
    {
        rightRayTeleport.SetActive(true);
        _teleportationProvider.enabled = true;
    }

    private void DisableContinuous()
    {
        _continuousMoveProvider.enabled = false;
    }

    private void EnableContinuous()
    {
        _continuousMoveProvider.enabled = true;
    }

    private void DisableGaze()
    {
        _gazeMovement.enabled = false;
    }

    private void EnableGaze()
    {
        _gazeMovement.enabled = true;
    }

    private void DisablePoint()
    {
        _pointMovement.enabled = false;
    }

    private void EnablePoint()
    {
        _pointMovement.enabled = true;
    }

    private void EnableContinuousTeleport()
    {
        rightRayTeleport.SetActive(true);
        _teleportContinuous.enabled = true;
    }

    private void DisableContinuousTeleport()
    {
        rightRayTeleport.SetActive(false);
        _teleportContinuous.enabled = false;
    }
}
