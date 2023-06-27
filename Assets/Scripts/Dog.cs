using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed;
    private float dogSpeed;
    public float angrySpeed;
    public float chillSpeed;
    [SerializeField] private int areaOfPatrol;
    [SerializeField] private int areaOfHunting;
    public Transform point;
    [SerializeField] private Transform pointPatrol;
    bool movingRight;
    private int movingAngry;
    private int goEating;
    Transform player;
    public float stopDistance;
    public bool chill = false;
    bool angry = false;
    bool goBack = false;
    bool angryBark = false;
    private Animator anim;
    public static Rigidbody2D rb;
    public bool noAffraid;
    private Rigidbody2D headRb;
    bool isShoked;
    private GameObject stars;
    bool isDizzy;
    public bool isEating;
    public Transform dogFood;
    private bool alreadyEat;

    void Awake()
    {

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dogSpeed = rb.velocity.magnitude;
        movingAngry = 0;
        // headRb = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
        stars = transform.GetChild(34).gameObject;
        noAffraid = player.GetComponent<CowController>().noAffraidDogs;
        if (noAffraid)
        {
            gameObject.layer = LayerMask.NameToLayer("PassiveEnemy");
        }

    }
    void Update()
    {
        if (isEating)
        {
            angry = false;
            chill = false;
            goBack = false;
            stars.SetActive(false);
        }
        if (!isEating)
        {
            if (isShoked)
            {
                StartCoroutine(DogShok());
                angry = false;
                chill = false;
                goBack = false;
            }
            if (!isShoked)
            {
                if (Vector2.Distance(transform.position, pointPatrol.position) < areaOfPatrol && angry == false)
                {
                    chill = true;
                }
                if (Vector2.Distance(transform.position, player.position) < stopDistance && noAffraid == false && Vector2.Distance(transform.position, point.position) < areaOfHunting)
                {
                    angry = true;
                    chill = false;
                    goBack = false;
                }
                if (Vector2.Distance(transform.position, player.position) > stopDistance)
                {
                    goBack = true;
                    angry = false;
                    // chill = false;
                }
                if (Vector2.Distance(transform.position, point.position) >= areaOfHunting && Vector2.Distance(transform.position, player.position) <= stopDistance && angry == true)
                {
                    angryBark = true;
                    goBack = false;
                    chill = false;
                    angry = false;
                }
            }
        }
        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
        else if (angryBark == true)
        {
            AngryBark();
        }
        if (isEating == true)
        {
            if (!alreadyEat)
            {
                anim.SetBool("isChill", true);
                anim.SetBool("isPatrol", false);
                anim.SetBool("isAngry", false);
                anim.SetBool("isShoked", false);
                alreadyEat = true;
            }
            StartCoroutine(GoEating());
        }
    }
    void Chill()
    {
        speed = chillSpeed;
        if (transform.position.x > pointPatrol.position.x + areaOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < pointPatrol.position.x - areaOfPatrol)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            anim.SetBool("isPatrol", true);
            anim.SetBool("isAngry", false);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            anim.SetBool("isPatrol", true);
            anim.SetBool("isAngry", false);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Angry()
    {
        if (transform.position.x > player.position.x + 2)
        {
            // transform.rotation = Quaternion.Euler(0, 180, 0);
            //  Invoke ("DogDelay", 1);
            // // StartCoroutine(DogDelayCorrutine2());
            movingAngry = 2;
        }
        else if (transform.position.x < player.position.x - 2)
        {
            // transform.rotation = Quaternion.Euler(0, 0, 0);
            // Invoke ("DogDelay2", 1);
            // // StartCoroutine(DogDelayCorrutine1());
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
            // transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", true);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (movingAngry == 2)
        {
            // transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", true);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (movingAngry == 3)
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", false);
            anim.SetBool("isChill", true);
            anim.SetBool("isShoked", false);
        }
        speed = angrySpeed;
    }
    void AngryBark()
    {
        anim.SetBool("isPatrol", false);
        anim.SetBool("isAngry", false);
        anim.SetBool("isChill", true);
        anim.SetBool("isShoked", false);
    }
    void GoBack()
    {
        if (transform.position.x > pointPatrol.position.x + areaOfHunting)
        {
            // StartCoroutine(DogDelayCorrutine2());
            movingRight = false;
        }
        else if (transform.position.x < pointPatrol.position.x - areaOfHunting)
        {
            // StartCoroutine(DogDelayCorrutine1());
            movingRight = true;
        }
        if (movingRight)
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", true);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = Vector2.MoveTowards(transform.position, pointPatrol.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", true);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = Vector2.MoveTowards(transform.position, pointPatrol.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    IEnumerator GoEating()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > dogFood.position.x + 2)
        {
            goEating = 2;
        }
        else if (transform.position.x < dogFood.position.x - 2)
        {
            goEating = 1;
        }
        else if (transform.position.x == dogFood.position.x)
        {
            goEating = 3;
        }
        if (transform.position.x > dogFood.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < dogFood.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goEating == 1)
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", true);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(dogFood.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (goEating == 2)
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", true);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(dogFood.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (goEating == 3)
        {
            anim.SetBool("isPatrol", false);
            anim.SetBool("isAngry", false);
            anim.SetBool("isChill", false);
            anim.SetBool("isShoked", false);
            anim.SetBool("isEating", true);
            rb.isKinematic = true;
            StartCoroutine(DogIsEating());
        }
        speed = angrySpeed;
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
        if (collision.gameObject.tag.Equals("Undestroyed") && collision.relativeVelocity.magnitude > 6 && !isShoked)
        {
            StartCoroutine(DogIsShoked());
            PlayerPrefs.SetInt("AwardDogsShoked", PlayerPrefs.GetInt("AwardDogsShoked") + 1);
            PlayDogDizzy();
        }
    }
    IEnumerator DogShok()
    {
        stars.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        anim.SetBool("isPatrol", false);
        anim.SetBool("isAngry", false);
        anim.SetBool("isChill", false);
        anim.SetBool("isShoked", true);
        yield return new WaitForSeconds(4f);
    }
    IEnumerator DogIsShoked()
    {
        isShoked = true;
        gameObject.tag = "Destroyed";
        yield return new WaitForSeconds(5f);
        stars.SetActive(false);
        gameObject.tag = "EnemyDog";
        isShoked = false;
    }

    public void WaitingTillNextBite()
    {
        StartCoroutine(DogBiteCat());
    }
    IEnumerator DogBiteCat()
    {
        gameObject.layer = LayerMask.NameToLayer("PassiveEnemy");
        yield return new WaitForSeconds(2.5f);
        stars.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
    IEnumerator DogIsEating()
    {
        gameObject.tag = "Destroyed";
        yield return new WaitForSeconds(10f);
        gameObject.tag = "EnemyDog";
        anim.SetBool("isEating", false);
        rb.isKinematic = false;
        isEating = false;
    }
}