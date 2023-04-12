using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader: MonoBehaviour {
	public Image          img;
	public AnimationCurve fadeCurve;

	private void Start() {
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene) {
		StartCoroutine(FadeOut(scene));
	}

	private IEnumerator FadeIn() {
		float t = 1f;

		while (t > 0) {
			t -= Time.deltaTime;
			float a = fadeCurve.Evaluate(t);
			img.color = new Color(0, 0, 0, a);
			yield return 0;
		}
	}

	private IEnumerator FadeOut(string scene) {
		float t = 0f;

		while (t < 1) {
			t += Time.deltaTime;
			float a = fadeCurve.Evaluate(t);
			img.color = new Color(0, 0, 0, a);
			yield return 0;
		}

		SceneManager.LoadScene(scene);
	}
}