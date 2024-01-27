using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOATFire : MonoBehaviour
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
        transform.position += (Time.deltaTime * speed * transform.up)*-1;
        aliveTime -= 1 * Time.deltaTime;
        if (aliveTime < 0) { Destroy(gameObject); }
    }
}
