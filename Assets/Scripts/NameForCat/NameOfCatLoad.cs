using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameOfCatLoad : MonoBehaviour
{
    public string nameOfCat;
    public string savedNameCat;
    public Text loadedName;
    [SerializeField] private int catIndex;

    private void Start()
    {
        nameOfCat = PlayerPrefs.GetString("nameOfCat" + catIndex);
        loadedName.text = nameOfCat;

    }
    private void Update()
    {
        nameOfCat = PlayerPrefs.GetString("nameOfCat" + catIndex);
        loadedName.text = nameOfCat;
        // StartCoroutine(UpdateName());
    }

    private IEnumerator UpdateName()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            nameOfCat = PlayerPrefs.GetString("nameOfCat" + catIndex);
            loadedName.text = nameOfCat;
        }
    }
}
