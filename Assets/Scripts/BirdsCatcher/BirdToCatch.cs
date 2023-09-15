using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdToCatch : MonoBehaviour
{
    public Animator birdAnim;
    public bool coroutineIsRunning = false;
    [SerializeField] private string tag;
    private WaveMove birdWaveMove;
    public GameObject featherPuff;
    public BirdsCatcherLogic _birdsCatcherLogic;
    public bool isEnemy;
    //public UnityEvent birdCatched;

    private void Start()
    {
        birdWaveMove = GetComponent<WaveMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemy)
        {
            this.GetComponent<BoxCollider2D>().isTrigger = true;

            return;
        }
        if (!coroutineIsRunning && other.CompareTag(tag))
        {
            CatchBird();
        }
    }

    private void CatchBird()
    {
        //birdCatched?.Invoke();       
        MakeFeatherPuff();
        SoundManager.snd.PlayBirdHitSounds();
        _birdsCatcherLogic.UpdateBirdsAmount();
        birdAnim.gameObject.SetActive(false);
        StartCoroutine(DisableFeatherAndBird());
    }

    private void HitCat()
    {

    }

    private IEnumerator DisableFeatherAndBird()
    {
        coroutineIsRunning = true;
        yield return new WaitForSeconds(2f);             
        if (featherPuff != null)
        {
            featherPuff.SetActive(false);
        }      
        this.gameObject.SetActive(false);
        birdAnim.gameObject.SetActive(true);
        coroutineIsRunning = false;
    }

    private void MakeFeatherPuff()
    {
        featherPuff = ObjectPooler.SharedInstance.GetPooledObject("EnemyFan");
        if (featherPuff != null)
        {
            featherPuff.transform.position = transform.position;
            featherPuff.SetActive(true);
        }
    }
}
