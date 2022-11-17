using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChairQuestChild : QuestClass
{
    private GameObject paintAct;
    public GameObject CottonParticles;
    private GameObject pooh;
    bool paint = false;
    protected override void InstantiateQestEffects()
    {
        pooh = Instantiate(CottonParticles, transform.position, Quaternion.identity);
        pooh.transform.position = new Vector3(transform.position.x, transform.position.y + 3.2f, -9);
        SoundManager.snd.PlayLongCatSounds();
        SoundManager.snd.PlayScratchLoudSounds();
    }
}