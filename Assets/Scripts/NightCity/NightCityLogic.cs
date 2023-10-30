using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCityLogic : MonoBehaviour
{
    [Header("Player")]
    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float speed;

    [SerializeField] private Slider slider;
    [SerializeField] private GameTimer timer;
    [SerializeField] private CityWindowsController windowsController;

    private void Start()
    {
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        controller.enemy5 = "EnemyGround";
        controller.jumpForce = jumpForce;
        controller.normalSpeed = speed;
        controller.transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

    }

    public void UpdateSlider(float addToSlider)
    {
        slider.value = slider.value + addToSlider;
        if (slider.value >= 100f)
        {
            timer.StopTimer();
        }
        else
        {
            windowsController.UpdateThrowingFrequency((int)slider.value);
        }
    }

}
