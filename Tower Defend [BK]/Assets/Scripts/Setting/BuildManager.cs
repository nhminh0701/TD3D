using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildManager: MonoBehaviour
{
    // Storing Turret To build and Build them
    // Any thing dealing with build turret
    public static BuildManager instance;

    TurretBlueprint turretToBuild;
    Node selectedNode;

    [SerializeField] List<GameObject> standardTurretPrefabs;
    
    public bool CanBuild { get { return turretToBuild != null; } }

    public bool HasEnoughMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    public NodeUI nodeUI;

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
        // Use for turret availability checking of the Node with Node component
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(turretToBuild.buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money left: " + PlayerStats.money);
    }


    public void SelecteNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    private void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    /// <summary>
    /// The function to be called in the Shop.cs.
    /// The Shop.cs handle buying the set turret in this script
    /// Also we reset the node selection with this function
    /// </summary>
    /// <param name="turret"></param>
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
}
