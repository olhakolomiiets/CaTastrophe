using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour

{
    private Inventory inventory;
    public int i;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
            Debug.Log("TEEEEEESTTTTTT");
        }
    }
}