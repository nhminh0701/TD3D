using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The Object relocate to the target Node position and switch on/off the world canvas
/// Attach to the UI element which display the turret option when the node containing
/// the turret is selected
/// </summary>
public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public GameObject upgradeButton;
    public Text upgradeCost;
    public Text price;

    Node target;



    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (_target.upgradable)
        {
            if (!upgradeButton.activeInHierarchy) { upgradeButton.SetActive(true); }
            upgradeCost.text = "UPGRADE\n$" + _target.turretBlueprintClass.turretList[_target.turretBlueprint.level].cost.ToString();
        }
        
        price.text = "SELL\n$" + _target.turretBlueprint.price.ToString();

        ui.SetActive(true);

        if (!_target.upgradable)
        {
            upgradeButton.SetActive(false);
        }
    }

    public void Hide()
    {
        if (!upgradeButton.activeInHierarchy) { upgradeButton.SetActive(true); }
        ui.SetActive(false);
    }

    public void UpgradeTurret()
    {
        if (target.reachMaxLV) upgradeButton.SetActive(false);
        target.UpgradeTurret();

        price.text = "SELL\n$" + target.turretBlueprint.price.ToString();
    }

    public void SellTurret()
    {
        target.SellTurret();
        Hide();
    }
}
