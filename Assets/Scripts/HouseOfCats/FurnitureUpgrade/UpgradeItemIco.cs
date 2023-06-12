using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeItemIco : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _icoSelectedState;
    [SerializeField] private GameObject _icoAlreadyBought;
    [SerializeField] private GameObject _icoWithPrice;
    [SerializeField] private Text _priceText;

    [SerializeField] private int _itemPrice;

    private int TotalScore;

    public int _itemIcoState;

    public string _upgradeUnitPref;
    public int _unitId;

    public HouseCatUpgradeUnit UpgradeUnit;

    public GameObject IcoSelectedState => _icoSelectedState;
    public GameObject IcoAlreadyBought => _icoAlreadyBought;
    public GameObject IcoWithPrice => _icoWithPrice;
    public Text PriceText => _priceText;


    private void Start()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
    }

    public void UpdateIco(string upgradeUnitPref, int unitId)
    {
        _upgradeUnitPref = upgradeUnitPref;
        _unitId = unitId;

        _itemIcoState = PlayerPrefs.GetInt(upgradeUnitPref + unitId);

        UpdateIcoState(_itemIcoState);
    }

    public void IcoSelfUpdate()
    {
        _itemIcoState = PlayerPrefs.GetInt(_upgradeUnitPref + _unitId);
        UpdateIcoState(_itemIcoState);
    }

    public void UpdateIcoState(int itemIcoState)
    {
        switch (itemIcoState)
        {
            case 2:
                _infoPanel.SetActive(false);
                _icoSelectedState.SetActive(true);
                break;
            case 1:
                _icoWithPrice.SetActive(false);
                _icoSelectedState.SetActive(false);
                _infoPanel.SetActive(true);
                _icoAlreadyBought.SetActive(true);
                break;
            case 0:
                _icoAlreadyBought.SetActive(false);
                _icoSelectedState.SetActive(false);
                _infoPanel.SetActive(true);
                _icoWithPrice.SetActive(true);
                break;
            default:
                _icoAlreadyBought.SetActive(false);
                _icoSelectedState.SetActive(false);
                _infoPanel.SetActive(true);
                _icoWithPrice.SetActive(true);
                break;
        }
    }


    public void BuyItem()
    {
        //Debug.Log("TotalScore  " +TotalScore + "  PlayerPrefs.GetInt(_upgradeUnitPref + _unitId)  " + PlayerPrefs.GetInt(_upgradeUnitPref + _unitId) + "  _itemPrice  " + _itemPrice);

        SoundManager.snd.PlayButtonsSound();
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        if (TotalScore >= _itemPrice && PlayerPrefs.GetInt(_upgradeUnitPref + _unitId) == 0)
        {           
            TotalScore -= _itemPrice;
            PlayerPrefs.SetInt("TotalScore", TotalScore);
            _icoSelectedState.SetActive(true);
            _infoPanel.SetActive(false);

            UpgradeUnit.UpdateItemIcoState();

            PlayerPrefs.SetInt(_upgradeUnitPref + _unitId, 2);
            _itemIcoState = 2;

            if (UpgradeUnit.DependentObject != null)
            { UpgradeUnit.DependentObject.SetActive(true); }            

            UpgradeUnit.UpdateItems(this);
            UpgradeUnit.isActive = true;
        }
        else if (PlayerPrefs.GetInt(_upgradeUnitPref + _unitId) == 1)
        {
            UpgradeUnit.UpdateItemIcoState();

            PlayerPrefs.SetInt(_upgradeUnitPref + _unitId, 2);
            _itemIcoState = 2;

            _infoPanel.SetActive(false);
            _icoAlreadyBought.SetActive(false);
            _icoSelectedState.SetActive(true);
            UpgradeUnit.UpdateItems(this);           
        }

        else if (TotalScore < _itemPrice && PlayerPrefs.GetInt(_upgradeUnitPref + _unitId) == 0)
        {
            UpgradeUnit.NoMoneyCoroutine();
        }

    }



}
