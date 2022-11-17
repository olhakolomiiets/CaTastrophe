using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    [SerializeField]
    private GameObject Slot;
    [SerializeField]
    private GameObject Slot2;
    private Transform player;
    private GameObject inventory;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        slots[0] = inventory.transform.GetChild(0).gameObject;
        slots[1] = inventory.transform.GetChild(1).gameObject;
        Slot = inventory.transform.GetChild(0).gameObject;
        Slot2 = inventory.transform.GetChild(1).gameObject;
    }
    public bool HasFullSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i])
            {
                return true;
            }
        }
        return false;
    }
    public void removeFromArray()
    {
        for (int i = 0; i < 1; i++)
        {
            if (isFull[1])
            {
                GameObject mySlotChild2 = Slot2.transform.GetChild(0).gameObject;
                Debug.Log("Inventory error" + Slot2.transform.childCount);
                Destroy(mySlotChild2);
                isFull[1] = false;
            }
            else if (isFull[0])
            {
                GameObject mySlotChild = Slot.transform.GetChild(0).gameObject;
                Debug.Log("Inventory error" + Slot.transform.childCount);
                Destroy(mySlotChild);
                isFull[0] = false;
            }
            else
            {
                Debug.Log("Inventory error");
            }
        }
    }
}