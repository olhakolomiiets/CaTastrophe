using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchCat : MonoBehaviour
{
    public bool Used;
    public string CatchCatPref;
    public Animator AnimatedCatObject;
    public WantedBoard Board;
    public Animator[] paw;
    void Start()
    {
        if (PlayerPrefs.GetInt(CatchCatPref) == 1)
        {
            Used = true;
            CatObjectAnimationSwitch();
        }
    }
    public void CatIsCatched()
    {
        if (!Used)
        {
            foreach (Animator anim in paw)
            {
                anim.SetTrigger("Done");
            }
            PlayerPrefs.SetInt(CatchCatPref, 1);
            PlayerPrefs.SetInt("AwardSherlock", PlayerPrefs.GetInt("AwardSherlock") + 1);
            SoundManager.snd.PlayCatSounds();
            Used = true;
            CatObjectAnimationSwitch();
            Board.PosterOff();
            Board.SherlockQuestWin();
        }
    }
    public void CatObjectAnimationSwitch()
    {
        if (Used && PlayerPrefs.GetInt("AwardSherlock") == 10)
        {
           AnimatedCatObject.SetBool("activeAnimation", true);
        }
        else if (Used)
        {
            AnimatedCatObject.SetBool("activeAnimation", false);
        }
    }
}