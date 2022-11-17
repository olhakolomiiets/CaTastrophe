using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInitialize : MonoBehaviour
{
   private void Awake() {
     MobileAds.Initialize(initStatus => {});
   }
    // Update is called once per frame
    void Update()
    {
        
    }
}
