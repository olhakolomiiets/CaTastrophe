using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
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

    }
}
