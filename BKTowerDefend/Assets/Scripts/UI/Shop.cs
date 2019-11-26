using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    // Or selected turret by player
    public List<TurretData> usingTurretAssets;

    private void Start()
    {
        usingTurretAssets = DataGlobal.instance.dataAsset.GetAvailableTurrets();
        buildManager = BuildManager.instance;
    }

    public void SelectTurret(int index)
    {
        // Call from UI Element (Button) communicate with BuildManager
        // communicate with money, handle UI i.e open close shop
        buildManager.SelectTurretToBuild(usingTurretAssets[index]);
    }
}
