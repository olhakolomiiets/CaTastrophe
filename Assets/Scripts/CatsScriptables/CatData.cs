using UnityEngine;

[CreateAssetMenu(fileName = "NewCatData", menuName = "Game/Cat Data")]
public class CatData : ScriptableObject
{
    [Header("Identification")]
    public int id;
    public string catName;
    public string catUserName;

    [Header("Skills")]
    public float startSpeedSkill;
    public float startJumpSkill;
    public float currentSpeedSkill;
    public float currentJumpSkill;

    [Header("Visual")]
    public Sprite icon;
}
