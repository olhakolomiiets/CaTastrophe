using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToCollect : MonoBehaviour
{
    public SpriteRenderer sprite;
    public WallObjectsFallLogic _wallObjectsFallLogic;
    [SerializeField] private bool isAddingLife;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {

            if (isAddingLife)
            {
                collision.GetComponent<CowController>().AddLife();
            }
            else
            {
                _wallObjectsFallLogic.UpdateCollectedAmount();                
            }
            this.gameObject.SetActive(false);
        }
    }
}
