using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpObject : MonoBehaviour, IClickable
{
    public DestructableObjectHandler objectDestroyed;
    private void OnEnable()
    {
        if (objectDestroyed.destroyedVersion.activeSelf)
        {
            objectDestroyed.ico.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            objectDestroyed.ico.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        }
    }
    public void Click()
    {
        Debug.Log("PUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUSHHHHHHHHHHHHHHHHH");
        objectDestroyed.Click();
        SoundManager.snd.PlayButtonsSound();
    }
}