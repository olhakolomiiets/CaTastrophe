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
                case 6:
                    ActivateHat(5);
                    break;
                case 7:
                    ActivateHat(6);
                    break;
                case 8:
                    ActivateHat(7);
                    break;
                case 9:
                    ActivateHat(8);
                    break;
                case 10:
                    ActivateHat(9);
                    break;
                case 11:
                    ActivateHat(10);
                    break;
                case 12:
                    ActivateHat(11);
                    break;
                case 13:
                    ActivateHat(12);
                    break;
                case 14:
                    ActivateHat(13);
                    break;
                case 15:
                    ActivateHat(14);
                    break;
                case 16:
                    ActivateHat(15);
                    break;
                case 17:
                    ActivateHat(16);
                    break;
                case 18:
                    ActivateHat(17);
                    break;
                case 19:
                    ActivateHat(18);
                    break;
                case 20:
                    ActivateHat(19);
                    break;
                case 21:
                    ActivateHat(20);
                    break;
                case 22:
                    ActivateHat(21);
                    break;
                case 23:
                    ActivateHat(22);
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
            case 6:
                ActivateHat(5);
                break;
            case 7:
                ActivateHat(6);
                break;
            case 8:
                ActivateHat(7);
                break;
            case 9:
                ActivateHat(8);
                break;
            case 10:
                ActivateHat(9);
                break;
            case 11:
                ActivateHat(10);
                break;
            case 12:
                ActivateHat(11);
                break;
            case 13:
                ActivateHat(12);
                break;
            case 14:
                ActivateHat(13);
                break;
            case 15:
                ActivateHat(14);
                break;
            case 16:
                ActivateHat(15);
                break;
            case 17:
                ActivateHat(16);
                break;
            case 18:
                ActivateHat(17);
                break;
            case 19:
                ActivateHat(18);
                break;
            case 20:
                ActivateHat(19);
                break;
            case 21:
                ActivateHat(20);
                break;
            case 22:
                ActivateHat(21);
                break;
            case 23:
                ActivateHat(22);
                break;


            default:
                DeactivateAllHats();
                break;
        }

    }
}
