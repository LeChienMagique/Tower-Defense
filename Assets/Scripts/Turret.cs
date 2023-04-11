using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Search;
using UnityEngine;

public class Turret: MonoBehaviour {
	[CanBeNull] private Transform target;
	private             Enemy     targetEnemy;

	[Header("General")] public float range = 15f;

	[Header("Use Bullets (default)")] public GameObject bulletPrefab;
	public                                   float      fireRate = 1;
	private                                  float      fireCd   = 0;

	[Header("Use Laser")] public bool           useLaser       = false;
	public                       int            damageOverTime = 30;
	public                       float          slowAmount    = .5f;
	
	public                       LineRenderer   lineRenderer;
	public                       ParticleSystem impactEffect;
	public                       Light          impactLight;
	
	
	[Header("Unity Setup Fields")] public string    enemyTag = "Enemy";
	public                                Transform partToRotate;
	public                                float     turnSpeed = 10;
	public                                Transform firePoint;

	// Start is called before the first frame update
	void Start() {
		InvokeRepeating("UpdateTarget", 0, 0.5f);
	}

	private void UpdateTarget() {
		GameObject[] enemies          = GameObject.FindGameObjectsWithTag(enemyTag);
		float        shortestDistance = Mathf.Infinity;
		GameObject   nearest          = null;
		foreach (GameObject enemy in enemies) {
			float distance = Vector3.Distance(transform.position, enemy.transform.position);
			if (distance < shortestDistance) {
				shortestDistance = distance;
				nearest          = enemy;
			}
		}
		if (nearest is not null && shortestDistance <= range) {
			target      = nearest.transform;
			targetEnemy = target.GetComponent<Enemy>();
		}
		else {
			target = null;
		}
	}

	// Update is called once per frame
	void Update() {
		fireCd -= Time.deltaTime;
		if (target == null) {
			if (useLaser && lineRenderer.enabled) {
				lineRenderer.enabled = false;
				impactEffect.Stop();
				impactLight.enabled = false;
			}
			return;
		}

		try {
			Vector3 _ = target.position;
		}
		catch (MissingReferenceException e) {
			target = null;
			return;
		}

		LockOnTarget();

		if (useLaser) {
			Laser();
		}
		else if (fireCd <= 0) {
			Shoot();
			fireCd = 1 / fireRate;
		}
	}

	private void Laser() {
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);
		
		if (!lineRenderer.enabled) {
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}
		
		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;
		impactEffect.transform.position = target.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
		
		
	}

	private void LockOnTarget() {
		Vector3    dir      = target.position - transform.position;
		Quaternion lookRot  = Quaternion.LookRotation(dir);
		Vector3    rotation = Quaternion.Lerp(partToRotate.rotation, lookRot, turnSpeed * Time.deltaTime).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	private void Shoot() {
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet     bullet   = bulletGO.GetComponent<Bullet>();
		if (bullet != null) {
			bullet.Seek(target);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}