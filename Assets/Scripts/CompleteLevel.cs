using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel: MonoBehaviour {
	public SceneFader sceneFader;
	public string     menuSceneName = "MainMenu";

	public string nextLevel = "Level02";

	public void Continue() {
		sceneFader.FadeTo(nextLevel);
	}

	public void Menu() {
		sceneFader.FadeTo(menuSceneName);
	}
}