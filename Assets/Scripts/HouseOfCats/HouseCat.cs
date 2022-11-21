using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HouseCat : MonoBehaviour, IClickable
{
    public float sleepTimer;
    public float eatTimer;
    public float washTimer;
    public float poopTimer;
    public float hideTimer;
    public float barpTimer;
    public float playTimer;
    public float drinkTimer;
    public float sportTimer;
    public Animator anim;
    private StateMashine stateMashine;
    // [SerializeField] public GameObject bar;
    [SerializeField] public GameObject barTop;
    [Header("Places for States")]
    [Tooltip("Places for different States")]
    [SerializeField] public Transform chair;
    [SerializeField] public Transform plate;
    [SerializeField] public Transform foodPlaceLeft;
    [SerializeField] public Transform foodPlaceRight;
    [SerializeField] public Transform sofa;
    [SerializeField] public Transform house;
    [SerializeField] public Animator animHouse;
    [SerializeField] public Transform toilet;
    [SerializeField] public Transform ballPlace;
    [SerializeField] public Transform drinkPlaceLeft;
    [SerializeField] public Transform drinkPlaceRight;
    [SerializeField] public Transform drinkPlace;
    [SerializeField] public Transform sportPlace;
    public Text nameOfCat;
    private int goHiding;
    private int goEating;
    private int goSleeping;
    private int goWashing;
    private int goPooping;
    private int goBarp;
    private int goPlay;
    private int goDrink;
    private int goSport;
    private int goStrike;
    public bool isEating;
    public bool isBarping;
    public bool isSleeping;
    public bool isHiding;
    public bool isWashing;
    public bool isPooping;
    public bool isPlaying;
    public bool isDrinking;
    public bool isSporting;
    public bool isStriking;
    private SortingGroup catSortingGroup;
    public static Rigidbody2D rb;
    // public BoxCollider2D col;
    public bool jump;
    [SerializeField]
    private FloatSO powerCat;
    public int washingAnim;
    public int sportingAnim;
    public bool animFinished = true;
    public FoodUpgradeHandler foodUpgradeHandler;
    public bool IsFoodInPlate;
    public delegate void UpdateNameDelegate();
    public static event UpdateNameDelegate UpdateNameEvent;
    private int i;

    private void Awake()
    {

        stateMashine = GetComponent<StateMashine>();
        Dictionary<Type, State> allStates = new Dictionary<Type, State>(){
           {typeof(StateHide), new StateHide(this)},
           {typeof(StateEat), new StateEat(this)},
           {typeof(StateSleep), new StateSleep(this)},
           {typeof(StateWashing), new StateWashing(this)},
           {typeof(StatePoop), new StatePoop(this)},
           {typeof(StateBarp), new StateBarp(this)},
           {typeof(StatePlaying), new StatePlaying(this)},
           {typeof(StateDrink), new StateDrink(this)},
           {typeof(StateSport), new StateSport(this)},
           {typeof(StateStriking), new StateStriking(this)},
        };
        stateMashine.SetUpStates(allStates, typeof(StateStriking));
        catSortingGroup = this.GetComponent<SortingGroup>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        foodUpgradeHandler = plate.GetComponent<FoodUpgradeHandler>();
        StartCoroutine(SetNamesOnHouses());
        NameOfCat.UpdateEvent += SetNameUpdate;
        i = UnityEngine.Random.RandomRange(1, 5);
    }
    public void ChangePowerBy(float amount)
    {
        powerCat.ChangeAmountBy(amount);
    }
    public float PowerValue()
    {
        return powerCat.Value;
    }
    private void Update()
    {
        if (isEating)
        {
            StartCoroutine(GoEating());
        }
        if (isSleeping)
        {
            StartCoroutine(GoSleeping());
        }
        if (isHiding)
        {
            StartCoroutine(GoHiding());
        }
        if (isWashing)
        {
            StartCoroutine(GoWashing());
        }
        if (isPooping)
        {
            StartCoroutine(GoPooping());
        }
        if (isBarping)
        {
            StartCoroutine(GoBarp());
        }
        if (isPlaying)
        {
            StartCoroutine(GoPlay());
        }
        if (isDrinking)
        {
            StartCoroutine(GoDrinking());
        }
        if (isSporting)
        {
            StartCoroutine(GoSporting());
        }
        if (isStriking)
        {
            StartCoroutine(GoStriking());
        }
        // bar.transform.position = Vector3.MoveTowards(bar.transform.position, 
        // new Vector3(transform.position.x, transform.position.y + 4f, bar.transform.position.z), 15 * Time.deltaTime);
    }
    public void Click()
    {
        ClickManager.CallCloseAllPanels();
        barTop.transform.parent.gameObject.GetComponent<EnergyCat>().UpdateEnergy();
        barTop.SetActive(true);
    }

    IEnumerator GoEating()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > foodPlaceRight.position.x + 2)
        {
            goEating = 2;
        }
        else if (transform.position.x < foodPlaceLeft.position.x - 2)
        {
            goEating = 1;
        }
        else if (transform.position.x == foodPlaceLeft.position.x || transform.position.x == foodPlaceRight.position.x)
        {
            goEating = 3;
        }
        if (transform.position.x > plate.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < plate.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goEating == 1)
        {

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(foodPlaceLeft.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goEating == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(foodPlaceRight.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goEating == 3)
        {
            StartCoroutine(Eat());

        }
    }
    IEnumerator GoSleeping()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > sofa.position.x + 2)
        {
            goSleeping = 2;
        }
        else if (transform.position.x < sofa.position.x - 2)
        {
            goSleeping = 1;
        }
        else if (transform.position.x == sofa.position.x)
        {
            goSleeping = 3;
        }
        if (transform.position.x > sofa.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < sofa.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goSleeping == 1)
        {

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(sofa.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goSleeping == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(sofa.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goSleeping == 3)
        {
            StartCoroutine(Sleep());
        }
    }
    IEnumerator GoHiding()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > house.position.x + 2)
        {
            goHiding = 2;
        }
        else if (transform.position.x < house.position.x - 2)
        {
            goHiding = 1;
        }
        else if (transform.position.x == house.position.x)
        {
            goHiding = 3;
        }
        if (transform.position.x > house.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < house.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goHiding == 1)
        {

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(house.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goHiding == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(house.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goHiding == 3)
        {
            if (isHiding)
            {
                StartCoroutine(Hide());
            }
        }
    }
    IEnumerator GoWashing()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > chair.position.x + 2)
        {
            goWashing = 2;
        }
        else if (transform.position.x < chair.position.x - 2)
        {
            goWashing = 1;
        }
        else if (transform.position.x == chair.position.x)
        {
            goWashing = 3;
        }
        if (transform.position.x > chair.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < chair.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goWashing == 1)
        {

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(chair.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goWashing == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(chair.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goWashing == 3)
        {

            StartCoroutine(Wash());
        }
    }
    IEnumerator GoPooping()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > toilet.position.x + 2)
        {
            goPooping = 2;
        }
        else if (transform.position.x < toilet.position.x - 2)
        {
            goPooping = 1;
        }
        else if (transform.position.x == toilet.position.x)
        {
            goPooping = 3;
        }
        if (transform.position.x > toilet.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < toilet.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goPooping == 1)
        {

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(toilet.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goPooping == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(toilet.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goPooping == 3)
        {
            StartCoroutine(Poop());

        }
    }
    IEnumerator GoBarp()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > (chair.position.x - 5f))
        {
            goBarp = 2;
        }
        else if (transform.position.x < (chair.position.x - 5f))
        {
            goBarp = 1;
        }
        else if (transform.position.x == (chair.position.x - 5f))
        {
            goBarp = 3;
        }
        if (transform.position.x > (chair.position.x - 5f) + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < (chair.position.x - 5f) - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goBarp == 1)
        {

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3((chair.position.x - 5f), transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goBarp == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3((chair.position.x - 5f), transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goBarp == 3)
        {
            StartCoroutine(Barp());
        }
    }
    IEnumerator GoPlay()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > ballPlace.position.x + 2)
        {
            goPlay = 2;
        }
        else if (transform.position.x < ballPlace.position.x - 2)
        {
            goPlay = 1;
        }
        else if (transform.position.x == ballPlace.position.x)
        {
            goPlay = 3;
        }
        if (transform.position.x > ballPlace.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < ballPlace.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goPlay == 1)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ballPlace.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goPlay == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ballPlace.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goPlay == 3)
        {
            StartCoroutine(Play());
        }
    }
    IEnumerator GoDrinking()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > drinkPlaceRight.position.x + 2)
        {
            goDrink = 2;
        }
        else if (transform.position.x < drinkPlaceLeft.position.x - 2)
        {
            goDrink = 1;
        }
        else if (transform.position.x == drinkPlaceLeft.position.x || transform.position.x == drinkPlaceRight.position.x)
        {
            goDrink = 3;
        }
        if (transform.position.x > drinkPlace.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < drinkPlace.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goDrink == 1)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(drinkPlaceLeft.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goDrink == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(drinkPlaceRight.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goDrink == 3)
        {
            StartCoroutine(Drink());
        }
    }
    IEnumerator GoSporting()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > sportPlace.position.x + 2)
        {
            goSport = 2;
        }
        else if (transform.position.x < sportPlace.position.x - 2)
        {
            goSport = 1;
        }
        else if (transform.position.x == sportPlace.position.x)
        {
            goSport = 3;
        }
        if (transform.position.x > sportPlace.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < sportPlace.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goSport == 1)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(sportPlace.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goSport == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(sportPlace.position.x, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goSport == 3)
        {
            StartCoroutine(Sport());
        }
    }
    IEnumerator GoStriking()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position.x > foodPlaceRight.position.x + 2 + i)
        {
            goStrike = 2;
        }
        else if (transform.position.x < foodPlaceLeft.position.x - 2 - i)
        {
            goStrike = 1;
        }
        else if (transform.position.x == foodPlaceLeft.position.x - i || transform.position.x == foodPlaceRight.position.x + i)
        {
            goStrike = 3;
        }
        if (transform.position.x > plate.position.x + 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < plate.position.x - 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (goStrike == 1)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(foodPlaceLeft.position.x - i, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goStrike == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(foodPlaceRight.position.x + i, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else if (goStrike == 3)
        {
            StartCoroutine(Strike());
        }
    }
    private void FixedUpdate()
    {
        StartCoroutine(Jump());
    }
    IEnumerator Eat()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isEating", true);
        eatTimer -= 1f * Time.deltaTime;
        yield return new WaitForSeconds(2f);
        if (!isEating)
        {
            anim.SetBool("isEating", false);
        }
    }
    IEnumerator Sleep()
    {
        if (isSleeping)
        {
            sleepTimer -= 1 * Time.deltaTime;
            anim.SetBool("isRunning", false);
            anim.SetBool("isSleeping", true);
        }
        yield return new WaitForSeconds(1f);
        if (!isSleeping)
        {
            anim.SetBool("isSleeping", false);
        }
    }
    IEnumerator Hide()
    {
        anim.SetBool("isRunning", false);
        catSortingGroup.sortingLayerName = "Default";
        animHouse.SetBool("isHiding", true);
        hideTimer -= 1 * Time.deltaTime;
        yield return new WaitForSeconds(1f);
        if (!isHiding)
        {
            catSortingGroup.sortingLayerName = "Player";
            animHouse.SetBool("isHiding", false);
        }
    }
    IEnumerator Wash()
    {
        if (isWashing)
        {
            anim.SetBool("isRunning", false);
            switch (washingAnim)
            {
                case 1:
                    anim.SetBool("isWashing", true);
                    break;
                case 2:
                    anim.SetBool("isWashing2", true);
                    break;
                default:
                    anim.SetBool("isWashing2", true);
                    break;
            }
        }
        washTimer -= 1 * Time.deltaTime;
        yield return new WaitForSeconds(2f);
        if (!isWashing)
        {
            switch (washingAnim)
            {
                case 1:
                    anim.SetBool("isWashing", false);
                    break;
                case 2:
                    anim.SetBool("isWashing2", false);
                    break;
                default:
                    anim.SetBool("isWashing2", false);
                    break;
            }
        }
        else if (washTimer < -3)
        {
            animFinished = true;
        }
    }
    IEnumerator Poop()
    {
        if (isPooping)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isPooping", true);
            poopTimer -= 1f * Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        if (!isPooping)
        {
            anim.SetBool("isPooping", false);
        }
    }
    IEnumerator Barp()
    {
        if (isBarping)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBarping", true);
            barpTimer -= 1 * Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        if (!isBarping)
        {
            anim.SetBool("isBarping", false);
        }
          else if (barpTimer < -3)
        {
            animFinished = true;
        }
    }
    IEnumerator Play()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (isPlaying)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isPlaying", true);
            rb.isKinematic = true;
            playTimer -= 1 * Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        if (!isPlaying)
        {
            anim.SetBool("isPlaying", false);
            rb.isKinematic = false;
        }
    }
    IEnumerator Drink()
    {
        if (isDrinking)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isDrinking", true);
            drinkTimer -= 1f * Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        if (!isDrinking)
        {
            anim.SetBool("isDrinking", false);
        }
    }
    IEnumerator Sport()
    {
        anim.SetBool("isRunning", false);
        sportTimer -= 1 * Time.deltaTime;
        switch (sportingAnim)
        {
            case 1:
                anim.SetBool("isSporting", true);
                break;
            case 2:
                anim.SetBool("isHunting", true);
                break;
            default:
                anim.SetBool("isSporting", true);
                break;
        }
        yield return new WaitForSeconds(2f);
        if (!isSporting)
        {
            switch (sportingAnim)
            {
                case 1:
                    anim.SetBool("isSporting", false);
                    break;
                case 2:
                    anim.SetBool("isHunting", false);
                    break;
                default:
                    anim.SetBool("isSporting", false);
                    break;
            }
        }
        else if (sportTimer < -3)
        {
            animFinished = true;
        }
    }
    IEnumerator Strike()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isStriking", true);
        yield return new WaitForSeconds(1.5f);
        if (!isStriking)
        {
            anim.SetBool("isStriking", false);
        }
    }
    IEnumerator Jump()
    {
        if (jump && transform.position.x == chair.position.x)
        {
            transform.position = new Vector3(chair.position.x, chair.position.y + 0.1f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            jump = false;
        }
    }
    public IEnumerator ChangeAnim()
    {
        animFinished = true;
        yield return new WaitForSeconds(0f);
        // animFinished = false;
    }
    public bool IsCatStriking()
    {
        if (foodUpgradeHandler.isFoodInPlate())
        {
            IsFoodInPlate = false;
            return false;
        }
        else
        {
            IsFoodInPlate = true;
            return true;
        }
    }
    public IEnumerator SetNamesOnHouses()
    {
        yield return new WaitForSeconds(0.2f);
        string nameCat = barTop.GetComponentInParent<NameOfCatLoad>().nameOfCat;
        nameOfCat.text = nameCat;
    }
    void SetNameUpdate()
    {
        StartCoroutine(SetNamesOnHouses());
    }
    private void OnDisable()
    {
        NameOfCat.UpdateEvent -= SetNameUpdate;
    }
}