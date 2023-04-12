using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class WaveSpawner: MonoBehaviour {
	public static int EnemiesAlive = 0;

	public Wave[]    waves;
	public Transform spawnPoint;

	public  float timeBetweenWaves = 5f;
	private float countdown        = 2f;

	public TextMeshProUGUI waveCountdownText;

	public GameManager gameManager;

	private int waveIndex = 0;

	private void Update() {
		if (EnemiesAlive > 0) {
			return;
		}

		if (waveIndex >= waves.Length) {
			gameManager.WinLevel();
			this.enabled = false;
		}
		
		if (countdown <= 0f) {
			countdown = timeBetweenWaves;
			StartCoroutine(SpawnWave());
			return;
		}

		countdown -= Time.deltaTime;
		countdown =  Mathf.Clamp(countdown, 0, Mathf.Infinity);

		waveCountdownText.text = $"{countdown:00.00}";
	}

	private IEnumerator SpawnWave() {
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];
		EnemiesAlive = wave.count;

		for (int i = 0 ; i < wave.count ; i++) {
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}
		waveIndex++;
	}

	private void SpawnEnemy(GameObject enemy) {
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
}