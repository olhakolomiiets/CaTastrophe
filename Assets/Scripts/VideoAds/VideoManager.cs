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

        if (Input.GetKey(KeyCode.V))
        {
            if (animator.GetBool("AdsSeatWashLegLook") == false)
            {
                animator.SetBool("AdsSeatWashLegLook", true);
            }
            else
            {
                animator.SetBool("AdsSeatWashLegLook", false);
            }
        }

        if (Input.GetKey(KeyCode.B))
        {
            if (animator.GetBool("lowPower2") == false)
            {
                animator.SetBool("lowPower2", true);
            }
            else
            {
                animator.SetBool("lowPower2", false);
            }
        }

        if (Input.GetKey(KeyCode.N))
        {
            if (animator.GetBool("lowPower4") == false)
            {
                animator.SetBool("lowPower4", true);
            }
            else
            {
                animator.SetBool("lowPower4", false);
            }
        }

        if (Input.GetKey(KeyCode.M))
        {
            if (animator.GetBool("lowPower5") == false)
            {
                animator.SetBool("lowPower5", true);
            }
            else
            {
                animator.SetBool("lowPower5", false);
            }
        }
    }
}
