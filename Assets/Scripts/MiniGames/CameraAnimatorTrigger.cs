using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimatorTrigger : MonoBehaviour
{
    public Camera specificCamera; 
    public List<Animator> objectAnimators = new List<Animator>();
    [SerializeField] private string animateTrigger;

    private void Update()
    {
        foreach (Animator objectAnimator in objectAnimators)
        {
            if (IsObjectVisibleToCamera(objectAnimator))
            {
                objectAnimator.SetTrigger(animateTrigger);
            }
        }
    }

    private bool IsObjectVisibleToCamera(Animator objectAnimator)
    {
        if (specificCamera == null || objectAnimator == null)
        {
            return false;
        }

        // Calculate the object's position in the camera's viewport.
        Vector3 objectViewportPoint = specificCamera.WorldToViewportPoint(objectAnimator.transform.position);

        // Check if the object is within the camera's viewport.
        return objectViewportPoint.x >= 0 && objectViewportPoint.x <= 1 &&
               objectViewportPoint.y >= 0 && objectViewportPoint.y <= 1 &&
               objectViewportPoint.z >= 0;
    }
}
