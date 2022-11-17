using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CourtainQuestChild : QuestClass
{
    private GameObject pooh;
    private GameObject shine;
    public float xCorrection;
    public float yCorrection;
    public GameObject CottonParticles;

   protected override void Start()
    {
        base.Start();
        shine = gameObject.transform.GetChild(2).gameObject;    
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("courtainDestroy1") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                shine.SetActive(true);
                if (PlayerPrefs.GetInt("courtainDestroyTipUsed") == 0)
                {
                    questTip.SetActive(true);
                }
                btnDo.onClick.AddListener(Do);
                btnActive.SetActive(true);
            }
        }
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("courtainDestroy1") == 1)
        {
            shine.SetActive(false);
            Invoke("TipHide", 2);
            btnDo.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    protected override void InstantiateQestEffects()
    {
        pooh = Instantiate(CottonParticles, transform.position, Quaternion.identity);
        pooh.transform.position = new Vector3(transform.position.x - xCorrection, transform.position.y - yCorrection, -9);
        SoundManager.snd.PlayLongCatSounds();
        SoundManager.snd.PlayScratchSounds();
    }
}