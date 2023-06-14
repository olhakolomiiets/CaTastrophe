using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HouseCatUpgradeUnit : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject _furnitureUpgradeUI;
    [SerializeField] private RectTransform _scrollGridGroup;
    [SerializeField] private GameObject _emptyPlaceImg;
    [SerializeField] private GameObject noMoneyTag;
    [SerializeField] private Vector3 _itemForScale;
    public List<UpgradeItem> _allUnitItems;
    public List<UpgradeItemIco> _allIcoItems;
    public int unitId;

    [Header("Animation")]
    [SerializeField] private GameObject _ItemAnimUI;

    [Header("Dependent Object")]
    [SerializeField] private GameObject _dependentObject;
    public GameObject DependentObject => _dependentObject;

    public bool isActive;

    private float _scrollGridGroupXAxis;


    private void Start()
    {
        CheckUnit();
    }

    public void CheckUnit()
    {
        for (int i = 0; i < _allUnitItems.Count; i++)
        {
            if (PlayerPrefs.GetInt(_allUnitItems[i].upgradeUnitPref + unitId) == 2)
            {
                _allUnitItems[i].gameObject.SetActive(true);
                _emptyPlaceImg.SetActive(false);

                isActive = true;

                if (_dependentObject != null)
                {
                    _dependentObject.SetActive(true);
                }
            }
            else if (PlayerPrefs.GetInt(_allUnitItems[i].upgradeUnitPref + unitId) <= 0)
            {
                _allUnitItems[i].gameObject.SetActive(false);

                if (_dependentObject != null && !isActive)
                {
                    _dependentObject.SetActive(false);
                }
            }
        }
    }

    public void EnableFurnitureUpgradeUI()
    {
        if (_furnitureUpgradeUI.activeSelf)
        {            
            _furnitureUpgradeUI.SetActive(false);

        }
        else
        {
            DestroyGridGroupChild();
            _allIcoItems.Clear();

            if (_allIcoItems.Count <= 0)
            {              
                for (int i = 0; i < _allUnitItems.Count; i++)
                {
                    GameObject newPrefabInstance = Instantiate(_allUnitItems[i].UpgradeItemIcoPrefab, _scrollGridGroup);
                    var IcoInstance = newPrefabInstance.GetComponent<UpgradeItemIco>();
                    IcoInstance.UpdateIco(_allUnitItems[i].upgradeUnitPref, unitId);
                    IcoInstance.UpgradeUnit = this;

                    _allIcoItems.Add(IcoInstance);

                    _scrollGridGroupXAxis = 245 * _allUnitItems.Count;
                    _scrollGridGroup.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _scrollGridGroupXAxis);
                }                
            }
            _furnitureUpgradeUI.SetActive(true);
        }
    }

    public void DestroyGridGroupChild()
    {
        foreach (Transform child in _scrollGridGroup)
        {
            Destroy(child.gameObject);
        }        
    }



    public void UpdateItemIcoState()
    {
        for (int i = 0; i < _allIcoItems.Count; i++)
        {
            if (_allIcoItems[i]._itemIcoState > 0)
            {
                _allIcoItems[i]._itemIcoState = 1;
                PlayerPrefs.SetInt(_allIcoItems[i]._upgradeUnitPref + _allIcoItems[i]._unitId, 1);
                _allIcoItems[i].IcoSelfUpdate();
            }
        }
    }

    public void UpdateItems(UpgradeItemIco upgradeItemIco)
    {
        if (!isActive)
        {
            _emptyPlaceImg.SetActive(false);
        }        
        for (int i = 0; i < _allIcoItems.Count; i++)
        {
            if (_allIcoItems[i] == upgradeItemIco)
            {
                _allUnitItems[i].gameObject.SetActive(true);
                Scale(_allUnitItems[i].gameObject);

            }
            else
            {
                _allUnitItems[i].gameObject.SetActive(false);
            }
        }
    }


    public void ToggleAnimationOfActiveObject()
    {
        Debug.Log("     O     -----------------     O     ");
        for (int i = 0; i < _allUnitItems.Count; i++)
        {
            if (_allUnitItems[i].gameObject.active)
            {
                _allUnitItems[i].gameObject.GetComponent<Animator>().SetTrigger("ToggleAnim");
            }
        }
    }

    private void Scale(GameObject objectToScale)
    {
        objectToScale.transform.DOScale(_itemForScale, 0.3f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo);
    }

    public void NoMoneyCoroutine()
    {
        StartCoroutine(NoMoney());
    }

    IEnumerator NoMoney()
    {
        noMoneyTag.SetActive(true);
        yield return new WaitForSeconds(3f);
        noMoneyTag.SetActive(false);
    }

}
