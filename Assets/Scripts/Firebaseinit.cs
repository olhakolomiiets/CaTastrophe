
using UnityEngine;
using Firebase;
using Firebase.Analytics;


public class Firebaseinit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(continuationAction: task =>
       {
           FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
       });
    }
}