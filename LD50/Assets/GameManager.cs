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
    }

	public void StartPlaying() {
		gameState = GameState.Playing;
		UIMan.DisplayPlayUI();
	}


	public void GameOver() {
		gameState = GameState.GameOver;
		UIMan.DisplayGameOverUI();
	}


	public void QuitGame() {
		Application.Quit();
	}


	public void RestartGame() {
		SceneManager.LoadScene(0);
	}

   

    void Update() {
        
    }


}
