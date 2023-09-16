using UnityEngine;

public class ParabolicThrow : MonoBehaviour
{
    public GameObject objectToThrow;
    public Transform startPoint;
    public Transform endPoint;
    public float throwHeight = 5.0f;
    public float throwDuration = 2.0f;
    public float minRotationSpeed = 0.0f;
    public float maxRotationSpeed = 180.0f;

    private float throwStartTime;

    private GameObject thrownObject;
    private Vector3 initialPosition;
    private float rotationSpeedZ; // Only rotation speed along the Z-axis

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowObject();
        }

        if (thrownObject != null)
        {
            UpdateObjectPosition();
        }
    }

    private void ThrowObject()
    {
        thrownObject = Instantiate(objectToThrow, startPoint.position, Quaternion.identity);
        initialPosition = thrownObject.transform.position;
        throwStartTime = Time.time;

        // Generate random rotation speed along the Z-axis
        rotationSpeedZ = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private void UpdateObjectPosition()
    {
        float timeElapsed = Time.time - throwStartTime;
        float t = timeElapsed / throwDuration;

        if (t >= 1.0f)
        {
            Destroy(thrownObject);
            thrownObject = null;
            return;
        }

        Vector3 direction = endPoint.position - startPoint.position;
        direction.y = 0.0f;
        float horizontalDistance = direction.magnitude;

        float horizontalVelocity = horizontalDistance / throwDuration;
        float verticalVelocity = (throwHeight * 2.0f) / throwDuration;

        float currentHorizontalPosition = horizontalVelocity * timeElapsed;
        float currentVerticalPosition = verticalVelocity * timeElapsed - 0.5f * Mathf.Abs(Physics.gravity.y) * timeElapsed * timeElapsed;

        Vector3 newPosition = initialPosition + (direction.normalized * currentHorizontalPosition);
        newPosition.y += currentVerticalPosition;

        thrownObject.transform.position = newPosition;

        // Apply rotation to the thrown object along the Z-axis
        Vector3 rotation = new Vector3(0.0f, 0.0f, rotationSpeedZ * Time.deltaTime);
        thrownObject.transform.Rotate(rotation);
    }
}