using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iliketrains : Skill
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
            currentCD = 1;
            StartCoroutine(StartCooldown());

            StartCoroutine(FireTrain());
            Shoot();

        }
        else
        {
            print("Its on CD: " + currentCD);
        }

    }

    IEnumerator FireTrain()
    {
        // Warnin before train comes
        print("Waiting for train!");
        yield return new WaitForSeconds(0.1f);
        Shoot();
    }


    void Shoot()
    {
        GameObject NOOT = Instantiate(Resources.Load("sdfTrain"), 
            GameObject.Find("TrainTransorm").transform, false) as GameObject;
        NOOT.transform.position = Vector3.zero;
        NOOT.transform.rotation = Quaternion.Euler(0,0,0);
        TrainFire NOOTFire = NOOT.GetComponent<TrainFire>();

        print(NOOTFire);
        NOOTFire.enemyPlayer = enemyPlayer;
        NOOTFire.speed = speed;
        NOOTFire.aliveTime = aliveTime;
        NOOTFire.enabled = true;
    }
}
