using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItems : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollectItem") 
        {
            Player.Instance.GetItems((Item)Resources.Load($"Items/{collision.gameObject.name}"));
            collision.gameObject.SetActive(false);
        }
    }
}
