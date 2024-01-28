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
            other.gameObject.GetComponent<PlayerController>().TakeDamage(15f);
            StartCoroutine(other.gameObject.GetComponent<PlayerComba>().ApplyPushForce(other.gameObject.GetComponent<PlayerComba>(), Vector3.right, false));
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
