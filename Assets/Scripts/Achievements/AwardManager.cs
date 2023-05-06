using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardManager : MonoBehaviour
{
    public static AwardManager instance = null;

    #region EditorFields

    [Header ("Right Grid")]
    [SerializeField] private List<AchieveSO> rightAchievesSO = new List<AchieveSO>();
    [SerializeField] private List<AchieveSO> housesAchievesSO = new List<AchieveSO>();
    [SerializeField] private GameObject _rightAwardsGrid;

    [Header("Left Grid")]
    [SerializeField] private List<AchieveSO> leftAchievesSO = new List<AchieveSO>();
    [SerializeField] private GameObject _leftAwardsGrid;

    [Header("Other")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _particlesStarsAchieve;

    #endregion

    #region PrivateFields

    private IEnumerator coroutineRightGrid;
    private IEnumerator coroutineHouses;
    private IEnumerator coroutineLeftGrid;
    #endregion


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        _camera.enabled = true;
    }

/*    private void Start()
    {
        GameObject newPrefabInstance1 = Instantiate(rightAchievesSO[0].AchieveLvl1, _rightAwardsGrid.transform);
        GameObject particlesPrefabInstance1 = Instantiate(_particlesStarsAchieve, newPrefabInstance1.transform);

        GameObject newPrefabInstance = Instantiate(leftAchievesSO[6].AchieveLvl1, _leftAwardsGrid.transform);
        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
    }*/

    IEnumerator CheckAhiveCorrRightGrid()
    {
        while (true)
        {
            for (int i = 0; i < rightAchievesSO.Count; i++)

            {
                if (rightAchievesSO[i].GetAchieveScore() >= rightAchievesSO[i].TargetNumberLvl1)
                {

                    if (PlayerPrefs.GetInt("AchievementState_1" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_1" + i, 1);

                        GameObject newPrefabInstance = Instantiate(rightAchievesSO[i].AchieveLvl1, _rightAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
                if (rightAchievesSO[i].GetAchieveScore() >= rightAchievesSO[i].TargetNumberLvl2)
                {

                    if (PlayerPrefs.GetInt("AchievementState_2" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_2" + i, 1);

                        GameObject newPrefabInstance = Instantiate(rightAchievesSO[i].AchieveLvl2, _rightAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
                if (rightAchievesSO[i].GetAchieveScore() >= rightAchievesSO[i].TargetNumberLvl3)
                {

                    if (PlayerPrefs.GetInt("AchievementState_3" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_3" + i, 1);

                        GameObject newPrefabInstance = Instantiate(rightAchievesSO[i].AchieveLvl3, _rightAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator CheckHousesAhiveCorrRightGrid()
    {
        while (true)
        {
            for (int i = 0; i < housesAchievesSO.Count; i++)

            {
                if (housesAchievesSO[i].GetAchieveScore() >= housesAchievesSO[i].TargetNumber
                && housesAchievesSO[i].GetAllStars() >= housesAchievesSO[i].NeedStars)
                {
                    if (PlayerPrefs.GetInt("AchievementHouseState_" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementHouseState_" + i, 1);

                        GameObject newPrefabInstance = Instantiate(housesAchievesSO[i].HouseAchieve, _rightAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator CheckAhiveCorrLeftGrid()
    {
        while (true)
        {
            for (int i = 0; i < leftAchievesSO.Count; i++)

            {
                if (leftAchievesSO[i].GetAchieveScore() >= leftAchievesSO[i].TargetNumberLvl1)
                {

                    if (PlayerPrefs.GetInt("AchievementState_1" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_1" + i, 1);

                        GameObject newPrefabInstance = Instantiate(leftAchievesSO[i].AchieveLvl1, _leftAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
                if (leftAchievesSO[i].GetAchieveScore() >= leftAchievesSO[i].TargetNumberLvl2)
                {

                    if (PlayerPrefs.GetInt("AchievementState_2" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_2" + i, 1);

                        GameObject newPrefabInstance = Instantiate(leftAchievesSO[i].AchieveLvl2, _leftAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
                if (leftAchievesSO[i].GetAchieveScore() >= leftAchievesSO[i].TargetNumberLvl3)
                {

                    if (PlayerPrefs.GetInt("AchievementState_3" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_3" + i, 1);

                        GameObject newPrefabInstance = Instantiate(leftAchievesSO[i].AchieveLvl3, _leftAwardsGrid.transform);
                        GameObject particlesPrefabInstance = Instantiate(_particlesStarsAchieve, newPrefabInstance.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);
                        Destroy(particlesPrefabInstance);
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }



    void OnEnable()
    {
        coroutineHouses = CheckHousesAhiveCorrRightGrid();
        coroutineRightGrid = CheckAhiveCorrRightGrid();
        coroutineLeftGrid = CheckAhiveCorrLeftGrid();

        StartCoroutine(coroutineHouses);
        StartCoroutine(coroutineRightGrid);
        StartCoroutine(coroutineLeftGrid);
    }

    void OnDisable()
    {
        StopCoroutine(coroutineHouses);
        StopCoroutine(coroutineRightGrid);
        StopCoroutine(coroutineLeftGrid);
    }
}