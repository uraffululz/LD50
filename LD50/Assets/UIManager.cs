using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameManager gameMan;
	Player_Flooring playerFloorScript;

	[SerializeField] GameObject GameStartBG;
	[SerializeField] GameObject GameOverBG;

	[SerializeField] GameObject timerBG;
	[SerializeField] Text timerText;
	public float gameTotalTime {get; private set;}

	[SerializeField] Text gameStatText;


    void Start() {
		gameMan = GetComponent<GameManager>();
		playerFloorScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Flooring>();

        DisplayGameStartUI();
    }

   

    void Update() {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			gameTotalTime += Time.deltaTime;
			DisplayTimerText();
		}
    }


	void DisplayGameStartUI() {
		GameStartBG.SetActive(true);
	}


	public void DisplayPlayUI() {
		GameStartBG.SetActive(false);
		GameOverBG.SetActive(false);

		timerBG.SetActive(true);
	}


	void DisplayTimerText() {
		timerText.text = ((int)gameTotalTime).ToString();
	}


	public void DisplayGameOverUI() {
		int endGameTime = (int)gameTotalTime;
		int floorsPlaced = playerFloorScript.totalFloorsPlaced;

		gameStatText.text = ("Total Time Survived: " + endGameTime + " || Total Floors Placed: " + floorsPlaced + " \n " +
			"JUDGE YOURSELF, AND ENJOY THE FALL");

		timerBG.SetActive(false);

		GameOverBG.SetActive(true);
	}

}
