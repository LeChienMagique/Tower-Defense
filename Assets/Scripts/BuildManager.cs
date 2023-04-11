using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager: MonoBehaviour {
	public static BuildManager instance;

	private void Awake() {
		if (instance is not null) {
			Debug.LogError("More than one BuilManager instance in scene!");
			return;
		}
		instance = this;
	}

	public GameObject standardTurretPrefab;
	public GameObject missileLauncherPrefab;
	public GameObject laserBeamerPrefab;

	public GameObject buildEffect;
	
	private TurretBlueprint turretToBuild;

	public bool CanBuild => turretToBuild != null;

	public bool HasMoney => PlayerStats.Money >= turretToBuild.cost;

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
	}

	public void BuildTurretOn(Node node) {
		if (PlayerStats.Money < turretToBuild.cost) {
			Debug.Log("Not enough money to build this turret!");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;
		
		GameObject turret = Instantiate(turretToBuild.prefab,
		                                node.GetBuildPosition(),
		                                Quaternion.identity);
		node.turret = turret;

		GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
		
		Debug.Log($"Turret built! Money left: ${PlayerStats.Money}");
	}
}