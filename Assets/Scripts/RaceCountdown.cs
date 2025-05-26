using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceCountdown : MonoBehaviour
{
    public List<Sprite> countdownNumbers;

    public AudioSource countdownAudio;

    public Sprite goText;

    public GameObject player;

    public int startCountingFrom = 3;

    // Delay so that the Countdown appears slower
    public float countdownDelay = 1f;

    public GameObject countdownImage;

    public void StartRaceCountdown()
    {
        // Set the Image for Countdown to be active
        countdownImage.SetActive(true);

        // Hide the Timer Text when Countdown Starts
        GetComponent<TimeTrialTimer>().getTimerText().gameObject.SetActive(false);

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        // Disable the Player Movement
        player.GetComponent<KartController>().enabled = false;

        int counter = startCountingFrom;
        countdownAudio.Play();
        
        while (counter > 0)
        {
            countdownImage.GetComponent<Image>().sprite = countdownNumbers[counter - 1];

            yield return new WaitForSeconds(countdownDelay);
            counter--;
        }

        // Display Go
        countdownImage.GetComponent<RectTransform>().localScale = new Vector3(1.92490005f, 1, 1);

        countdownImage.GetComponent<Image>().sprite = goText;

        yield return new WaitForSeconds(countdownDelay);

        // Reset Scaling after Go
        countdownImage.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);

        // Reactivate the Player Movement
        player.GetComponent<KartController>().enabled = true;

        countdownImage.SetActive(false);
        
        // Display Timer Countdown back
        GetComponent<TimeTrialTimer>().getTimerText().gameObject.SetActive(true);

        GetComponent<TimeTrialTimer>().StartTimer();
    }
}