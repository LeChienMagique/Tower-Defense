using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {
	public float startSpeed = 10;
	[HideInInspector]
	public float speed;

	public  float        health = 100;
	public  int        worth  = 50;
	public  GameObject deathEffect;

	private void Start() {
		speed = startSpeed;
	}

	public void TakeDamage(float amount) {
		health -= amount;
		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
		Destroy(effect, 5);
		
		PlayerStats.Money += worth;
		Destroy(gameObject);
	}

	public void Slow(float amount) {
		speed = startSpeed * (1 - amount);
	}

}