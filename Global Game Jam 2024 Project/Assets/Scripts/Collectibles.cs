using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public string itemName; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int playerNumber = other.GetComponent<Phase1PlayerController>().playerNum; 

            Inventory.AddItem(itemName, playerNumber);

            Destroy(gameObject);
        }
    }
}
