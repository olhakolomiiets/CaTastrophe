using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam1;
    [SerializeField] private CinemachineVirtualCamera vCam2;

    private bool firstCamera = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchPriority();
        }
    }
    private void SwitchPriority()
    {
        if(firstCamera)
        {
            vCam1.Priority = 0;
            vCam2.Priority = 1;
        }
        else
        {
            vCam1.Priority = 1;
            vCam2.Priority = 0;
        }
        firstCamera = !firstCamera;
    }

}
