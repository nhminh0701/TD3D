using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// Storing data to build turret
/// and managing these data
/// </summary>
public class BuildManager: MonoBehaviour
{
    // Storing Turret To build and Build themt
    public static BuildManager instance;

    // Target reference, i.e the turret prefab
    // and the position (node) to build the prefab
    // are determined here and other script can
    // manage these selections based on the available
    // public function here
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

    public TurretBlueprint GetTurretToBuild() { return turretToBuild; }
}
