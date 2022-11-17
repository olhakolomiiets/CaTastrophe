using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{

    bool fix = false;
    private Animator playerAnimator;
    public RuntimeAnimatorController playerContr;
    public PlayableDirector director;
    private GameObject player;
    private bool controls;
    private Text timer;

    private void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerContr = playerAnimator.runtimeAnimatorController;
        controls = playerAnimator.gameObject.GetComponent<CowController>().enabled = false;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        timer.enabled = false;
    }
    void Update()
    {
        if (director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnimator.runtimeAnimatorController = playerContr;
            controls = playerAnimator.gameObject.GetComponent<CowController>().enabled = true;
            timer.enabled = true;
        }
    }
}
