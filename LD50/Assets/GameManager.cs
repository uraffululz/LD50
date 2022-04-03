using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	UIManager UIMan;
 
	public enum GameState {Started, Playing, GameOver};
	public static GameState gameState;


    void Start() {
		UIMan = GetComponent<UIManager>();

        gameState = GameState.Started;
		Time.timeScale = 0f;
    }


	public void StartPlaying() {
		gameState = GameState.Playing;
		Time.timeScale = 1f;
		UIMan.DisplayPlayUI();
	}


	public void GameOver() {
		gameState = GameState.GameOver;
		
		UIMan.DisplayGameOverUI();

		Time.timeScale = 0f;
	}


	public void QuitGame() {
		print("Quitting game");
		Application.Quit();
	}


	public void RestartGame() {
		SceneManager.LoadScene(0);
	}

   

    void Update() {
		
    }


}
