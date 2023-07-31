using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Review;

public class GoogleReviewManager : MonoBehaviour
{
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;


    private void Start()
    {

    }

    IEnumerator RequestReviews()
    {
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;

        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            yield break;
        }

        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null;
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {          
            yield break;
        }
    }

    public void GetReviewOnGooglePlay()
    {
        StartCoroutine(RequestReviews());
    }
}
