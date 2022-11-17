using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoveStopForHouse : StateMachineBehaviour
{
    private CowController controller ;
    private EventTrigger buttonLeft;
    private EventTrigger buttonRight;
    private Button ButtonJump;

    private float mySpeed;

    

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        buttonLeft = GameObject.FindGameObjectWithTag("ButtonLeft").GetComponent<EventTrigger>();
        buttonRight = GameObject.FindGameObjectWithTag("ButtonRight").GetComponent<EventTrigger>();
        ButtonJump = GameObject.FindGameObjectWithTag("ButtonJump").GetComponent<Button>();
        
        controller.enabled = false;
        buttonLeft.enabled = false;
        buttonRight.enabled = false;
        ButtonJump.enabled = false;
      
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("isRunning", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    animator.SetBool("isRunning", false);
      controller.enabled = true;
        buttonLeft.enabled = true;
        buttonRight.enabled = true;
        ButtonJump.enabled = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
   
}
