using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPaws : MonoBehaviour
{
 private GameObject paw1;
 private GameObject paw2;
 private GameObject paw3;
 private GameObject paw4;
 private GameObject paw5;
 private GameObject paw6;
 private GameObject paw7;
 private GameObject paw8;
 private GameObject paw9;
 private GameObject paw10;
 private GameObject paw11;
 private GameObject paw12;
 private GameObject paw13;
 private GameObject paw14;

 public Slider slider;
 private float sliderValue;

public Text text;
 

  
    void Start()
    {

        paw1 = gameObject.transform.GetChild(0).gameObject;
        paw2 = gameObject.transform.GetChild(1).gameObject;
        paw3 = gameObject.transform.GetChild(2).gameObject;
        paw4 = gameObject.transform.GetChild(3).gameObject;
        paw5 = gameObject.transform.GetChild(4).gameObject;
        paw6 = gameObject.transform.GetChild(5).gameObject;
        paw7 = gameObject.transform.GetChild(6).gameObject;
        paw8 = gameObject.transform.GetChild(7).gameObject;
        paw9 = gameObject.transform.GetChild(8).gameObject;
        paw10 = gameObject.transform.GetChild(9).gameObject;
        paw11 = gameObject.transform.GetChild(10).gameObject;
        paw12 = gameObject.transform.GetChild(11).gameObject;
        paw13 = gameObject.transform.GetChild(12).gameObject;
        paw14 = gameObject.transform.GetChild(13).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = slider.value;
        text.text = sliderValue.ToString();
        StartCoroutine(PawsShow());


        
        
    }
        IEnumerator PawsShow () 
{
    if (sliderValue> 0.09f) {
    paw1.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.16f) {
    paw2.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.22f) {
    paw3.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.28f) {
    paw4.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.33f) {
    paw5.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.38f) {
    paw6.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.42f) {
    paw7.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.48f) {
    paw8.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.53f) {
    paw9.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.60f) {
    paw10.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.64f) {
    paw11.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.70f) {
    paw12.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.75f) {
    paw13.SetActive(true);
    yield return new WaitForSeconds(0.05f);
}
if (sliderValue> 0.83f) {
    paw14.SetActive(true);
}

yield return null;
}
    }




        
    // switch (sliderValue)
    //     {
    //         case 0.08f:
    //             paw1.SetActive(true);
    //             break;
    //         case 0.16f:
    //             paw2.SetActive(true);
    //             break;
    //         case 0.24f:
    //             paw3.SetActive(true);
    //             break;
    //         case 0.32f:
    //             paw4.SetActive(true);
    //             break;
    //         case 0.4f:
    //             paw5.SetActive(true);
    //             break;
    //         case 0.48f:
    //             paw6.SetActive(true);
    //             break;
    //         case 0.56f:
    //             paw7.SetActive(true);
    //             break;
    //         case 0.64f:
    //             paw8.SetActive(true);
    //             break;
    //         case 0.72f:
    //             paw9.SetActive(true);
    //             break;
    //         case 0.8f:
    //             paw10.SetActive(true);
    //             break;
    //         case 0.88f:
    //             paw11.SetActive(true);
    //             break;
    //         case 0.92f:
    //             paw12.SetActive(true);
    //             break;
    //         case 0.96f:
    //             paw13.SetActive(true);
    //             break;
    //         case 1f:
    //             paw14.SetActive(true);
    //             break;
           
    //     }



