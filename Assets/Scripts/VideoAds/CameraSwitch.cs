using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
    private int activeCameraIndex = 0;

    private void Start()
    {
        // Set the priority of the first camera to be the highest.
        cameras[0].Priority = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        // Set the priority of the current camera to be the lowest.
        cameras[activeCameraIndex].Priority = 0;

        // Increment the index to switch to the next camera.
        activeCameraIndex++;
        if (activeCameraIndex >= cameras.Count)
        {
            activeCameraIndex = 0;
        }

        // Set the priority of the new camera to be the highest.
        cameras[activeCameraIndex].Priority = 10;
    }
}
