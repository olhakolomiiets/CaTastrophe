using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSlow : MonoBehaviour
{
    public Scrollbar slider;
    private float value;
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(Sliderr());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sliderr()
{
    
 for (float q = 0f; q < 1; q += .0002f)
   {
      slider.value = q;
      yield return new WaitForSeconds(.001f);
      
   }
     
    }
  
  
}

