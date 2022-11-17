using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatIco : MonoBehaviour
{
    public int icoID;
    public List<GameObject> cheatIconsVariants; 

    public Animator icoAnimator;
 
    public int FirstActivePower() 
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).gameObject.activeSelf == true)
            {
                return i;
            }          
        }  
        return 6; 
    }

}
