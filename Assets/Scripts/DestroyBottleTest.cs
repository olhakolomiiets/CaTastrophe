using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBottleTest : MonoBehaviour
{
    [SerializeField] private DestroyPartsTest[] _bottleParts;

    void Start()
    {
        StartCoroutine(SetStatic());
    }
    IEnumerator SetStatic()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _bottleParts.Length; i++)
        {
            _bottleParts[i].Rb.bodyType = RigidbodyType2D.Static;
            _bottleParts[i].Collider2d.enabled = false;
        }
    }
}
