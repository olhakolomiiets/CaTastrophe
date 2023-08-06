using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastOpenHouse : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    
    void OnEnable()
    {
        int levelComplete = PlayerPrefs.GetInt("LastOpenHouse");

        switch (levelComplete)
        {
            case 0:
                rectTransform.anchoredPosition = new Vector2(5212, 0);
                break;
            case 2:
                rectTransform.anchoredPosition = new Vector2(5212, 0);
                break;
            case 3:
                rectTransform.anchoredPosition = new Vector2(4862, 0);
                break;
            case 4:
                rectTransform.anchoredPosition = new Vector2(4254, 0);
                break;
            case 5:
                rectTransform.anchoredPosition = new Vector2(2858, 0);
                break;
            case 6:
                rectTransform.anchoredPosition = new Vector2(2233, 0);
                break;
            case 7:
                rectTransform.anchoredPosition = new Vector2(435, 0);
                break;
            case 8:
                rectTransform.anchoredPosition = new Vector2(-292, 0);
                break;
            case 9:
                rectTransform.anchoredPosition = new Vector2(-1067, 0);
                 break;
            case 10:
                rectTransform.anchoredPosition = new Vector2(-1992, 0);
                 break;
            case 11:
                rectTransform.anchoredPosition = new Vector2(-4110, 0);
                 break;
            case 12:
                rectTransform.anchoredPosition = new Vector2(-4791, 0);
                 break;
            default:
            rectTransform.anchoredPosition = new Vector2(5212, 0);
            break;
         }       
    }
}
