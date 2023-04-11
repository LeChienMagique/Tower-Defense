using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour {
	public GameObject ui;

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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu() {
		Debug.Log("Go to menu.");
	}
}