using System.Collections;
using UnityEngine;

public class TrollFace : Skill
{
    public float effectDuration = 5f;
    public float effectIntensity = -1f;

    private PlayerController playerController;
    private bool isEffectActive = false;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public override void UseSkill()
    {
        if (!isEffectActive && playerController != null)
        {
            StartCoroutine(ControlInversionRoutine());
        }
    }

    private IEnumerator ControlInversionRoutine()
    {
        isEffectActive = true;

        playerController.InvertControls(effectIntensity);

        yield return new WaitForSeconds(effectDuration / 2f);

        playerController.InvertControls(-effectIntensity);

        yield return new WaitForSeconds(effectDuration / 2f);

        playerController.InvertControls(1f);
        isEffectActive = false;
    }
}
