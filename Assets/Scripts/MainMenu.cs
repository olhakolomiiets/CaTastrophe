
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CharacterSelect()
    {
        SceneManager.LoadScene("players-select");
    }
    public void ExchangeShop()
    {
        SceneManager.LoadScene("Exchange");
    }
    public void SuperpowersShop()
    {
        SceneManager.LoadScene("superpowers-shop");
    }
    public void SetPlayer(int index)
    {
        PlayerPrefs.SetInt("Player", index);
    }
    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void MenuLevels()
    {
        SceneManager.LoadScene("menu-levels");
        Time.timeScale = 1;
    }
    public void MenuMain()
    {
        SceneManager.LoadScene("main-menu");
        Time.timeScale = 1;
    }
    public void QiutGame()
    {
        Application.Quit();
    }
}