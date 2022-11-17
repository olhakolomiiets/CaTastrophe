using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarCat : MonoBehaviour
{
    Vector3 localScale;
    [SerializeField]
    private HouseCat houseCat;
    private float power;
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        power = houseCat.PowerValue();
        localScale.y = power / 10;
        transform.localScale = localScale;
    }
}
