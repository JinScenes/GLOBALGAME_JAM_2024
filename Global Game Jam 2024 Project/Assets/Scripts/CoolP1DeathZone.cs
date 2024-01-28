using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CoolP1DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }
}
