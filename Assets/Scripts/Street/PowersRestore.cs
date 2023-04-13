using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowersRestore : MonoBehaviour
{
    [SerializeField] private GameObject firstPowerRestore;
    [SerializeField] private GameObject powerRestoreButton;
    [SerializeField] private GameObject[] players;
    [SerializeField] private EnergyCat catEnergy;
    [SerializeField] private FloatSO catPowersSO;
    [SerializeField] private float powersToRestore;
    [SerializeField] private Text amountPowers;
    //[SerializeField] private GameObject particles;

    private void Awake()
    {
        catEnergy = players[PlayerPrefs.GetInt("Player")].transform.gameObject.GetComponent<EnergyCat>();
        catPowersSO = catEnergy.powerSO;
        powersToRestore = PlayerPrefs.GetFloat("countPowersToRestore", powersToRestore);

        if (powersToRestore > 0)
        {
            powerRestoreButton.SetActive(true);
            amountPowers.text = $"x{powersToRestore}";
        }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("firstPowerRestore") == false && catPowersSO.Value < 3f)
        {
            firstPowerRestore.SetActive(true);
            PlayerPrefs.SetInt("firstPowerRestore", 1);
            powersToRestore++;
            amountPowers.text = $"x{powersToRestore}";
            PlayerPrefs.SetFloat("countPowersToRestore", powersToRestore);
        }
    }

    public void RestoreEnergy()
    {
        if (catPowersSO.Value <= 9f && powersToRestore >= 1)
        {
            int i = PlayerPrefs.GetInt("Player");
            catEnergy = players[i].transform.gameObject.GetComponent<EnergyCat>();
            catEnergy.EnergyRestore();
            powersToRestore--;
            amountPowers.text = $"x{powersToRestore}";
            PlayerPrefs.SetFloat("countPowersToRestore", powersToRestore);

            if (powersToRestore == 0)
            {
                powerRestoreButton.SetActive(false);
                //particles.SetActive(true);
                //Instantiate(particles, transform.position, Quaternion.identity);
            }
        }
    }
}
