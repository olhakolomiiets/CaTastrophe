using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPointManager : MonoBehaviour
{
    public int activePlayer;
    public GameObject[] players;

    private void Awake()
    {
        players[PlayerPrefs.GetInt("Player")].SetActive(true);
    }
}
