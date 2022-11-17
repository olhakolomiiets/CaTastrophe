﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameOfCatForGame : MonoBehaviour
{

    public string nameOfCat;
    public Text loadedName;

    private void Start()
    {
        int catIndex = PlayerPrefs.GetInt("Player");
        nameOfCat = PlayerPrefs.GetString("nameOfCat" + catIndex);
        loadedName.text = nameOfCat;
    }
}
