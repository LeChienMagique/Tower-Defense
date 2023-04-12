using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy: MonoBehaviour {
	public                   float startSpeed = 10;
	[HideInInspector] public float speed;

	public float      startHealth = 100;
	public float      health;
	public int        worth = 50;
	public GameObject deathEffect;

	[Header("Unity Stuff")] public Image healthBar;
	private                        bool  isDead = false;

	private void Start() {
		health = startHealth;
		speed  = startSpeed;
	}

	public void TakeDamage(float amount) {
		health               -= amount;
		healthBar.fillAmount =  health / startHealth;
		if (health <= 0 && !isDead) {
			Die();
		}
	}

	private void Die() {
		isDead = true;
		
		GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
		Destroy(effect, 5);

		WaveSpawner.EnemiesAlive--;

		PlayerStats.Money += worth;
		Destroy(gameObject);
	}

	public void Slow(float amount) {
		speed = startSpeed * (1 - amount);
	}
}