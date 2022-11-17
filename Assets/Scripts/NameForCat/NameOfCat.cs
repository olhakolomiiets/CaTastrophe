using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameOfCat : MonoBehaviour
{

    public string nameOfCat;
    public string savedNameCat;
    public Text inputText;
    public Text loadedName;
    private int catIndex;
    public static event HouseCat.UpdateNameDelegate UpdateEvent;

    private void Start()
    {
        nameOfCat = PlayerPrefs.GetString("nameOfCat" + catIndex);
        loadedName.text = nameOfCat;
    }
    public void SetName()
    {
        savedNameCat = inputText.text.ToUpper();
        PlayerPrefs.SetString("nameOfCat" + catIndex, savedNameCat);
        nameOfCat = PlayerPrefs.GetString("nameOfCat" + catIndex);
        loadedName.text = nameOfCat;
        UpdateEvent();
    }
    public void changeCatIndex(int index)
    {
        catIndex = index;
    }
}
