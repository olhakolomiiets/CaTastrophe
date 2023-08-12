using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour
{
    [SerializeField] private GameObject tipShop;
    [SerializeField] private GameObject tipFood;
    [SerializeField] private GameObject tipToilet;
    [SerializeField] private GameObject tipSofa;
    [SerializeField] private GameObject tipSecondFloor;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject buttonBlockBackground;
    [SerializeField] private GameObject tipFurniture;
    [SerializeField] private GameObject buyFloor;
    [SerializeField] private GameObject buyFloorButton;
    public RectTransform transform1;
    public RectTransform transform2;
    public RectTransform transform3;
    public RectTransform transform4;
    public RectTransform transform5;
    [SerializeField] private Camera mainCamera;


    void Start()
    {
        if (PlayerPrefs.GetInt("CatHouseFirstMessages") == 0)
        {
            tipShop.SetActive(true);
            background.SetActive(true);
            buttonBlockBackground.SetActive(true);
            PlayerPrefs.SetInt("CatHouseFirstMessages", 1);
        }
    }

    public void ActiveTipFood()
    {
        tipShop.SetActive(false);
        tipFood.SetActive(true);
    }
    public void ActiveTipToilet()
    {
        mainCamera.transform.DOLocalMove(transform1.position, 0.7f);
        StartCoroutine(ChangeTips(tipFood, tipToilet));
    }

    public void ActiveTipSofa()
    {
        mainCamera.transform.DOLocalMove(transform2.position, 0.7f);
        StartCoroutine(ChangeTips(tipToilet, tipSofa));
    }

    public void ActiveTipFurniture()
    {
        mainCamera.transform.DOLocalMove(transform5.position, 0.7f);
        StartCoroutine(ChangeTips(tipSofa, tipFurniture));
    }
    public void ActiveTip2Floor()
    {       
        mainCamera.transform.DOLocalMove(transform3.position, 0.7f);
        StartCoroutine(Tip2Floor());
        buyFloorButton.SetActive(false);
    }
    public void ExitTip()
    {
        tipSecondFloor.SetActive(false);
        background.SetActive(false);
        buttonBlockBackground.SetActive(false);
        buyFloor.SetActive(false);
        mainCamera.transform.DOLocalMove(transform4.position, 0.7f);
        buyFloorButton.SetActive(true);

    }
    IEnumerator ChangeTips(GameObject tipActive, GameObject tipInactive)
    {
        tipActive.SetActive(false);
        yield return new WaitForSeconds(0.65f);
        tipInactive.SetActive(true);
    }
    IEnumerator Tip2Floor()
    {
        tipFurniture.SetActive(false);
        yield return new WaitForSeconds(0.65f);
        buyFloor.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        tipSecondFloor.SetActive(true);
    }
}
