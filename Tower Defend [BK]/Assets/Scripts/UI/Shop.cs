using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // TODO after finishing the game:
    // Create [turret], instantiate the turret type depend on
    // index int input

    BuildManager buildManager;

    public List<TurretClass> turrets;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectTurret(int index)
    {
        // Call from UI Element (Button) communicate with BuildManager
        // communicate with money, handle UI i.e open close shop
        buildManager.SelectTurretToBuild(turrets[index]);
    }
}
