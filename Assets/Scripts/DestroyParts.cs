using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParts : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDir;
    [SerializeField]
    int spin;
    Rigidbody2D rb;
    Collider2D collider2d;
    // Collider2D otherCollider2d;
    // LayerMask layer;
    // LayerMask destroyedLayer;
    // LayerMask ground;
    // ContactFilter2D destroyedFilter;
    // public bool isOnMovingFurniture;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        rb.AddForce(forceDir);
        rb.AddTorque(spin);
        StartCoroutine(SetStatic());
        // layer = LayerMask.GetMask("MovingFuniture");
        // destroyedLayer = LayerMask.GetMask("Destroyed");
        // ground = LayerMask.GetMask("Ground");
        // destroyedFilter.SetLayerMask(destroyedLayer);
    }
    IEnumerator SetStatic()
    {
        yield return new WaitForSeconds(10f);
        // if (collider2d.IsTouchingLayers(layer))
        // {
        //     if (collider2d.IsTouching(otherCollider2d, destroyedFilter))
        // {
        //     // otherCollider2d.gameObject.SetActive(false);
        //     Debug.Log("otherCollider2d   " + otherCollider2d.gameObject.name);
        //     // otherCollider2d.GetComponent<DestroyParts>().isOnMovingFurniture=true;
        // }
        //     Debug.Log("Touch");
        // }
        // else if (isOnMovingFurniture==false)
        // {
            rb.bodyType = RigidbodyType2D.Static;
            collider2d.enabled = false;
        // }

    }
}
