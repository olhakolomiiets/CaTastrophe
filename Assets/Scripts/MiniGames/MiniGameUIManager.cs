using UnityEngine;
using UnityEngine.UI;

public class MiniGameUIManager : MonoBehaviour
{
    [Space(5)]
    [SerializeField] private GameObject infoWindow;
    [SerializeField] private Text gameName;
    [SerializeField] private Text infoText;

    [Space(5)]
    [SerializeField] private GameObject BirdCatcherStartWindow;
    [SerializeField] private int needLevel2;

    [Space(5)]
    [SerializeField] private GameObject BasketballStartWindow;
    [SerializeField] private int needLevel3;

    [Space(5)]
    [SerializeField] private GameObject RoofRunnerStartWindow;
    [SerializeField] private int needLevel4;

    [Space(5)]
    [SerializeField] private GameObject WallStartWindow;
    [SerializeField] private int needLevel5;

    [Space(5)]
    [SerializeField] private GameObject HeadBasketballStartWindow;
    [SerializeField] private int needLevel6;

    private int _levelIndex;

    public void OpenMiniGame(int miniGameNumber)
    {        
        switch (miniGameNumber)
        {
            case 1:
                _levelIndex = PlayerPrefs.GetInt("LevelStar1" + needLevel2);
                if (_levelIndex >= 1)
                {
                    BirdCatcherStartWindow.SetActive(true);
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
                { BasketballStartWindow.SetActive(true); }
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
                { RoofRunnerStartWindow.SetActive(true); }
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
                { WallStartWindow.SetActive(true); }
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
                { HeadBasketballStartWindow.SetActive(true); }
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
