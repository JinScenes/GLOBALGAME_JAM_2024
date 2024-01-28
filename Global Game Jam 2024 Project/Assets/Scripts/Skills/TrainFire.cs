using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainFire : MonoBehaviour
{
    bool dealtDamageToUser = false;
    bool dealtDamageToEnemyPlayer = false;

    public GameObject enemyPlayer;

    public float waitTimeTillCanHitAgain = .5f;
    public float speed;
    public float aliveTime;

    private void OnTriggerStay(Collider other)
    {
        if (enabled && other.gameObject.tag == "Player")
        {
            if(other.gameObject == enemyPlayer)
            {
                if (!dealtDamageToEnemyPlayer){
                    dealtDamageToEnemyPlayer = true;
                    StartCoroutine(StartCDForEnemy());
                    other.gameObject.GetComponent<PlayerController>().TakeDamage(30f);
                    StartCoroutine(other.gameObject.GetComponent<PlayerComba>().ApplyPushForce(other.gameObject.GetComponent<PlayerComba>(), Vector3.right, false));
                }
            }
            else
            {
                if (!dealtDamageToUser){
                    dealtDamageToUser = true;
                    StartCoroutine(StartCDForUser());
                    other.gameObject.GetComponent<PlayerController>().TakeDamage(30f);
                    StartCoroutine(other.gameObject.GetComponent<PlayerComba>().ApplyPushForce(other.gameObject.GetComponent<PlayerComba>(), Vector3.right, false));
                }
               
            }
            
        }
    }

    IEnumerator StartCDForUser()
    {
        yield return new WaitForSeconds(waitTimeTillCanHitAgain);
        dealtDamageToUser = false;
    }

    IEnumerator StartCDForEnemy()
    {
        yield return new WaitForSeconds(waitTimeTillCanHitAgain);
        dealtDamageToEnemyPlayer = false;
    }

    private void Update()
    {
        transform.position += (Time.deltaTime * speed * transform.forward);
        aliveTime -= 1 * Time.deltaTime;
        if (aliveTime < 0) { Destroy(gameObject); }
    }
}
