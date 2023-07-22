using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotCleaner : MonoBehaviour
{
    public float speed;
    private float dogSpeed;
    public float angrySpeed;
    public float chillSpeed;
    public int positionOfPatrol;
    public Transform point;
    bool movingRight;
    private int movingAngry;
    Transform player;
    public float stopDistance;
    bool chill = false;
    bool angry = false;
    bool goBack = false;
    private Animator anim;
    public static Rigidbody2D rb;
    private bool noAffraid;
    private Rigidbody2D headRb;
    bool isShoked = false;
    private GameObject stars;
    bool isDizzy;
    bool isBroken;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject electro;
    [SerializeField] private GameObject brush;
    [SerializeField] private GameObject _textBonus;
    private Text _text;
    [SerializeField] private int secondsAdd;
    public float yCorrection;
    public AudioSource sound;
    [SerializeField] private GameTimer timer;
    private string secText;
    void Start()
    {
        _text = _textBonus.transform.GetChild(1).GetComponent<Text>();
        secText = Lean.Localization.LeanLocalization.GetTranslationText("Seconds");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dogSpeed = rb.velocity.magnitude;
        movingAngry = 0;
        noAffraid = player.GetComponent<CowController>().noAffraidDogs;

    }
    void Update()
    {
        if (isBroken)
        {

        }
        if (!isBroken)
        {
            if (Vector2.Distance(transform.position, point.position) < positionOfPatrol)
            {
                chill = true;
            }
            if (chill == true)
            {
                Chill();
            }
        }
    }
    void Chill()
    {
        speed = chillSpeed;
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _textBonus.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _textBonus.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Angry()
    {
        if (transform.position.x > player.position.x + 2)
        {
            movingAngry = 2;
        }
        else if (transform.position.x < player.position.x - 2)
        {
            movingAngry = 1;
        }
        else if (transform.position.x == player.position.x)
        {
            movingAngry = 3;
        }
        if (transform.position.x > player.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < player.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (movingAngry == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (movingAngry == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (movingAngry == 3)
        {
            // anim.SetBool("isPatrol", false);
            // anim.SetBool("isAngry", false);
            // anim.SetBool("isChill", true);
            // anim.SetBool("isShoked", false);
        }
        speed = angrySpeed;
    }
    void GoBack()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void PlayAudio()
    {
        SoundManager.snd.PlayDogSound();
    }
    public void PlayDogDizzy()
    {
        SoundManager.snd.PlayDogWhiningSounds();
        SoundManager.snd.PlayDizzySounds();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isShoked)
        {
            StartCoroutine(Stop());
        }
        if (collision.gameObject.tag.Equals("Undestroyed") && collision.relativeVelocity.magnitude > 6 && !isBroken)
        {
            StartCoroutine(BrokeCleaner());
            FirebaseAnalytics.LogEvent(name: "robot_cleaner_broken");
        }
    }
    IEnumerator Stop()
    {
        isShoked = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(5f);
        isShoked = false;
    }
    IEnumerator BrokeCleaner()
    {
        isBroken = true;
        SoundManager.snd.PlayPlasticImpactBigSounds();
        Instantiate(smoke, new Vector3(transform.position.x, transform.position.y - yCorrection, 0), Quaternion.identity);
        electro.SetActive(true);
        sound.enabled = false;
        _textBonus.SetActive(true);
        _text.text = $"+ {secondsAdd} {secText}";
        timer.AddSecondsToTimer(secondsAdd);
        yield return new WaitForSeconds(5f);
    }

}
