using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItems : MonoBehaviour
{
    [SerializeField]
    Item Item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.Instance.GetItems(Item);
            this.gameObject.SetActive(false);
        }
    }
}
