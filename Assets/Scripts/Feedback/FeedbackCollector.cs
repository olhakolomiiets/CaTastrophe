using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FeedbackCollector : MonoBehaviour
{
    [SerializeField] private Text txtData;
    [SerializeField] private Button btnSubmit;
    [SerializeField] private CollectionOption option;

    private enum CollectionOption { openGFormLink, sendGFormData };

    private const string kReceiverEmailAddress = "noc.game.dev@gmail.com";

    private const string kGFormBaseURL = "https://docs.google.com/forms/d/e/1FAIpQLScZZ3KthEkYW2POH4cRkX_MHrezJCPyifZKcVMAo181upy70A/";
    private const string kGFormEntryID = "entry.889064995";

    void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(txtData);
        UnityEngine.Assertions.Assert.IsNotNull(btnSubmit);
        btnSubmit.onClick.AddListener(delegate {
            switch (option)
            {
                case CollectionOption.openGFormLink:
                    OpenGFormLink();
                    break;
                case CollectionOption.sendGFormData:
                    StartCoroutine(SendGFormData(txtData.text));
                    break;
            }
        });
    }

    private static void OpenGFormLink()
    {
        string urlGFormView = kGFormBaseURL + "viewform";
        OpenLink(urlGFormView);
    }

    private static IEnumerator SendGFormData<T>(T dataContainer)
    {
        bool isString = dataContainer is string;
        string jsonData = isString ? dataContainer.ToString() : JsonUtility.ToJson(dataContainer);

        WWWForm form = new WWWForm();
        form.AddField(kGFormEntryID, jsonData);
        string urlGFormResponse = kGFormBaseURL + "formResponse";
        using (UnityWebRequest www = UnityWebRequest.Post(urlGFormResponse, form))
        {
            yield return www.SendWebRequest();
        }
    }

    // We cannot have spaces in links for iOS
    public static void OpenLink(string link)
    {
        bool googleSearch = link.Contains("google.com/search");
        string linkNoSpaces = link.Replace(" ", googleSearch ? "+" : "%20");
        Application.OpenURL(linkNoSpaces);
    }
}


