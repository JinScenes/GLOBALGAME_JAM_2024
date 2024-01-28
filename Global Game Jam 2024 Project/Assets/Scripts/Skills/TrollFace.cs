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
        SetValues();
        playerController = GetComponentInParent<PlayerController>();
    }

    public override void UseSkill()
    {
        if (!isEffectActive && playerController != null)
        {
            if (currentCD <= 0)
            {
                StartCoroutine(StartCooldown());
                StartCoroutine(ControlInversionRoutine());
            }
        }
    }

    private IEnumerator ControlInversionRoutine()
    {
        isEffectActive = true;
        GameObject trollfaceGameOBJ = Instantiate(Resources.Load("TrollfaceIMAGE"), GameObject.Find("Troll Face Transform").transform, false) as GameObject;

        PlayerController enemy = enemyPlayer.GetComponent<PlayerController>();
        enemy.InvertControls(effectIntensity);

        yield return new WaitForSeconds(effectDuration / 2f);

        enemy.InvertControls(-effectIntensity);

        yield return new WaitForSeconds(effectDuration / 2f);
        
        enemy.InvertControls(effectIntensity);
        Destroy(trollfaceGameOBJ);
        isEffectActive = false;
    }
}
