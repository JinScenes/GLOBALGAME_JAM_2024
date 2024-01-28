using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iliketrains : Skill
{
    public float speed = 40f;
    public float aliveTime = 8f;
    public float offset = 20f;
    GameObject trainPlatform;

    private void Start()
    {
        SetValues();
        trainPlatform = GameObject.Find("TrainPlatform");
        skillCD = 20f;
    }
    public override void UseSkill()
    {
        if (currentCD <= 0)
        {
            StartCoroutine(StartCooldown());

            StartCoroutine(FireTrain());

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
        AudioManager.instance.PlayAudios("Train noise");

        SetPlatform(true);
        yield return new WaitForSeconds(4f);
        Shoot();
        AudioManager.instance.PlayAudios("I like trains");

        yield return new WaitForSeconds(5);
        SetPlatform(false);
    }

    void SetPlatform(bool setBool)
    {
        trainPlatform.GetComponent<MeshRenderer>().enabled = setBool;
        trainPlatform.GetComponent<BoxCollider>().enabled = setBool;
    }

    void Shoot()
    {
        GameObject NOOT = Instantiate(Resources.Load("sdfTrain"), GameObject.Find("TrainTransorm").transform, false) as GameObject;
        TrainFire NOOTFire = NOOT.GetComponent<TrainFire>();

        NOOTFire.enemyPlayer = enemyPlayer;
        NOOTFire.speed = speed;
        NOOTFire.aliveTime = aliveTime;
        NOOTFire.enabled = true;
    }
}
