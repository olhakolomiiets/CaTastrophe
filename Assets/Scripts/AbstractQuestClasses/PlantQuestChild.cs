using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlantQuestChild : QuestClass
{
    public GameObject GroundParticles;
    public GameObject GroundParticles2;
    private GameObject ground;
    private GameObject ground2;

    protected override void InstantiateQestEffects()
    {
       ground = Instantiate(GroundParticles, transform.position, Quaternion.identity);
        ground2 = Instantiate(GroundParticles2, transform.position, Quaternion.identity);
        ground.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
        ground2.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
    }
}