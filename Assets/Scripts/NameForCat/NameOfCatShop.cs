using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameOfCatShop : MonoBehaviour
{

    public string nameOfCat;
    public string savedNameCat;
    public Text inputText;
    private int catIndex;
    public void SetName()
    {
        if (!string.IsNullOrEmpty(inputText.text))
        {
            catIndex = PlayerPrefs.GetInt("Player");
            savedNameCat = inputText.text.ToUpper();
            PlayerPrefs.SetString("nameOfCat" + catIndex, savedNameCat);
        }
        else
        {
            catIndex = PlayerPrefs.GetInt("Player");
            savedNameCat = "Cat".ToString().ToUpper();
            PlayerPrefs.SetString("nameOfCat" + catIndex, savedNameCat); 
        }        
    }
}
