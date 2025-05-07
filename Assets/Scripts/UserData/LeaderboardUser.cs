using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUser : MonoBehaviour
{
    [SerializeField] private Image background;
    //[SerializeField] private Image catIco;
    [SerializeField] private Text displayName;
    [SerializeField] private Text rank;
    [SerializeField] private Text score;

    [Header("Colors")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color activeColor;

    public void Display(LeaderboardEntry data, int position, string id)
    {
        displayName.text = data.name;
        rank.text = position.ToString();
        score.text = data.total_score.ToString();
        background.color = data.device_id == id ? activeColor : defaultColor;
        //catIco.sprite = catSprite;
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string device_id;
        public string name;
        public double total_score;
    }
}
