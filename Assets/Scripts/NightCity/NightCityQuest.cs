using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCityQuest : MonoBehaviour
{
    public GameObject btnActive;
    public Button btn;
    public Animator[] dogFood;
    //private Animator[] bull;
    bool triggered = false;
    public bool Used;
    public NightCityLogic nightCityLogic;

    private void Update()
    {
        //if (triggered)
        //{
        //    btn.onClick.AddListener(Do);
        //}
        //else if (!triggered)
        //{
        //    btn.onClick.RemoveListener(Do);
        //}
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("-------------------------------------------------------------------");
        if (Used == false)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && !triggered)
            {
                triggered = true;
                btnActive.SetActive(true);
                btn.onClick.AddListener(Do);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && triggered)
        {
            triggered = false;
            btnActive.SetActive(false);
            btn.onClick.RemoveListener(Do);
        }
    }

    public void Do()
    {
        nightCityLogic.UpdateSlider(20f);
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        //btn.GetComponent<StopMoveForDo>().StopMove();
        //if (!Used)
        //{
        //    foreach (Animator anim in dogFood)
        //    {
        //        anim.SetTrigger("Done");
        //    }
        //    foreach (Animator anim in bull)
        //    {
        //        anim.SetTrigger("action1");
        //    }
        //    SoundManager.snd.PlayLongCatSounds();
        //    PlayerPrefs.SetInt("dogFoodTipUsed", 1);
        //    Used = true;
        //    btn.onClick.RemoveListener(Do);
        //    btnActive.SetActive(false);
        //}
    }
}
