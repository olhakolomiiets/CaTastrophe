using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achieve : MonoBehaviour
{
    [SerializeField]
    private string AchieveNamePref;
    private Slider slider;
    private Slider slider2;
    private Slider slider3;
    private Text digits;
    private Text digits2;
    private Text digits3;
    public int TargetNumber;
    public int TargetNumber2;
    public int TargetNumber3;
    public int realNumber;
    private GameObject title;
    private GameObject title2;
    private GameObject title3;
    private GameObject description;
    private GameObject description2;
    private GameObject description3;
    private GameObject sliderObj;
    private GameObject sliderObj2;
    private GameObject sliderObj3;
    private GameObject digitsObj;
    private GameObject digitsObj2;
    private GameObject digitsObj3;
    public GameObject winText;
    private GameObject icon;
    private GameObject icon2;
    private GameObject icon3;
    private Image iconSprite;
    public Sprite win_Sprite;
    public Sprite lock_Sprite;
    public bool done;
    public bool stage1complete;
    public bool stage2complete;
    public bool stage3complete;
    void Start()
    {
        Initialization();
        SliderValue();
        digits.text = GetAchieveScore().ToString() + "/" + TargetNumber;
        digits2.text = GetAchieveScore().ToString() + "/" + TargetNumber2;
        digits3.text = GetAchieveScore().ToString() + "/" + TargetNumber3;
        if (realNumber >= TargetNumber && !stage1complete)
        {
            iconSprite = icon.GetComponent<Image>();
            iconSprite.sprite = win_Sprite;
            title.SetActive(false);
            description.SetActive(false);
            sliderObj.SetActive(false);
            digitsObj.SetActive(false);
            title2.SetActive(true);
            description2.SetActive(true);
            sliderObj2.SetActive(true);
            digitsObj2.SetActive(true);
            stage1complete = true;
        }
        if (realNumber >= TargetNumber2 && !stage2complete)
        {
            icon.SetActive(false);
            title2.SetActive(false);
            description2.SetActive(false);
            sliderObj2.SetActive(false);
            digitsObj2.SetActive(false);
            title3.SetActive(true);
            description3.SetActive(true);
            sliderObj3.SetActive(true);
            digitsObj3.SetActive(true);
            icon2.SetActive(true);
            stage2complete = true;
        }

        if (realNumber >= TargetNumber3 && !stage3complete)
        {
            title3.SetActive(false);
            description3.SetActive(false);
            sliderObj3.SetActive(false);
            digitsObj3.SetActive(false);
            icon2.SetActive(false);
            icon3.SetActive(true);
            winText.SetActive(true);
            stage3complete = true;
            done = true;
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
            slider2.value = realNumber;
            slider3.value = realNumber;
        }
    }
    private void Update()
    {
        realNumber = GetAchieveScore();
    }
    private void Initialization()
    {
        icon = this.gameObject.transform.GetChild(1).gameObject;
        title = this.gameObject.transform.GetChild(2).gameObject;
        description = this.gameObject.transform.GetChild(3).gameObject;
        sliderObj = this.gameObject.transform.GetChild(4).gameObject;
        slider = sliderObj.GetComponent<Slider>();
        digitsObj = this.gameObject.transform.GetChild(5).gameObject;
        digits = digitsObj.GetComponent<Text>();
        winText = this.gameObject.transform.GetChild(6).gameObject;
        icon2 = this.gameObject.transform.GetChild(7).gameObject;
        title2 = this.gameObject.transform.GetChild(8).gameObject;
        description2 = this.gameObject.transform.GetChild(9).gameObject;
        sliderObj2 = this.gameObject.transform.GetChild(10).gameObject;
        slider2 = sliderObj2.GetComponent<Slider>();
        digitsObj2 = this.gameObject.transform.GetChild(11).gameObject;
        digits2 = digitsObj2.GetComponent<Text>();
        icon3 = this.gameObject.transform.GetChild(12).gameObject;
        title3 = this.gameObject.transform.GetChild(13).gameObject;
        description3 = this.gameObject.transform.GetChild(14).gameObject;
        sliderObj3 = this.gameObject.transform.GetChild(15).gameObject;
        slider3 = sliderObj3.GetComponent<Slider>();
        digitsObj3 = this.gameObject.transform.GetChild(16).gameObject;
        digits3 = digitsObj3.GetComponent<Text>();
    }
}