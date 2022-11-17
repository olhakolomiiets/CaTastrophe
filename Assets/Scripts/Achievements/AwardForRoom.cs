using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AwardForRoom : MonoBehaviour
{
    [SerializeField] private string AchieveNamePref;
    [SerializeField] private Slider slider;
    // private Text digits;
    // private Text digits2;
    // private Text digits3;
    public int TargetNumber;
    public int TargetNumber2;
    public int TargetNumber3;
    public int realNumber;
    private Image iconSprite;
    public bool done;
    public bool stage1complete;
    public bool stage2complete;
    public bool stage3complete;
    [SerializeField] private Text numberReal;
    [SerializeField] private Text numberTarget;
    [SerializeField] private GameObject numbers;
    [SerializeField] private GameObject icons;
    [SerializeField] private GameObject iconWin;
    [SerializeField] private Text numberTotal;



    void Start()
    {
        // Initialization();
        // SliderValue();
        // // digits.text = GetAchieveScore().ToString() + "/" + TargetNumber;
        // // digits2.text = GetAchieveScore().ToString() + "/" + TargetNumber2;
        // // digits3.text = GetAchieveScore().ToString() + "/" + TargetNumber3;
        // if (realNumber < TargetNumber)
        // {
        //     numberTarget.text = TargetNumber.ToString();
        // }
        // if (realNumber >= TargetNumber)
        // {
        //     numberTarget.text = TargetNumber2.ToString();
        // }
        // if (realNumber >= TargetNumber2)
        // {
        //     numberTarget.text = TargetNumber3.ToString();
        // }

        // if (realNumber >= TargetNumber3)
        // {
        //     numbers.SetActive(false);
        //     icons.SetActive(false);
        //     slider.gameObject.SetActive(false);
        //     iconWin.SetActive(true);
        //     numberTotal.text = GetAchieveScore().ToString();
        // }
    }
    private void OnEnable()
    {
        Initialization();
        SliderValue();

        if (realNumber < TargetNumber)
        {
            numberTarget.text = TargetNumber.ToString();
        }
        if (realNumber >= TargetNumber)
        {
            numberTarget.text = TargetNumber2.ToString();
        }
        if (realNumber >= TargetNumber2)
        {
            numberTarget.text = TargetNumber3.ToString();
        }

        if (realNumber >= TargetNumber3)
        {
            numbers.SetActive(false);
            icons.SetActive(false);
            slider.gameObject.SetActive(false);
            iconWin.SetActive(true);
            numberTotal.text = GetAchieveScore().ToString();
        }
    }
    public int GetAchieveScore()
    {
        if (PlayerPrefs.HasKey(AchieveNamePref))
        {
            return PlayerPrefs.GetInt(AchieveNamePref);
        }
        else
        {
            return 0;
        }
    }
    public void SliderValue()
    {
        if (Settings.profile)
        {
            realNumber = GetAchieveScore();
            slider.value = realNumber;
            numberReal.text = realNumber.ToString();
        }
    }
    private void Update()
    {
        realNumber = GetAchieveScore();
    }
    private void Initialization()
    {
        slider.maxValue = TargetNumber3;
    }
}