using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffLight : MonoBehaviour
{
    private GameObject player;
    private Transform leftEyeTransform;
    private Transform rightEyeTransform;
    private GameObject rightEye;
    private GameObject leftEye;
    private GameObject rightEyeCopy;
    private GameObject leftEyeCopy;
    private GameObject blackScreen;
    public Animator[] bull;
    public Button btn;
    private GameObject btnActive;
    private ScoreManager sm;
    private GameObject tip;
    public AudioClip sound;
    private AudioSource source;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bull = player.GetComponents<Animator>();
        btnActive = btn.transform.GetChild(0).gameObject;
        rightEye = player.transform.GetChild(16).gameObject;
        leftEye = player.transform.GetChild(7).gameObject;
        leftEyeTransform = leftEye.GetComponent<Transform>();
        rightEyeTransform = rightEye.GetComponent<Transform>();
        blackScreen = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        blackScreen.transform.position = new Vector3
        (leftEyeTransform.position.x, leftEyeTransform.transform.position.y, transform.position.z);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            // foreach(Animator anim in questAnim) {
            //     anim.SetTrigger("IsTriggered");
            // if (PlayerPrefs.GetInt("courtainDestroyTipUsed") == 0)
            // {
            //     tip.SetActive(true);
            // }
            btn.onClick.AddListener(Do);
            btnActive.SetActive(true);
        }
    }

    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        // foreach (Animator anim in questAnim)
        // {
        //     anim.SetTrigger("Done");
        // }
      foreach (Animator anim in bull)
        {
            anim.SetTrigger("actionPush");
        }
        SoundManager.snd.PlayCatSounds();       
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        StartCoroutine(EyesLayerChanger());

        FirebaseAnalytics.LogEvent(name: "light_turned_off");
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            // Invoke ("TipHide", 2);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    private void TipHide()
    {
        tip.SetActive(false);
    }
    private void CloneEyes()
    {
        rightEyeCopy = Instantiate(rightEye, new Vector3(rightEyeTransform.position.x, transform.position.y, 0), Quaternion.identity);
        rightEyeCopy.transform.parent = rightEye.gameObject.transform;
        rightEyeCopy.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        leftEyeCopy = Instantiate(leftEye, new Vector3(leftEyeTransform.position.x, transform.position.y, 0), Quaternion.identity);
        leftEyeCopy.transform.parent = rightEye.gameObject.transform;
        leftEyeCopy.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
    }
    IEnumerator EyesLayerChanger()
    {
        blackScreen.SetActive(true);
        blackScreen.transform.position = new Vector3(leftEyeTransform.position.x, leftEyeTransform.transform.position.y, transform.position.z);
        rightEyeCopy = Instantiate(rightEye, new Vector3(rightEyeTransform.position.x, transform.position.y, 0), Quaternion.identity);
        rightEyeCopy.transform.parent = gameObject.transform;
        rightEyeCopy.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        rightEyeCopy.GetComponent<SpriteRenderer>().sortingOrder = 10;
        leftEyeCopy = Instantiate(leftEye, new Vector3(leftEyeTransform.position.x, transform.position.y, 0), Quaternion.identity);
        leftEyeCopy.transform.parent = gameObject.transform;
        leftEyeCopy.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        leftEyeCopy.GetComponent<SpriteRenderer>().sortingOrder = 10;
        yield return new WaitForSeconds(10f);
        blackScreen.SetActive(false);
        blackScreen.transform.position = new Vector2(0, 0);
        Destroy(rightEyeCopy);
        Destroy(leftEyeCopy);
    }
}