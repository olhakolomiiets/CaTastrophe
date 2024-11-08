using UnityEngine;
using UnityEngine.UI;

public class GiveMeMoney : MonoBehaviour
{
    public InputField inputField;
    private const string secretCode = "#25251805";

    public void CheckCodeAndAddScore()
    {
        if (inputField.text == secretCode)
        {
            int currentScore = PlayerPrefs.GetInt("TotalScore", 0);
            PlayerPrefs.SetInt("TotalScore", currentScore + 100000);
        }
        else
        {
            Debug.Log("Incorrect code.");
        }
    }
}
