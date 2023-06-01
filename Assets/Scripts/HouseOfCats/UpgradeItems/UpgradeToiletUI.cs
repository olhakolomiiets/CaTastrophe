using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeToiletUI : MonoBehaviour, IClickable
{
    #region EditorFields

    [SerializeField] private int _purch;
    [SerializeField] private GameObject noMoneyTag;
    public ToiletUpgradeHandler CurrentToiletUpgradeHandler;
    [SerializeField] private Text _priceTxt;
    [SerializeField] private Text _levelTxt;
    [SerializeField] private GameObject _arrow1;
    [SerializeField] private GameObject _arrow2;
    [SerializeField] private Vector3 _arrowForScale;
    private Tweener tweenerArrow1;
    private Tweener tweenerArrow2;
    private Vector3 _originalScaleArrow1;
    private Vector3 _originalScaleArrow2;

    #endregion

    #region Properties

    #endregion

    private void Awake()
    {
        _originalScaleArrow1 = _arrow1.transform.localScale;
        _originalScaleArrow2 = _arrow2.transform.localScale;
    }
    void Start()
    {
       
    }

    private void OnEnable()
    {
        _purch = PlayerPrefs.GetInt(CurrentToiletUpgradeHandler.ToiletUpgradePrefFloor, 0);
        switch (_purch)
        {
            case 0:
                _priceTxt.text = CurrentToiletUpgradeHandler.UpgradePrices.UpgradePriceToiletLvl2.ToString();
                _levelTxt.text = 2.ToString();
                tweenerArrow1 = _arrow1.transform.DOScale(_arrowForScale, 1)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
                tweenerArrow1.Play();
                break;
            case 1:
                _priceTxt.text = CurrentToiletUpgradeHandler.UpgradePrices.UpgradePriceToiletLvl3.ToString();
                _levelTxt.text = 3.ToString();
                tweenerArrow2 = _arrow2.transform.DOScale(_arrowForScale, 1)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
                tweenerArrow2.Play();
                break;
            default:
                _priceTxt.text = CurrentToiletUpgradeHandler.UpgradePrices.UpgradePriceToiletLvl2.ToString();
                _levelTxt.text = 2.ToString();
                tweenerArrow1 = _arrow1.transform.DOScale(_arrowForScale, 1)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
                tweenerArrow1.Play();
                break;
        }
    }

    public void Click()
    {
        CurrentToiletUpgradeHandler.Upgrade();
    }

    IEnumerator NoMoney()
    {

        noMoneyTag.SetActive(true);

        yield return new WaitForSeconds(1f);

        noMoneyTag.SetActive(false);
    }

    private void OnDisable()
    {
        tweenerArrow1.Kill();
        tweenerArrow2.Kill();

        _arrow1.transform.localScale = _originalScaleArrow1;
        _arrow2.transform.localScale = _originalScaleArrow2;
    }
}
