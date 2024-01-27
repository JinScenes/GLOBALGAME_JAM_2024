using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario64Painting : Skill
{
    GameObject painting;
    Mario64ColliderHandle Mario64ColliderHandle;

    private void Start()
    {
        SetValues();
        
        painting = Instantiate(Resources.Load("MarioPainting"), player.transform) as GameObject;
        painting.name = "Mario64Painting";
        Mario64ColliderHandle = painting.GetComponent<Mario64ColliderHandle>();
        Mario64ColliderHandle.enemyPlayer = enemyPlayer;

    }
    public override void UseSkill()
    {
        if (currentCD <= 0)
        {
            StartCoroutine(StartCooldown());

            painting.GetComponent<BoxCollider>().enabled = true;
            Mario64ColliderHandle.enabled = true;

            Invoke("StopSkill", 8f);

        }
        else
        {
            print("Its on CD: " + currentCD);
        }

    }

    void StopSkill()
    {
        painting.GetComponent<BoxCollider>().enabled = false;
        Mario64ColliderHandle.enabled = false;
    }

}
