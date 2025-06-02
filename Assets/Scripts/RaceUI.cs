using UnityEngine;
using UnityEngine.UI;

public class RaceUI : MonoBehaviour
{

    public GameObject player;
    public GameObject finishLine;

    // Category Gameobject
    public GameObject raceUI;

    public GameObject exploreUI;

    public GameObject startRaceButton;

    public void StartRace()
    {
        Debug.Log("RaceUI: Race has started");

        exploreUI.SetActive(false);
        startRaceButton.SetActive(false);

        // Get the Player to start behind the Starting Line
        player.transform.position = finishLine.transform.position - new Vector3(20, 0f, 0);

        player.transform.position = new Vector3(player.transform.position.x, 1f, player.transform.position.z);


        // Get the Player to face towards the Finish Line
        player.transform.LookAt(finishLine.transform);

        player.transform.rotation = Quaternion.Euler(0f, -160f, 0f);

        // Start Race Countdown
        GetComponent<RaceCountdown>().StartRaceCountdown();

        raceUI.SetActive(true);
    }

    public void EndRace()
    {
        Debug.Log("RaceUI: Race has ended!");

        raceUI.gameObject.SetActive(false);

        // Display Exploratory UI
        exploreUI.SetActive(true);

        startRaceButton.SetActive(true);
    }
}