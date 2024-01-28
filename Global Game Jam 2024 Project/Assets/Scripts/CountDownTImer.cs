using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDownTImer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign this in the inspector with your UI Text element
    [SerializeField] private float timeRemaining = 60; // 60 seconds countdown

    void Update()
    {
        if (timeRemaining > 0)
        {

            timeRemaining -= Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.CeilToInt(timeRemaining).ToString();
        }
        else
        {
            SceneManager.LoadScene("Phase_2");
        }
    }
}
