using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;
public class LocomotionManager : MonoBehaviour
{
    public GameObject leftRayTeleport;
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
        _continuousMoveProvider = GetComponent<ContinuousMoveProviderBase>();
        
    }

    public void SwitchLocomotion(int locomotionValue)
    {
        if(locomotionValue == 0)
        {
            DisableContinuous();
            EnableTeleport();
        }
        else if (locomotionValue == 1)
        {
            DisableTeleport();
            EnableContinuous();
        }
    }

    private void DisableTeleport()
    {
        leftRayTeleport.SetActive(false);
        rightRayTeleport.SetActive(false);
        _teleportationProvider.enabled = false;
    }

    private void EnableTeleport()
    {
        leftRayTeleport.SetActive(true);
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
}
