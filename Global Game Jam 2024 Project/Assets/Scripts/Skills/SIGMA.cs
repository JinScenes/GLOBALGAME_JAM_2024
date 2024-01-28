using UnityEngine;
using System.Collections;

public class SIGMA : Skill
{
    public float immunityDuration = 5f;

    private PlayerComba playerComba;

    private void Start()
    {
        playerComba = GetComponentInParent<PlayerComba>();
    }

    public override void UseSkill()
    {
        if (playerComba != null)
        {
            StartCoroutine(PushImmunityRoutine());
        }
    }

    private IEnumerator PushImmunityRoutine()
    {
        playerComba.SetPushImmunity(true);
        yield return new WaitForSeconds(immunityDuration);
        playerComba.SetPushImmunity(false);
    }
}
