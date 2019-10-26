using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Object relocate to the target Node position and switch on/off the world canvas
/// Attach to the UI element which display the turret option when the node containing
/// the turret is selected
/// </summary>
public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public GameObject upgradeButton;
    Node target;

    public void SetTarget(Node _target)
    {
        if (!_target.upgradable)
        {
            upgradeButton.SetActive(false);
        }

        target = _target;

        transform.position = target.GetBuildPosition();

        ui.SetActive(true);
    }

    public void Hide()
    {
        if (!upgradeButton.activeInHierarchy) { upgradeButton.SetActive(true); }
        ui.SetActive(false);
    }

    public void UpgradeTurret()
    {
        target.UpgradeTurret();
    }
}
