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
    TurretAsset thisTurretAsset;
    Node selectedNode;
    
    public bool CanBuild { get { return thisTurretAsset != null; } }

    NodeUI nodeUI;
    public bool hasEnoughMoney { get { return  PlayerStats.money >= thisTurretAsset.listTurretLV[0].inGamePurchasePrice; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        nodeUI = NodeUI.instance;
    }

    public void SelecteNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        thisTurretAsset = null;

        nodeUI.SetTarget(node);
    }

    private void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    /// <summary>
    /// Called from Shop.cs.
    /// The Shop.cs handle buying the set turret in this script
    /// Also we reset the node selection with this function
    /// </summary>
    /// <param name="turret"></param>
    public void SelectTurretToBuild(TurretAsset turret)
    {
        thisTurretAsset = turret;
        DeselectNode();
    }

    public TurretAsset GetTurretToBuild() { return thisTurretAsset; }
}
