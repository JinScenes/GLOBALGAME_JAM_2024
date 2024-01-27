using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NOOTFIRE : MonoBehaviour
{
    bool dealtDamage = false;
    public GameObject enemyPlayer;
    public float speed;
    public float aliveTime;

    private void OnTriggerStay(Collider other)
    {
        if (enabled && !dealtDamage && other.gameObject == enemyPlayer)
        {
            dealtDamage = true;
            print("HIT THE GUY");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
        aliveTime-=1*Time.deltaTime;
        if(aliveTime < 0) { Destroy(gameObject); }
    }
}
