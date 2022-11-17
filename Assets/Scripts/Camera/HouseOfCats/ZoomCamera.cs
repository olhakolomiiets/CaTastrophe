using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax;
    [SerializeField]
    float leftLimit = -58f;
    [SerializeField]
    float rightLimit = 201f;
    [SerializeField]
    float bottomLimit = 2.17f;
    [SerializeField]
    float upperLimit = 42.8f; 
      

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);

        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));

        Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, leftLimit, rightLimit),
        Mathf.Clamp(Camera.main.transform.position.y, bottomLimit, upperLimit), Camera.main.transform.position.z);

       CheckMargins() ;

        //     if (Camera.main.orthographicSize > 13)
        // {
        //     leftLimit = Mathf.Lerp(leftLimit, -30f, Time.deltaTime * 2);
        //     rightLimit = Mathf.Lerp(rightLimit, 180f, Time.deltaTime);
        // }
        // if (Camera.main.orthographicSize < 13)
        // {
        //     leftLimit = Mathf.Lerp(leftLimit, -58f, Time.deltaTime * 2);
        //     rightLimit = Mathf.Lerp(rightLimit, 201f, Time.deltaTime);
        // }



    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
    public void CheckMargins() {
        float margin = Camera.main.orthographicSize;
        Vector3 p = transform.position;
        if(p.y<margin) {
            p.y = margin;
        }
        margin = 240f / 160f * Camera.main.orthographicSize;
        if(p.x<margin) {
            p.x = margin;
        }
        margin = 1240f - margin;
        if(p.x>margin) {
            p.x = margin;
        }
       
        transform.position = p;
    }
 
}
