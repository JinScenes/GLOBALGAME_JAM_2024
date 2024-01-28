using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Skill : MonoBehaviour
{
    public float skillCD = 2;
    public float currentCD = 0;

    public GameObject player;
    public GameObject enemyPlayer;
    public GameObject skillStuff;

    public float pushForce = 10f;
    public Vector3 pushDir = Vector3.forward;

    public abstract void UseSkill();

    public IEnumerator StartCooldown()
    {
        currentCD = skillCD;

        while (currentCD > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentCD-=0.1f;
            //print("TIck " + currentCD);
        }

    }

    public void SetValues()
    {

        player = gameObject;
        foreach (GameObject plr in GameObject.FindGameObjectsWithTag("Player"))
        {
            // If it's the other playaer
            if (gameObject != plr)
            {
                enemyPlayer = plr;
            }
        }

        skillStuff = GameObject.Find("SkillStuff");

    }

    protected void ApplyPush()
    {
        if (enemyPlayer != null)
        {
            Rigidbody enemyRb = enemyPlayer.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 force = pushDir.normalized * pushForce;
                enemyRb.AddForce(force, ForceMode.Impulse);
            }
        }
    }

}
