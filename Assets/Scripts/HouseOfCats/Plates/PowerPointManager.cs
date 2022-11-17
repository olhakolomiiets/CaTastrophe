using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPointManager : MonoBehaviour
{
    public float pointsWhenYouWereAbsent = 0f;
    public FloatSO[] powerCats;
    public int activePlayer;
    public GameObject[] players;
    private EnergyCat energyCat;
    [SerializeField]
    private string prefNamePointManager;

    private void Awake()
    {
        PlayerPrefs.SetInt("PowerPointStreet", 1);
        if (PlayerPrefs.GetInt(prefNamePointManager) == 1)
        {
            players[PlayerPrefs.GetInt("Player")].SetActive(true);           
            energyCat = players[PlayerPrefs.GetInt("Player")].transform.parent.gameObject.GetComponent<EnergyCat>();
            // energyCat.UpdateEnergy();
            // Debug.Log("333333333333333333333333333333 I updated SLIDER n AWAKE --- " + energyCat.name);  
        }
    }
    void Start()
    {
        if (PlayerPrefs.GetInt(prefNamePointManager) == 1)
        {
            energyCat.UpdateEnergy();
            Debug.Log("333333333333333333333333333333 I updated SLIDER on START --- " + energyCat.name);  
        }
        // Debug.Log("333333333333333333333333333333 I will update SLIDER --- " + energyCat.name);       
    }

    void OnApplicationQuit()
    {
        pointsWhenYouWereAbsent = 0f;
    }

    public void SubstractPower()
    {
        // // powerCats[PlayerPrefs.GetInt("Player")].ChangeAmountBy(-amount);
        // var x = powerCats[PlayerPrefs.GetInt("Player")].Value;
        // int y = Mathf.RoundToInt(x);
        // Debug.Log("333333333333333333333333333333 powerCats Value before substract" + x);
        int i = PlayerPrefs.GetInt("Player");
        Debug.Log("3333333333333333333333333333 " + "totalEnergyCat" + i + " = " + PlayerPrefs.GetFloat("totalEnergyCat" + i));

        energyCat = players[i].transform.parent.gameObject.GetComponent<EnergyCat>();
        energyCat.UseEnergy();

        var e = energyCat.totalEnergy;

        PlayerPrefs.SetFloat("totalEnergyCat" + i, (int)e);
        // Debug.Log("333333333333333333333333333333 powerCats Value after substract" + e);
        // energyCat.UpdateEnergy();
    }
}
