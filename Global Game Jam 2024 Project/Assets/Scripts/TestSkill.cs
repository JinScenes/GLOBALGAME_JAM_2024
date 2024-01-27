using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : Skill
{
    private void Start()
    {
        SetValues();
    }
    public override void UseSkill()
    {
        if(currentCD <= 0)
        {
            StartCoroutine(StartCooldown());
        }
        else
        {
            print("Its on CD: " + currentCD);
        }
       
    }

}
