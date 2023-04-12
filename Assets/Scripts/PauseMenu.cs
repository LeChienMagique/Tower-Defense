using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour {
	public GameObject ui;

	public string     menuSceneName = "MainMenu";
	public SceneFader sceneFader;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			Toggle();
		}
	}

	public void Toggle() {
		ui.SetActive(!ui.activeSelf);

		Time.timeScale = ui.activeSelf ? 0 : 1;
	}

	public void Retry() {
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu() {
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}
}