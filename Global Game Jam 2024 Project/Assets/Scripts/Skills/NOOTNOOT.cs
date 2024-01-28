using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOOTNOOT : Skill
{
    public float offset = 10;
    public float speed = 32f;
    public float aliveTime = 8f;
    public float timeInBetweenNOOTS = .5f;

    private void Start()
    {
        SetValues();
        //skillCD = 8f;
    }
    public override void UseSkill()
    {
        if (currentCD <= 0)
        {
            StartCoroutine(StartCooldown());

            StartCoroutine(FireNOOTS());
           
        }
        else
        {
            print("Its on CD: " + currentCD);
        }

    }
    
    IEnumerator FireNOOTS()
    {
        ShootNOOT();
        yield return new WaitForSeconds(timeInBetweenNOOTS);
        ShootNOOT();
    }

    void ShootNOOT()
    {
        GameObject NOOT = Instantiate(Resources.Load("NOOT"), player.transform.position + player.gameObject.transform.up * offset, Quaternion.identity, skillStuff.transform) as GameObject;
        NOOT.transform.LookAt(enemyPlayer.transform.position);
        NOOTFIRE NOOTFire = NOOT.GetComponent<NOOTFIRE>();

        NOOTFire.enemyPlayer = enemyPlayer;
        NOOTFire.speed = speed;
        NOOTFire.aliveTime = aliveTime;
        NOOTFire.enabled = true;
    }
}
