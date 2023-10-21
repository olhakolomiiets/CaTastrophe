using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUIManager : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private GameObject infoWindow;
    [SerializeField] private Text gameName;
    [SerializeField] private Text infoText;

    [Header("Bird Catcher")]
    [SerializeField] private GameObject birdCatcherStartWindow;
    [SerializeField] private int needLevel2;
    [SerializeField] private GameObject birdCatcheMsg;
    [SerializeField] private Text birdCatcherFirstTxt;
    [SerializeField] private Text birdCatcherReminderTxt;
    private static bool _wasReminderShown1;

    [Header("Basketball")]
    [SerializeField] private GameObject basketballStartWindow;
    [SerializeField] private int needLevel3;
    [SerializeField] private GameObject basketballMsg;
    [SerializeField] private Text basketballFirstTxt;
    [SerializeField] private Text basketballReminderTxt;
    private static bool _wasReminderShown2;

    [Header("Roof Runner")]
    [SerializeField] private GameObject runnerStartWindow;
    [SerializeField] private int needLevel4;
    [SerializeField] private GameObject runnerMsg;
    [SerializeField] private Text runnerFirstTxt;
    [SerializeField] private Text runnerReminderTxt;
    private static bool _wasReminderShown3;

    [Header("Wall")]
    [SerializeField] private GameObject wallStartWindow;
    [SerializeField] private int needLevel5;
    [SerializeField] private GameObject wallMsg;
    [SerializeField] private Text wallFirstTxt;
    [SerializeField] private Text wallReminderTxt;
    private static bool _wasReminderShown4;

    [Header("Basketball Head")]
    [SerializeField] private GameObject headBallStartWindow;
    [SerializeField] private int needLevel6;
    [SerializeField] private GameObject headBallMsg;
    [SerializeField] private Text headBallFirstTxt;
    [SerializeField] private Text headBallReminderTxt;
    private static bool _wasReminderShown5;

    private int _levelIndex;

    private List<GameObject> _msg;
    private List<Text> _firstTxt;
    private List<Text> _reminders;
    private List<int> _levels;

    private static bool[] _wasReminderShown = new bool[] { _wasReminderShown1, _wasReminderShown2, _wasReminderShown3, _wasReminderShown4, _wasReminderShown5 };

    private void Awake()
    {
        _msg = new List<GameObject> { birdCatcheMsg, basketballMsg, runnerMsg, wallMsg, headBallMsg };
        _firstTxt = new List<Text> { birdCatcherFirstTxt, basketballFirstTxt, runnerFirstTxt, wallFirstTxt, headBallFirstTxt };
        _reminders = new List<Text> { birdCatcherReminderTxt, basketballReminderTxt, runnerReminderTxt, wallReminderTxt, headBallReminderTxt };
        _levels = new List<int> { needLevel2, needLevel3, needLevel4, needLevel5, needLevel6 };

        //_wasReminderShown = new bool[] { _wasReminderShown1, _wasReminderShown2, _wasReminderShown3, _wasReminderShown4, _wasReminderShown5 };
    }

    private void Start()
    {
        DateTime now = DateTime.Now;
        int day = now.Day;


        for (int i = 0; i < _msg.Count; i++)
        {
            if (PlayerPrefs.GetInt("LevelStar1" + _levels[i]) >= 1)
            {               
                if (PlayerPrefs.GetInt("MiniGameFirstMessage" + _levels[i]) == 0)
                {
                    _msg[i].SetActive(true);
                    _firstTxt[i].gameObject.SetActive(true);
                }                    
                else if (day % 2 != 0 && i % 2 !=0 && !_wasReminderShown[i])
                {
                    _msg[i].SetActive(true);
                    _reminders[i].gameObject.SetActive(true);
                    _wasReminderShown[i] = true;
                }                
                else if (day % 2 == 0 && i % 2 == 0 && !_wasReminderShown[i])
                {
                    _msg[i].SetActive(true);
                    _reminders[i].gameObject.SetActive(true);
                    _wasReminderShown[i] = true;
                }
            }
        }
    }

    public void OpenMiniGame(int miniGameNumber)
    {        
        switch (miniGameNumber)
        {
            case 1:
                _levelIndex = PlayerPrefs.GetInt("LevelStar1" + needLevel2);
                if (_levelIndex >= 1)
                {
                    birdCatcherStartWindow.SetActive(true);
                    PlayerPrefs.SetInt("MiniGameFirstMessage" + needLevel2, 1);
                }                    
                else
                {
                    infoWindow.SetActive(true);
                    gameName.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("BirdCatcherTitle")}";
                    infoText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("MiniGameMessage")} {"1"}";
                }                               
                break;
            case 2:
                _levelIndex = PlayerPrefs.GetInt("LevelStar1" + needLevel3);
                if (_levelIndex >= 1)
                { 
                    basketballStartWindow.SetActive(true);
                    PlayerPrefs.SetInt("MiniGameFirstMessage" + needLevel3, 1);
                }
                else
                {
                    infoWindow.SetActive(true);
                    gameName.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("BasketballTitle")}";
                    infoText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("MiniGameMessage")} {"2"}";
                }
                break;
            case 3:
                _levelIndex = PlayerPrefs.GetInt("LevelStar1" + needLevel4);
                if (_levelIndex >= 1)
                {
                    runnerStartWindow.SetActive(true);
                    PlayerPrefs.SetInt("MiniGameFirstMessage" + needLevel4, 1);
                }
                else
                {
                    infoWindow.SetActive(true);
                    gameName.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("RoofRunnerTitle")}";
                    infoText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("MiniGameMessage")} {"3"}";
                }
                break;
            case 4:
                _levelIndex = PlayerPrefs.GetInt("LevelStar1" + needLevel5);
                if (_levelIndex >= 1)
                { 
                    wallStartWindow.SetActive(true);
                    PlayerPrefs.SetInt("MiniGameFirstMessage" + needLevel5, 1);
                }
                else
                {
                    infoWindow.SetActive(true);
                    gameName.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("WallTitle")}";
                    infoText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("MiniGameMessage")} {"4"}";
                }
                break;
            case 5:
                _levelIndex = PlayerPrefs.GetInt("LevelStar1" + needLevel6);
                if (_levelIndex >= 1)
                { 
                    headBallStartWindow.SetActive(true);
                    PlayerPrefs.SetInt("MiniGameFirstMessage" + needLevel6, 1);
                }
                else
                {
                    infoWindow.SetActive(true);
                    gameName.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("BasketballHeadTitle")}";
                    infoText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("MiniGameMessage")} {"5"}";
                }
                break;
        }
    }
}
