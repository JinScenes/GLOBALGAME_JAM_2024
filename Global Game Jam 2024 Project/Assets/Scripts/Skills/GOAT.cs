using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAT : Skill
{
    public float speed = 32f;
    public float aliveTime = 8f;
    public float offset = 20f;

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

            Shoot();

        }
        else
        {
            print("Its on CD: " + currentCD);
        }

    }



    void Shoot()
    {
        GameObject NOOT = Instantiate(Resources.Load("GOAT"), enemyPlayer.transform.position + enemyPlayer.gameObject.transform.up * offset, Quaternion.identity, skillStuff.transform) as GameObject;
        GOATFire NOOTFire = NOOT.GetComponent<GOATFire>();

        NOOTFire.enemyPlayer = enemyPlayer;
        NOOTFire.speed = speed;
        NOOTFire.aliveTime = aliveTime;
        NOOTFire.enabled = true;
    }
}
