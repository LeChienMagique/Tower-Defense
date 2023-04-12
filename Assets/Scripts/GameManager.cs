using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {
	public static bool       GameIsOver;
	public        GameObject gameOverUI;
	public        GameObject completeLevelUI;

	private void Start() {
		GameIsOver = false;
	}

	void Update() {
		if (GameIsOver) {
			return;
		}

		if (PlayerStats.Lives <= 0) {
			EndGame();
		}
	}
	public int levelToUnlock = 2;

	public void WinLevel() {
		GameIsOver = true;
		PlayerPrefs.SetInt("levelReached", levelToUnlock);
		
		completeLevelUI.SetActive(true);
	}

	private void EndGame() {
		GameIsOver = true;
		gameOverUI.SetActive(true);
		Debug.Log("Game Over!");
	}
}