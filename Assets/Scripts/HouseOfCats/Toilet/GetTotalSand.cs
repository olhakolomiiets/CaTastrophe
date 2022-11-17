using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTotalSand : MonoBehaviour
{
    public int TotalFood;
    // private ScoreManager sm;
    public Text foodTotalText;
    private void Awake()
    {
        UpdateFood();
    }
    private void Update()
    {
        // StartCoroutine(GetFood());
        UpdateFood();
    }
    private IEnumerator GetFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdateFood();
        }
    }
    private void UpdateFood()
    {
        TotalFood = PlayerPrefs.GetInt("TotalSand");
        // sm = FindObjectOfType<ScoreManager>();
        foodTotalText.text = TotalFood.ToString();
    }
}