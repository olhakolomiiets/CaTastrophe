using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanQuestNightCity : MonoBehaviour
{
    [SerializeField] private GameObject btnActive;
    [SerializeField] private Button btn;
    private Animator playerAnimator;
    [SerializeField] private string playerAnimationTag;
    [SerializeField] private float delayForMovingCan; 
    public bool Used;
    public NightCityLogic nightCityLogic;
    private bool triggered = false;

    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float throwHeight = 5.0f;
    public Transform startTransform;
    public Transform endTransform;
    public bool isMoving = false;
    [SerializeField] private float pointsToSlider;
    [SerializeField] private float leftLimitX = -60f;
    [SerializeField] private float rightLimitX = -36.8f;


    private void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && !triggered)
            {
                triggered = true;
                btnActive.SetActive(true);
                btn.onClick.AddListener(Do);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && triggered)
        {
            triggered = false;
            btnActive.SetActive(false);
            btn.onClick.RemoveListener(Do);
        }
    }

    public void Do()
    {
        playerAnimator.SetTrigger(playerAnimationTag);
        nightCityLogic.UpdateSlider(pointsToSlider);
        SoundManager.snd.PlayMetalStuffSounds();
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        btn.GetComponent<StopMoveForDo>().StopMove();
        SetPositionAndStartMove();
    }

    public void SetPositionAndStartMove()
    {
        startTransform.position = this.transform.position;
        StartCoroutine(MoveObject(gameObject, startTransform, endTransform));
    }

    private IEnumerator MoveObject(GameObject thrownObject, Transform startTransform, Transform endTransform)
    {
        yield return new WaitForSeconds(delayForMovingCan);
        // Generate a random rotation angle around the Z-axis
        float randomRotation = Random.Range(0f, 360f);
        // Apply the rotation to the GameObject
        thrownObject.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

        isMoving = true;
        float startTime = Time.time;

        float randomX = UnityEngine.Random.Range(leftLimitX, rightLimitX);

        // Set the position of spawnBallPoint with the random X value
        Vector3 newPosition = endTransform.position;
        newPosition.x = randomX;
        endTransform.position = newPosition;

        float journeyLength = Vector3.Distance(startTransform.position, endTransform.position);

        while (Time.time - startTime < 1.0f)
        {
            float journeyTime = (Time.time - startTime) / 1.0f;

            // Calculate a parabolic height offset
            float yOffset = throwHeight * 4.0f * journeyTime * (1.0f - journeyTime);

            // Calculate the current position along the path
            Vector3 currentPosition = Vector3.Lerp(startTransform.position, endTransform.position, journeyTime);
            currentPosition.y += yOffset;

            float t = 1f;
            Quaternion randomRotation2 = Quaternion.Euler(0f, 0f, Random.Range(0f, 360));
            thrownObject.transform.rotation = Quaternion.Lerp(startTransform.rotation, randomRotation2, t);

            thrownObject.transform.position = currentPosition;
            yield return null;
        }

        // Ensure the object reaches the exact destination
        thrownObject.transform.position = endTransform.position;
        isMoving = false;
        Used = false;
        SoundManager.snd.PlayMetalStuffSounds();
    }
}
