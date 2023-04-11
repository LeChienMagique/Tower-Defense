using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {
	public GameObject ui;
	
	private Node target;

	public TextMeshProUGUI upgradeCost;
	public Button          upgradeButton;

	public TextMeshProUGUI sellCost;
	
	public void SetTarget(Node _target) {
		target             = _target;
		transform.position = target.GetBuildPosition();

		if (!target.isUpgraded) {
			upgradeCost.text           = "$" + target.turretBlueprint.upgradeCost;
			upgradeButton.interactable = true;
		} else {
			upgradeButton.interactable = false;
			upgradeCost.text = "DONE";
		}
		
		sellCost.text = "$" + target.turretBlueprint.GetSellAmount();
		ui.SetActive(true);
	}

	public void Hide() {
		ui.SetActive(false);
	}

	public void Upgrade() {
		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
	}

	public void Sell() {
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}
}
