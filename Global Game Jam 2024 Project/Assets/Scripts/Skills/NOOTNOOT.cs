using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NOOTNOOT : Skill
{
    public float offset = 10;
    public float speed = 32f;
    public float aliveTime = 8f;
    public float timeInBetweenNOOTS = .5f;

    public Image imageToFade;
    public float fadeSpeed = 0.5f;


    private void Start()
    {
        imageToFade = GameObject.Find("NOOTIMAGAE").GetComponent<Image>();
        SetValues();
        //skillCD = 8f;
    }
    public override void UseSkill()
    {
        if (currentCD <= 0)
        {
            StartCoroutine(StartCooldown());
            StartFade();
            StartCoroutine(FireNOOTS());
           
        }
        else
        {
            print("Its on CD: " + currentCD);
        }

    }
    
    IEnumerator FireNOOTS()
    {
        ShootNOOT();
        yield return new WaitForSeconds(timeInBetweenNOOTS);
        ShootNOOT();
    }

    void ShootNOOT() 
    {
        AudioManager.instance.PlayAudios("NOOT");
        GameObject NOOT = Instantiate(Resources.Load("NOOT"), player.transform.position + player.gameObject.transform.up * offset, Quaternion.identity, skillStuff.transform) as GameObject;
        NOOT.transform.LookAt(enemyPlayer.transform.position);
        NOOTFIRE NOOTFire = NOOT.GetComponent<NOOTFIRE>();

        NOOTFire.enemyPlayer = enemyPlayer;
        NOOTFire.speed = speed;
        NOOTFire.aliveTime = aliveTime;
        NOOTFire.enabled = true;
    }

    private void StartFade()
    {
        // Reset the image alpha to zero before fading in
        Color startColor = imageToFade.color;
        startColor.a = 0f;
        imageToFade.color = startColor;

        // Start the fading coroutine
        StartCoroutine(Fade());
    }

    // Coroutine to slowly make the image appear and disappear
    private IEnumerator Fade()
    {
        while (true)
        {
            // Fade in
            while (imageToFade.color.a < 1f)
            {
                Color currentColor = imageToFade.color;
                currentColor.a += fadeSpeed * Time.deltaTime;
                imageToFade.color = currentColor;
                yield return null;
            }

            // Wait for a short duration at full alpha
            yield return new WaitForSeconds(1.0f);

            // Fade out
            while (imageToFade.color.a > 0f)
            {
                Color currentColor = imageToFade.color;
                currentColor.a -= fadeSpeed * Time.deltaTime;
                imageToFade.color = currentColor;
                yield return null;
            }

            // Wait for a short duration at zero alpha
            yield return new WaitForSeconds(1.0f);
            print("Reaached end");
            break;
        }
    }
}
