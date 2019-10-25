using UnityEngine;
using System.Collections.Generic;

public class BuildManager: MonoBehaviour
{
    // Storing Turret To build and Build them
    // Any thing dealing with build turret
    public static BuildManager instance;

    TurretBlueprint turretToBuild;

    [SerializeField] List<GameObject> standardTurretPrefabs;
    
    public bool CanBuild { get { return turretToBuild != null; } }

    public bool HasEnoughMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("Not enoug money");
            return;
        }

        PlayerStats.SpendMoney(turretToBuild.cost);

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(turretToBuild.buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money left: " + PlayerStats.money);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
