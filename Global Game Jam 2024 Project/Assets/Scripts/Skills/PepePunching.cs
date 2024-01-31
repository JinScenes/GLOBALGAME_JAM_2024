using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepePunching : Skill
{

    public float speed = 15f;
    public float aliveTime = 8f;

    private void Start()
    {
        SetValues();
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

        void Shoot()
        {
            GameObject NOOT = Instantiate(Resources.Load("PunchingPepeObj"), GameObject.Find("PepeTransform").transform, false) as GameObject;
            PepePunchingFire NOOTFire = NOOT.GetComponent<PepePunchingFire>();

            NOOTFire.enemyPlayer = enemyPlayer;
            NOOTFire.speed = speed;
            NOOTFire.aliveTime = aliveTime;
            NOOTFire.enabled = true;
        }


    }
}
