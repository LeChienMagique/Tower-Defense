using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet: MonoBehaviour {
	private Transform  target;
	public  float      speed           = 70;
	public  float      explosionRadius = 0;
	public  int        damage          = 50;
	public  GameObject impactEffect;

	public void Seek(Transform _target) {
		target = _target;
	}

	void Update() {
		if (target is null) {
			Destroy(gameObject);
			return;
		}
		try {
			Vector3 _ = target.position;
		}
		catch (MissingReferenceException e) {
			target = null;
			return;
		}

		Vector3 dir           = target.position - transform.position;
		float   distThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame) {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distThisFrame, Space.World);
		transform.LookAt(target);
	}

	private void HitTarget() {
		GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effect, 5);

		if (explosionRadius > 0) {
			Explode();
		}

		Destroy(gameObject);
	}

	private void Explode() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders) {
			if (collider.CompareTag("Enemy")) {
				Damage(collider.transform);
			}
		}
	}

	private void Damage(Transform enemy) {
		Enemy e = enemy.GetComponent<Enemy>();
		if (e != null) {
			e.TakeDamage(damage);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}