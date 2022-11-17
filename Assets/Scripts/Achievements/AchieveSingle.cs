using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchieveSingle : MonoBehaviour
{
    [SerializeField]
    private string AchieveNamePref;
    private Slider slider;
    private Text digits;
    public int TargetNumber;
    public int realNumber;
    private GameObject title;
    private GameObject description;
    private GameObject sliderObj;
    private GameObject digitsObj;
    public GameObject winText;
    private GameObject icon;
    private Image iconSprite;
    public Sprite win_Sprite;
    public Sprite lock_Sprite;
    public bool done;

    void Start()
    {
        Initialization();
        SliderValue();
        digits.text = GetAchieveScore().ToString() + "/" + TargetNumber;
        if (realNumber >= TargetNumber)
        {
            iconSprite = icon.GetComponent<Image>();
            iconSprite.sprite = win_Sprite;
            description.SetActive(false);
            sliderObj.SetActive(false);
            digitsObj.SetActive(false);
            winText.SetActive(true);
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
    }
}
