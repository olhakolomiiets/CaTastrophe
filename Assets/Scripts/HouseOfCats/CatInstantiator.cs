using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInstantiator : MonoBehaviour
{
    public CatPlaceInHouse[] catPlases;
    [SerializeField] private GameObject catBrown;
    [SerializeField] private GameObject catTiger;
    [SerializeField] private GameObject catBeige;
    [SerializeField] private GameObject catGrey;
    [SerializeField] private GameObject catWhite;
    [SerializeField] private GameObject slimCat;
    [SerializeField] private GameObject slimCatSO;
    [SerializeField] private GameObject slimCatS;
    [SerializeField] private GameObject slimCatR;
    [SerializeField] private GameObject slimCatG;
    [SerializeField] private GameObject slimCatDG;
    [SerializeField] private GameObject slimCatB;
    [SerializeField] private GameObject slimCat3B;
    [SerializeField] private GameObject slimCaDSO;
    public GameObject topBarBrown;
    public GameObject topBarTiger;
    public GameObject topBarBeige;
    public GameObject topBarWhite;
    public GameObject topBarGrey;
     public GameObject topBarslimCat;
    public GameObject topBarslimCatSO;
    public GameObject topBarslimCatS;
    public GameObject topBarslimCatR;
    public GameObject topBarslimCatG;
     public GameObject topBarslimCatDG;
    public GameObject topBarslimCatB;
    public GameObject topBarslimCat3B;
    public GameObject topBarslimCaDSO;
    [SerializeField] private HouseCat catScript;
    private CatPlaceInHouse placeForCat;
    [SerializeField] private GameObject cat;
    public int floorY;
    private void Awake()
    {
        PlayerPrefs.SetInt("cat1Floor1", 1);
    }
    void Start()
    {
        CheckBrownCat();
        CheckTigerCat();
        CheckBeigeCat();
        CheckGrayCat();
        CheckWhiteCat();
        CheckSlimCat ();
        CheckSOSlimCat();
        CheckSSlimCat();
        CheckRSlimCat();
        CheckGSlimCat();
        CheckDGSlimCat();
        ChecBSlimCat();
        Check3BSlimCat();
        CheckDSOSlimCat();

    }
    private CatPlaceInHouse FreePlaceInHouse()
    {
        int i = 0;
        for (i = 0; i <= catPlases.Length - 1; i++)
        {
            if (catPlases[i].IsPlaceBusy() == 0)
            {
                catPlases[i].SetPlaceBusy();
                return catPlases[i];
            }
        }
        return catPlases[0];
    }
    private void CheckBrownCat()
    {
        if (PlayerPrefs.GetInt("brownfatcat") == 1 && PlayerPrefs.GetInt("brownfatcatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(catBrown, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarBrown;
            PlayerPrefs.SetInt("brownfatcatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("brownfatcat") == 1 && PlayerPrefs.GetInt("brownfatcatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("brownfatcatPlace")];
            cat = Instantiate(catBrown, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarBrown;
        }
    }
    private void CheckTigerCat()
    {
        if (PlayerPrefs.GetInt("tigerfatcat") == 1 && PlayerPrefs.GetInt("tigerfatcatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(catTiger, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarTiger;
            PlayerPrefs.SetInt("tigerfatcatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("tigerfatcat") == 1 && PlayerPrefs.GetInt("tigerfatcatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("tigerfatcatPlace")];
            cat = Instantiate(catTiger, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarTiger;
        }
    }
    private void CheckBeigeCat()
    {
        if (PlayerPrefs.GetInt("beigefatcat") == 1 && PlayerPrefs.GetInt("beigefatcatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(catBeige, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarBeige;
            PlayerPrefs.SetInt("beigefatcatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("beigefatcat") == 1 && PlayerPrefs.GetInt("beigefatcatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("beigefatcatPlace")];
            cat = Instantiate(catBeige, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarBeige;
        }
    }
    private void CheckGrayCat()
    {
        if (PlayerPrefs.GetInt("greyfatcat") == 1 && PlayerPrefs.GetInt("greyfatcatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(catGrey, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarGrey;
            PlayerPrefs.SetInt("greyfatcatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("greyfatcat") == 1 && PlayerPrefs.GetInt("greyfatcatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("greyfatcatPlace")];
            cat = Instantiate(catGrey, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarGrey;
        }
    }
    private void CheckWhiteCat()
    {
        if (PlayerPrefs.GetInt("whitefatcat") == 1 && PlayerPrefs.GetInt("whitefatcatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(catWhite, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarWhite;
            PlayerPrefs.SetInt("whitefatcatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("whitefatcat") == 1 && PlayerPrefs.GetInt("whitefatcatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("whitefatcatPlace")];
            cat = Instantiate(catWhite, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarWhite;
        }
    }
    private void CheckSlimCat ()
    {
        if (PlayerPrefs.GetInt("SlimCat") == 1 && PlayerPrefs.GetInt("SlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCat, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCat;
            PlayerPrefs.SetInt("SlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("SlimCat") == 1 && PlayerPrefs.GetInt("SlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("SlimCatPlace")];
            cat = Instantiate(slimCat, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCat;
        }
    }
     private void CheckSOSlimCat()
    {
        if (PlayerPrefs.GetInt("SOSlimCat") == 1 && PlayerPrefs.GetInt("SOSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCatSO, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatSO;
            PlayerPrefs.SetInt("SOSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("SOSlimCat") == 1 && PlayerPrefs.GetInt("SOSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("SOSlimCatPlace")];
            cat = Instantiate(slimCatSO, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatSO;
        }
    }
    private void CheckSSlimCat()
    {
        if (PlayerPrefs.GetInt("SSlimCat") == 1 && PlayerPrefs.GetInt("SSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCatS, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatS;
            PlayerPrefs.SetInt("SSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("SSlimCat") == 1 && PlayerPrefs.GetInt("SSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("SSlimCatPlace")];
            cat = Instantiate(slimCatS, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatS;
        }
    }
    private void CheckRSlimCat()
    {
        if (PlayerPrefs.GetInt("RSlimCat") == 1 && PlayerPrefs.GetInt("RSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCatR, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatR;
            PlayerPrefs.SetInt("RSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("RSlimCat") == 1 && PlayerPrefs.GetInt("RSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("RSlimCatPlace")];
            cat = Instantiate(slimCatR, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatR;
        }
    }
    private void CheckGSlimCat()
    {
        if (PlayerPrefs.GetInt("GSlimCat") == 1 && PlayerPrefs.GetInt("GSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCatG, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatG;
            PlayerPrefs.SetInt("GSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("GSlimCat") == 1 && PlayerPrefs.GetInt("GSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("GSlimCatPlace")];
            cat = Instantiate(slimCatG, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatG;
        }
    }
    private void CheckDGSlimCat()
    {
        if (PlayerPrefs.GetInt("DGSlimCat") == 1 && PlayerPrefs.GetInt("DGSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCatDG, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatDG;
            PlayerPrefs.SetInt("DGSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("DGSlimCat") == 1 && PlayerPrefs.GetInt("DGSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("DGSlimCatPlace")];
            cat = Instantiate(slimCatDG, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatDG;
        }
    }
    private void ChecBSlimCat()
    {
        if (PlayerPrefs.GetInt("BSlimCat") == 1 && PlayerPrefs.GetInt("BSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCatB, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatB;
            PlayerPrefs.SetInt("BSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("BSlimCat") == 1 && PlayerPrefs.GetInt("BSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("BSlimCatPlace")];
            cat = Instantiate(slimCatB, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCatB;
        }
    }
    private void Check3BSlimCat()
    {
        if (PlayerPrefs.GetInt("3BSlimCat") == 1 && PlayerPrefs.GetInt("3BSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCat3B, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCat3B;
            PlayerPrefs.SetInt("3BSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("3BSlimCat") == 1 && PlayerPrefs.GetInt("3BSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("3BSlimCatPlace")];
            cat = Instantiate(slimCat3B, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCat3B;
        }
    }
    private void CheckDSOSlimCat()
    {
        if (PlayerPrefs.GetInt("DSOSlimCat") == 1 && PlayerPrefs.GetInt("DSOSlimCatPlace") == 0)
        {
            placeForCat = FreePlaceInHouse();
            cat = Instantiate(slimCaDSO, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCaDSO;
            PlayerPrefs.SetInt("DSOSlimCatPlace", placeForCat.idPlace);
        }
        else if (PlayerPrefs.GetInt("DSOSlimCat") == 1 && PlayerPrefs.GetInt("DSOSlimCatPlace") > 0)
        {
            placeForCat = catPlases[PlayerPrefs.GetInt("DSOSlimCatPlace")];
            cat = Instantiate(slimCaDSO, new Vector3(placeForCat.chair.transform.position.x, placeForCat.chair.transform.position.y, 0), Quaternion.identity);
            catScript = cat.GetComponent<HouseCat>();
            CopyPlaceParameters();
            catScript.barTop = topBarslimCaDSO;
        }
    }
    private void CopyPlaceParameters()
    {
        catScript.chair = placeForCat.chair;
        catScript.house = placeForCat.house;
        catScript.sofa = placeForCat.sofa;
        catScript.plate = placeForCat.plate;
        catScript.foodPlaceRight = placeForCat.foodPlaceRight;
        catScript.foodPlaceLeft = placeForCat.foodPlaceLeft;
        catScript.animHouse = placeForCat.animHouse;
        catScript.gameObject.layer = placeForCat.gameObject.layer;
        catScript.nameOfCat = placeForCat.catName;
        catScript.drinkPlace = placeForCat.drinkPlace;
        catScript.drinkPlaceLeft = placeForCat.drinkPlaceLeft;
        catScript.drinkPlaceRight = placeForCat.drinkPlaceRight;
        catScript.ballPlace = placeForCat.ballPlace;
        catScript.toilet = placeForCat.toilet;
        catScript.sportPlace = placeForCat.sportPlace;        
    }
}