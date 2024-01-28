using UnityEngine;
using System.Collections;

public class BingChilling : Skill
{
    public float slowDuration = 3f;
    public float slowFactor = 0.5f;

    private void Start()
    {
        SetValues();
    }

    public override void UseSkill()
    {
        if (currentCD <= 0)
        {
            StartCoroutine(ApplySlowToOpponent());
            StartCoroutine(StartCooldown());
        }
    }


    private IEnumerator ApplySlowToOpponent()
    {
        PlayerController enemyController = enemyPlayer.GetComponent<PlayerController>();
        if (enemyController != null)
        {
            enemyController.ModifySpeed(slowFactor);
            yield return new WaitForSeconds(slowDuration);
            enemyController.ModifySpeed(1 / slowFactor);
        }
        else
        {
            Debug.LogError("PlayerController component not found on the enemy player.");
        }
    }
}
