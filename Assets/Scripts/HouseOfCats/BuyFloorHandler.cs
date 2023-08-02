using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFloorHandler : MonoBehaviour, IClickable
{
    public string ppnamePower;
    public int price;
    public int purch;
    private int TotalScore;
    [SerializeField] private GameObject floor1;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject toilet;
    [SerializeField] private GameObject foodPlate;
    [SerializeField] private GameObject upgadeUnits;
    public static event FloorActiveToiletHandler.ToiletActiveFloorDelegate FloorBought;
    public static event FloorActiveFoodHandler.PlateActiveFloorDelegate FloorBoughtPlate;
    [SerializeField] private GameObject noMoneyTag;

    private void Awake()
    {
        purch = PlayerPrefs.GetInt(ppnamePower, 0);
        if (purch == 1)
        {
            floor1.SetActive(false);
            button.SetActive(false);
            foodPlate.SetActive(true);
            toilet.SetActive(true);
        }
    }
    void Start()
    {
    }

    public void Click()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        purch = PlayerPrefs.GetInt(ppnamePower, 0);
        if (TotalScore >= price && purch == 0)
        {
            PlayerPrefs.SetInt(ppnamePower, 1);
            floor1.SetActive(false);
            button.SetActive(false);
            foodPlate.SetActive(true);
            toilet.SetActive(true);
            upgadeUnits.SetActive(true);
            TotalScore = TotalScore - price;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
            FloorBought?.Invoke();
            FloorBoughtPlate?.Invoke();
        }
        else if (TotalScore < price)
        {
            StartCoroutine(NoMoney());
        }
        purch = PlayerPrefs.GetInt(ppnamePower, 0);
    }

    IEnumerator NoMoney()
    {

        noMoneyTag.SetActive(true);

        yield return new WaitForSeconds(3f);

        noMoneyTag.SetActive(false);
    }
}
