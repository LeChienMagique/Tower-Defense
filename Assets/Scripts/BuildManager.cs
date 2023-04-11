using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager: MonoBehaviour {
	public static BuildManager instance;
	public        NodeUI       nodeUI;

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
	public GameObject sellEffect;

	private TurretBlueprint turretToBuild;
	private Node            selectedNode;

	public bool CanBuild => turretToBuild != null;

	public bool HasMoney => PlayerStats.Money >= turretToBuild.cost;

	public void SelectNode(Node node) {
		if (selectedNode == node) {
			DeselectNode();
			return;
		}
		
		selectedNode  = node;
		turretToBuild = null;

		nodeUI.SetTarget(node);
	}

	public void DeselectNode() {
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
		selectedNode  = null;

		DeselectNode();
	}

	public TurretBlueprint GetTurretToBuild() {
		return turretToBuild;
	}
}