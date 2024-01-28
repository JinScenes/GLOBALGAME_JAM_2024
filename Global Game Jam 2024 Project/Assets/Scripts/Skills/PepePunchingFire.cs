using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepePunchingFire : MonoBehaviour
{
    bool dealtDamage = false;
    public GameObject enemyPlayer;
    public float speed;
    public float aliveTime;
    bool canFollow = false;
    public GameObject fist;

    private bool attacking = false;

    private void Start()
    {
        Vector3 targetPosition = new Vector3(enemyPlayer.transform.position.x, enemyPlayer.transform.position.y, transform.position.z);

        // Move towards the player smoothly
        transform.position = targetPosition;
        StartCoroutine(DoPunches());
    }

    IEnumerator DoPunches()
    {
       while(aliveTime > 0)
        {
            yield return new WaitForSeconds(2);
            StartCoroutine(Punch());
        }

    }

    IEnumerator Punch()
    {
        attacking = true;
        dealtDamage = false;
        canFollow = true;
        fist.SetActive(true);
        yield return new WaitForSeconds(1);
        attacking = false;
        fist.SetActive(false);



    }



    private void OnTriggerStay(Collider other)
    {
        if (enabled && !dealtDamage && other.gameObject == enemyPlayer && attacking)
        {
            attacking=false;
            dealtDamage = true;
            print("HIT THE GUY");
            //Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (attacking)
        {
            // Get the current position of the object
            Vector3 currentPosition = transform.position;

            // Set the new position based on the player's position (X and Y axes only)
            Vector3 targetPosition = new Vector3(enemyPlayer.transform.position.x, enemyPlayer.transform.position.y, currentPosition.z);

            // Move towards the player smoothly
            transform.position = Vector3.Lerp(currentPosition, targetPosition, speed * Time.deltaTime);

        }

        aliveTime -= 1 * Time.deltaTime;
        if (aliveTime < 0) { Destroy(gameObject); }
    }
}
