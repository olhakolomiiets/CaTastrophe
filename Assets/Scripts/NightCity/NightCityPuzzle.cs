using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCityPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> grayscalePuzzlePieces;
    [SerializeField] private Button buttonOpenCity;

    private void OnEnable()
    {
        for (int i = 0; i < grayscalePuzzlePieces.Count; i++)
        {
            if (PlayerPrefs.GetInt("NightCityPuzzlePiece" + i) == 1)
            {
                grayscalePuzzlePieces[i].SetActive(false);
            }
        }
        UnlockStartButton();
    }

    public void UnlockStartButton()
    {
        if (PlayerPrefs.GetInt("NightCityPuzzle") == 6)
        {
            buttonOpenCity.interactable = true;
        }
    }

    public void CloseNightCityPuzzle()
    {
        PlayerPrefs.SetInt("NightCityPuzzle", PlayerPrefs.GetInt("NightCityPuzzle") + 1);
        gameObject.SetActive(false);
    }
}
