using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCat : MonoBehaviour
{
    private ScoreManager sm;
    private int TotalScore;
    private Text priceText;
    [SerializeField] private int[] price;
    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
    void Start()
    {
        TotalScore = sm.GetTotalScore();
    }
    public void buy()
    {
    }
}
