using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondCamera;

    [SerializeField] private GameObject virtualCamera;



    [SerializeField] private Animator animator;

    private bool isCamera1Active = true;

    private void Awake()
    {
        //mainCamera = Camera.main;
    }

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            float newSize = mainCamera.orthographicSize - (zoomSpeed * Time.deltaTime);

            newSize = Mathf.Clamp(newSize, minSize, maxSize);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, Time.deltaTime * zoomSpeed);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            float newSize = mainCamera.orthographicSize + (zoomSpeed * Time.deltaTime);

            newSize = Mathf.Clamp(newSize, minSize, maxSize);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, Time.deltaTime * zoomSpeed);
        }
        if (Input.GetKey(KeyCode.C))
        {
            if(animator.GetBool("AdsSeatWashLook") == false)
            {
                animator.SetBool("AdsSeatWashLook", true);
            }
            else
            {
                animator.SetBool("AdsSeatWashLook", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            isCamera1Active = !isCamera1Active;

            mainCamera.gameObject.SetActive(isCamera1Active);
            secondCamera.gameObject.SetActive(!isCamera1Active);
        }

    }
}
