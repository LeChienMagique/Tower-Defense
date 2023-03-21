using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner: MonoBehaviour {
	public Transform enemyPrefab;
	public Transform spawnPoint;

	public  float timeBetweenWaves = 5f;
	private float countdown        = 2f;

	public TextMeshProUGUI waveCountdownText;
	
	private int waveIndex = 0;

	private void Update() {
		if (countdown <= 0f) {
			countdown = timeBetweenWaves;
			StartCoroutine(SpawnWave());
		}

		countdown              -= Time.deltaTime;
		
		waveCountdownText.text = Math.Round(countdown).ToString();
	}

	private IEnumerator SpawnWave() {
		waveIndex++;
		for (int i = 0 ; i < waveIndex ; i++) {
			SpawnEnemy();
			yield return new WaitForSeconds(.5f);
		}
	}

	private void SpawnEnemy() {
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}