using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Skill : MonoBehaviour
{
    public float skillCD = 2;
    public float currentCD = 0;

    public GameObject player;
    public GameObject enemyPlayer;

    public abstract void UseSkill();

    public IEnumerator StartCooldown()
    {
        currentCD = skillCD;

        while (currentCD > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentCD-=0.1f;
            print("TIck " + currentCD);
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
    }
}
