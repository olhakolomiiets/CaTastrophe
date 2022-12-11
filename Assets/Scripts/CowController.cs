using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class CowController : MonoBehaviour
{
    public string ppCatName;
    public float speed;
    public float normalSpeed;
    public float jumpForce;
    private float moveInput;
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    [SerializeField]
    public LayerMask whatIsGround;
    public LayerMask whatIsGround2;
    public LayerMask whatIsGround3;
    public LayerMask whatIsGround4;
    public LayerMask whatIsGround5;
    public LayerMask whatIsGround6;
    float dirX;
    [SerializeField]
    float moveSpeed = 5f;
    public static Rigidbody2D rb;
    [SerializeField]
    public static float lives;
    public int numOfHearts;
    public string enemy1;
    public string enemy2;
    public string enemy3;
    public string enemy4;
    public string enemy5;
    private Image[] hearts = new Image[4];
    public Sprite HeartFull;
    public Sprite HeartEmpty;
    private Animator anim;
    private GameObject LivesHearts;
    public float knockback;
    public float knockbackLenght = 0.3f;
    public float knockbackCount;
    public bool knockFromRight;
    public GameObject damageImage;
    public GameObject canvas;
    public bool noAffraidDogs;
    public bool ExtraLife;
    private int timeForLowPower6;
    private float energyCat;
    public GameObject lowPower;
    private bool lowPowerModeON;
    private Button jump;
    private EventTrigger left;
    private EventTrigger right;
    private RigidbodyConstraints2D originalConstraints;
    public GameObject activeCollaider;
    private bool isWaiting;
    private Button leftButton;
    private Button rightButton;
    private Button doButton;
    public FloatSO catPower;

    private void Awake()
    {
         #  region SetupCheatPowers

         if (PlayerPrefs.GetInt(ppCatName + "CheatEnemyDog") == 2)
        {
            enemy1 = "";
            noAffraidDogs = true;
        }
        else enemy1 = "EnemyDog";

         if (PlayerPrefs.GetInt(ppCatName + "PowerAntiPlant") == 2)
        {
            enemy2 = "";
        }
        else enemy2 = "EnemyPlant";

        if (PlayerPrefs.GetInt(ppCatName + "PowerAntiSteam") == 2)
        {
            enemy3 = "";
        }
        else enemy3 = "EnemySteam";

          if (PlayerPrefs.GetInt(ppCatName + "PowerAntiFan") == 2)
        {
            enemy4 = "";
        }
        else enemy4 = "EnemyFan";
        
        if (PlayerPrefs.GetInt(ppCatName + "PowerWeightShop") == 2)
        {
            activeCollaider.tag = "ActiveCollaiderHeavy";
        }
        else activeCollaider.tag = "ActiveCollaider";
        
        #endregion
        
        // int i = PlayerPrefs.GetInt("Player");
        // energyCat = PlayerPrefs.GetFloat("totalEnergyCat" + i);
    }
    private void OnEnable()
    {
        // int i = PlayerPrefs.GetInt("Player");
        // energyCat = PlayerPrefs.GetFloat("totalEnergyCat" + i);
        // Debug.Log("Enable +++++++++++++++++++++++++++++++EnergyCat totalEnergyCat4 is " + PlayerPrefs.GetFloat("totalEnergyCat" + i) +" where i =" +i);
    }
    void Start()
    {
        feetPos = gameObject.transform.Find("feetPos").transform;
        anim = GetComponent<Animator>();
        speed = 0f;
        rb = GetComponent<Rigidbody2D>();
        LivesHearts = GameObject.FindGameObjectWithTag("Lives");
        hearts[0] = LivesHearts.transform.GetChild(0).GetComponent<Image>(); ;
        hearts[1] = LivesHearts.transform.GetChild(1).GetComponent<Image>(); ;
        hearts[2] = LivesHearts.transform.GetChild(2).GetComponent<Image>(); ;
        hearts[3] = LivesHearts.transform.GetChild(3).GetComponent<Image>(); ;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        damageImage = canvas.gameObject.transform.GetChild(10).gameObject;
        lowPower = canvas.gameObject.transform.GetChild(17).gameObject;
        jump = canvas.transform.GetChild(0).GetComponent<Button>();
        left = canvas.transform.GetChild(2).GetComponent<EventTrigger>();
        right = canvas.transform.GetChild(3).GetComponent<EventTrigger>();
        doButton = canvas.transform.GetChild(1).GetComponent<Button>();
        leftButton = left.gameObject.GetComponent<Button>();
        rightButton = right.gameObject.GetComponent<Button>();
        originalConstraints = rb.constraints;
        if (PlayerPrefs.GetInt("extraLife") == 1)
        {
            numOfHearts = 4;
            ExtraLife = true;
            lives = 4;
            hearts[3].enabled = true;
        }
        else if (PlayerPrefs.GetInt("extraLife") == 0)
        {
            numOfHearts = 3;
            lives = 3;
            hearts[3].enabled = false;
        }
        Debug.Log("+++++++++++++++++++++++++++++++EnergyCat totalEnergyCat0 is " + PlayerPrefs.GetFloat("totalEnergyCat0"));
        Debug.Log("+++++++++++++++++++++++++++++++EnergyCat totalEnergyCat4 is " + PlayerPrefs.GetFloat("totalEnergyCat4"));
        if (catPower.Value <= 0.05f)
        {
            lowPower.SetActive(true);
            lowPowerModeON = true;
        }
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround | whatIsGround2 | whatIsGround3 | whatIsGround4 | whatIsGround5 | whatIsGround6);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");
        }

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.A))
            OnLeftButtonDown();
        else if (Input.GetKeyDown(KeyCode.D))
            OnRightButtonDown();

        if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D))
        {
            OnButtonUp();
        }

        if (lowPowerModeON)
        {
            if (isGrounded == true)
            {
                if (isWaiting == false)
                {
                    StartCoroutine(waiter());
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (lives > numOfHearts)
        {
            lives = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(lives))
            {
                hearts[i].sprite = HeartFull;
            }
            else
            {
                hearts[i].sprite = HeartEmpty;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (lives < 1)
            {
                Camera.main.GetComponent<UIManager>().Lose();
                PlayerPrefs.SetInt("AwardDiedTimes", PlayerPrefs.GetInt("AwardDiedTimes") + 1);
                damageImage.SetActive(false);
            }
        }
        if (knockbackCount <= 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            if (knockFromRight)
                rb.velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                rb.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }
        if (speed != 0f)
        {
            anim.SetBool("isRunning", true);
        }
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            OnRightButtonDown();
        }
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            OnLeftButtonDown();
        }
        if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D) | Input.GetButtonUp("Horizontal"))
        {
            OnButtonUp();
        }
    }
    public void OnJumpbuttonDown()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");
        }
    }
    public void OnLeftButtonDown()
    {
        if (speed <= 0f)
        {
            speed = -normalSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void OnRightButtonDown()
    {
        if (speed >= 0f)
        {
            speed = normalSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void OnButtonUp()
    {
        speed = 0f;
        anim.SetBool("isRunning", false);
    }
    public void GoForAnimation(bool goRight)
    {
        if (goRight)
        {
        if (speed >= 0f)
        {
            speed = 7f;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        }
        else
        {
        if (speed <= 0f)
        {
            speed = -7f;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == enemy1 | other.gameObject.tag == enemy2 | other.gameObject.tag == enemy3 | other.gameObject.tag == enemy4 | other.gameObject.tag == enemy5)
        {
            lives--;
            SoundManager.snd.PlayDamage();
            StartCoroutine(DamageImage());
            knockbackCount = knockbackLenght;
            if (transform.position.x < other.transform.position.x)
                knockFromRight = true;
            else
                knockFromRight = false;
        }
        if (other.gameObject.tag == "Robot")
            transform.parent = other.gameObject.transform;
    }
    IEnumerator DamageImage()
    {
        damageImage.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        damageImage.SetActive(false);
    }
    public void LivesUp()
    {
        lives++;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Robot")
            transform.parent = null;
    }
    private IEnumerator LowPower1()
    {
        OnButtonUp();
        left.enabled = false;
        right.enabled = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        jump.interactable = false;
        doButton.interactable = false;
        yield return new WaitForSeconds(0.4f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // rb.isKinematic = true;
        anim.SetBool("lowPower", true);
        yield return new WaitForSeconds(6f);
        // rb.isKinematic = false;
        rb.constraints = originalConstraints;
        anim.SetBool("lowPower", false);
        // left.enabled = true;
        // right.enabled = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        jump.interactable = true;
        doButton.interactable = true;
    }
    private IEnumerator LowPower2()
    {
        OnButtonUp();
        yield return new WaitForSeconds(0.4f);
        // rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        left.enabled = false;
        right.enabled = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        jump.interactable = false;
        doButton.interactable = false;
        anim.SetBool("lowPower2", true);
        yield return new WaitForSeconds(6f);
        // rb.isKinematic = false;
        rb.constraints = originalConstraints;
        anim.SetBool("lowPower2", false);
        left.enabled = true;
        right.enabled = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        jump.interactable = true;
        doButton.interactable = true;
    }
    private IEnumerator LowPower3()
    {
        OnButtonUp();
        yield return new WaitForSeconds(0.4f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        left.enabled = false;
        right.enabled = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        jump.interactable = false;
        doButton.interactable = false;
        anim.SetBool("lowPower3", true);
        yield return new WaitForSeconds(6f);
        rb.constraints = originalConstraints;
        anim.SetBool("lowPower3", false);
        left.enabled = true;
        right.enabled = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        jump.interactable = true;
        doButton.interactable = true;
    }
    private IEnumerator LowPower4()
    {
        OnButtonUp();
        yield return new WaitForSeconds(0.4f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        left.enabled = false;
        right.enabled = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        jump.interactable = false;
        doButton.interactable = false;
        anim.SetBool("lowPower4", true);
        yield return new WaitForSeconds(4f);
        rb.constraints = originalConstraints;
        anim.SetBool("lowPower4", false);
        left.enabled = true;
        right.enabled = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        jump.interactable = true;
        doButton.interactable = true;
    }
    private IEnumerator LowPower5()
    {
        OnButtonUp();
        yield return new WaitForSeconds(0.4f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        left.enabled = false;
        right.enabled = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        jump.interactable = false;
        doButton.interactable = false;
        anim.SetBool("lowPower5", true);
        yield return new WaitForSeconds(5f);
        rb.constraints = originalConstraints;
        anim.SetBool("lowPower5", false);
        left.enabled = true;
        right.enabled = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        jump.interactable = true;
        doButton.interactable = true;
        isWaiting = false;
    }
    IEnumerator waiter()
    {
        isWaiting = true;
        int wait_time = UnityEngine.Random.Range(10, 15);
        int courutineIndex = UnityEngine.Random.Range(1, 6);
        yield return new WaitForSeconds(wait_time);
        OnButtonUp();
        switch (courutineIndex)
        {
            case 1:
                StartCoroutine(LowPower1());
                break;
            case 2:
                StartCoroutine(LowPower2());
                break;
            case 3:
                StartCoroutine(LowPower3());
                break;
            case 4:
                StartCoroutine(LowPower4());
                break;
            case 5:
                StartCoroutine(LowPower5());
                break;
            default:
                StartCoroutine(LowPower1());
                break;
        }
        print("I started courutine " + courutineIndex + "after " + wait_time + " sec ");
        isWaiting = false;
        print("isWaiting Set to - " + isWaiting);
    }
}
