using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achieve12 : MonoBehaviour
{
    [SerializeField]
    private string AchieveNamePref;
    public int TargetNumber;
    public int TargetNumber2;
    public int TargetNumber3;
    public int realNumber;
    private GameObject title;
    private GameObject title2;
    private GameObject title3;
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
        realNumber = GetAchieveScore();
        Initialization();
        if (realNumber >= TargetNumber && !stage1complete)
        {
            iconSprite = icon.GetComponent<Image>();
            iconSprite.sprite = win_Sprite;
            title.SetActive(false);
            title2.SetActive(true);
            stage1complete = true;
        }
        if (realNumber >= TargetNumber2 && !stage2complete)
        {
            icon.SetActive(false);
            title.SetActive(false);
            title2.SetActive(false);
            title3.SetActive(true);
            icon2.SetActive(true);
            stage2complete = true;
        }
        if (realNumber >= TargetNumber3 && !stage3complete)
        {
            title.SetActive(false);
            title3.SetActive(false);
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
    private void Update()
    {
        realNumber = GetAchieveScore();
    }
    private void Initialization()
    {
        icon = this.gameObject.transform.GetChild(2).gameObject;
        title = this.gameObject.transform.GetChild(3).gameObject;
        winText = this.gameObject.transform.GetChild(1).gameObject;
        icon2 = this.gameObject.transform.GetChild(4).gameObject;
        title2 = this.gameObject.transform.GetChild(5).gameObject;
        icon3 = this.gameObject.transform.GetChild(6).gameObject;
        title3 = this.gameObject.transform.GetChild(7).gameObject;
    }
}
