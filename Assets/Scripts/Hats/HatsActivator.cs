using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatsActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> Hats;
    [SerializeField] private CowController playerController;
    public bool isShop;

    private void OnEnable()
    {
        Hats = new List<GameObject>();
        foreach (Transform child in transform)
        {
            Hats.Add(child.gameObject);
        }

        if (isShop)
        {
            StartCoroutine(UpdateHat());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateHat());
    }

    void Start()
    {
        if (!isShop)
        {
            DeactivateAllHats();
            SetActiveHat();
        }     
    }


    void Update()
    {
        
    }

    private void SetActiveHat()
    {
        if (PlayerPrefs.HasKey(playerController.ppCatName + "ActiveHat"))
        {
            switch (PlayerPrefs.GetInt(playerController.ppCatName + "ActiveHat"))
            {
                case 1:
                    ActivateHat(0);
                    break;
                case 2:
                    ActivateHat(1);
                    break;
                case 3:
                    ActivateHat(2);
                    break;
                case 4:
                    ActivateHat(3);
                    break;
                case 5:
                    ActivateHat(4);
                    break;
                default:
                    DeactivateAllHats();
                    break;
            }
        }
        
    }

    public void ActivateHat(int index)
    {
        if (index >= 0 && index < Hats.Count)
        {
            DeactivateAllHats(); 
            Hats[index].SetActive(true); 
        }
        else
        {
            Debug.LogWarning("Hat index out of range");
        }
    }

    private void DeactivateAllHats()
    {
        foreach (GameObject hat in Hats)
        {
            hat.SetActive(false);
        }
    }

    private IEnumerator UpdateHat()
    {
        while (true)
        {
            SetActiveHatInShop();
            yield return new WaitForSeconds(1f);
        }        
    }

    private void SetActiveHatInShop()
    {
        string nameCat = PlayerPrefs.GetString("CatInShopActive");
        Debug.Log(" nameCat " + nameCat + " PlayerPrefs.GetInt(nameCat + ActiveHat) " + PlayerPrefs.GetInt(nameCat + "ActiveHat"));
        switch (PlayerPrefs.GetInt(nameCat + "ActiveHat"))
        {
            case 1:
                ActivateHat(0);
                break;
            case 2:
                ActivateHat(1);
                break;
            case 3:
                ActivateHat(2);
                break;
            case 4:
                ActivateHat(3);
                break;
            case 5:
                ActivateHat(4);
                break;
            default:
                DeactivateAllHats();
                break;
        }

    }
}
