using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // original y position of uiNotification
    float notificationOriginalY;
    bool drift;

    [Header("Notification Variables")]
    [SerializeField]
    private GameObject uiNotification;
    [SerializeField]
    private float uiNotificationSpeed;
    [SerializeField]
    private float topNotifHeight;
    private Text notifText;

    private Text scoreText;
    private GameObject restartButton;

    public void EOPointNotification(int points)
    {
        // if already drifting, restart
        if(drift)
        {
            StopCoroutine("FadeOut");
            uiNotification.transform.position = new Vector3(uiNotification.transform.position.x, notificationOriginalY, uiNotification.transform.position.z);
        }

        notifText.text = "+" + points;
        uiNotification.SetActive(true);
        drift = true;
        StartCoroutine("FadeOut");
    }

    private void Start()
    {
        notificationOriginalY = uiNotification.transform.position.y;
        notifText = uiNotification.GetComponent<Text>();

        // hopefully throwing these errors will help with debugging
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        if (scoreText == null)
            Debug.LogError("Player -> Start: Score text not found");

        restartButton = GameObject.FindGameObjectWithTag("RestartButton");
        if (restartButton == null)
            Debug.LogError("Player -> Start: Restart button not found");
        restartButton.SetActive(false);
    }

    private void Update()
    { 
        float deltaPos = uiNotification.transform.position.y - notificationOriginalY;
        if (drift)
        {
            if (deltaPos < notificationOriginalY + topNotifHeight)
            {
                uiNotification.transform.Translate(Vector3.up * uiNotificationSpeed);
            }
            // if risen all the way, reset position and stop drift
            else
            {
                drift = false;
                uiNotification.transform.position = new Vector3(uiNotification.transform.position.x, notificationOriginalY, uiNotification.transform.position.z);
                uiNotification.SetActive(false);
            }
        }
    }

    private IEnumerator FadeOut()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.01f)
        {
            Color c = notifText.color;
            c.a = ft;
            notifText.color = c;
            yield return null;
        }
    }

    public void GameOver(int score)
    {
        // enable restart game modal
        restartButton.SetActive(true);

        // set player back to original position to stop continual play after game over
        // ---- PROBLEM with this fix: if player dies early, will just respawn instantly and see it ----
        // Reset();

        // updating final score text
        Text finalScoreText = GameObject.FindGameObjectWithTag("FinalScoreText").GetComponent<Text>();
        if (finalScoreText == null)
        {
            Debug.LogError("Player -> GameOver: Final score text not found");
            return;
        }

        finalScoreText.text = "Final Score: " + score;
    }

    public void Reset()
    {
        // turn off modal
        restartButton.SetActive(false);
    }

    public void UpdateStats(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
