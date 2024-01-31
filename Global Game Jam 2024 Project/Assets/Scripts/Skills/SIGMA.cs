using UnityEngine;
using System.Collections;

public class SIGMA : Skill
{
    public float immunityDuration = 5f;

    private PlayerComba playerComba;

    private void Start()
    {
        SetValues();
        playerComba = GetComponentInParent<PlayerComba>();
    }

    public override void UseSkill()
    {
        if (playerComba != null)
        {
            if (currentCD <= 0)
            {
                StartCoroutine(PushImmunityRoutine());
                AudioManager.instance.PlayAudios("gigachad");

            }
           
        }
    }

    private IEnumerator PushImmunityRoutine()
    {
        playerComba.SetPushImmunity(true);
        yield return new WaitForSeconds(immunityDuration);
        playerComba.SetPushImmunity(false);
    }
}
