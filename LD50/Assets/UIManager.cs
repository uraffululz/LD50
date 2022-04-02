using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameManager gameMan;

	[SerializeField] GameObject GameStartBG;
	[SerializeField] GameObject GameOverBG;

	[SerializeField] GameObject timerBG;
	[SerializeField] Text timerText;
	[SerializeField] float gameTotalTime;


    void Start() {
		gameMan = GetComponent<GameManager>();

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
		timerBG.SetActive(false);

		GameOverBG.SetActive(true);
	}

}
