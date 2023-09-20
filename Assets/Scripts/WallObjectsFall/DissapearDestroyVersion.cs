using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearDestroyVersion : MonoBehaviour
{
    public float destroyDuration = 5.0f;

    private void Start()
    {
        StartCoroutine(DestroyThis());
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(destroyDuration);
        Destroy(this.gameObject);

    }
}
