using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// TurretClass: core data storage of current turret (GO)
/// current blueprint (for cost/level) 
/// list of higher blueprint (for update)
/// </summary>
public class Node : MonoBehaviour
{
    // Keep track contructs built on and user input
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretData currentTurretClass;
    //[HideInInspector]
    public TurretDataPerLV currentTurretLv;
    //[HideInInspector][Tooltip("Is the level of the turret is maximized?")]
    public bool upgradable;
    //[HideInInspector]
    public bool reachMaxLV = false;
    // This lv can be setup to increase game's difficulty via playerpref or json
    //[HideInInspector]
    public int maxLv;
    public int currentLv;

    ResourceDataAsset resourceDataAssetController;
    TurretResourceAsset currentTurretResource;
    Renderer rend;
    Color startColor;
    BuildManager buildManager;

    private void Start()
    {
        resourceDataAssetController = DataGlobal.instance.resourceDataAsset;
        // reachMaxLV = turretBlueprint.level == maxLv - 1;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // A turret already built in
        if (turret != null)
        {
            buildManager.SelecteNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        // Build a turret
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretData turretAsset)
    {
        currentTurretClass = turretAsset;

        currentTurretLv = turretAsset.listTurretLV[0];
        currentLv = 1;

        if (PlayerStats.money < currentTurretLv.inGamePurchasePrice)
        {
            Debug.Log("Not enoug money");
            return;
        }

        GetTurretResources(turretAsset.itemName);

        EventManager.ChangePlayerInStageMoney(-currentTurretLv.inGamePurchasePrice);

        InstantiateNewTurret();

        maxLv = currentTurretClass.unlockStatusCode;

        upgradable = !(currentLv == maxLv);
        reachMaxLV = currentLv == maxLv - 1;
    }

    private void GetTurretResources(string turretId)
    {
        List<TurretResourceAsset> listTurretResources = resourceDataAssetController.listTurretResourceAsset;
        foreach(TurretResourceAsset turretResource in listTurretResources)
        {
            if (turretResource.itemName == turretId)
            {
                currentTurretResource = turretResource;
                return;
            }
        }
    }

    public void UpgradeTurret()
    {
        if (!upgradable)
        {
            Debug.LogError("Maximized level or wrong design");
            return;
        }

        currentTurretLv = currentTurretClass.listTurretLV[currentLv];
        currentLv++;

        if (currentLv == maxLv - 1) {
            Debug.Log("Max Cmm r");
            reachMaxLV = true; }
        if (currentLv == maxLv)
        {
            upgradable = false;
        }

        if (PlayerStats.money < currentTurretLv.inGamePurchasePrice)
        {
            Debug.Log("Not enoug money");
            return;
        }

        EventManager.ChangePlayerInStageMoney(-currentTurretLv.inGamePurchasePrice);

        // Destroy old turret
        //Destroy(turret);
        SimplePool.Despawn(turret);

        // Building new turret
        InstantiateNewTurret();
    }

    public void SellTurret()
    {
        EventManager.ChangePlayerInStageMoney(currentTurretLv.sellPrice);

        // Reset turret data
        SimplePool.Despawn(turret);
        currentTurretLv = null;
        currentTurretClass = null;
        currentTurretResource = null;
        turret = null;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // is the pointer with the given id over en EventSystem.object?
            // if the mouse was clicked over a UI element
            return;
        }

        if (!buildManager.CanBuild) return;

        if (buildManager.hasEnoughMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }


    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void InstantiateNewTurret()
    {
        GameObject _turret = (GameObject)SimplePool.Spawn(currentTurretResource.listTurretPrefabs[currentLv - 1], GetBuildPosition(), Quaternion.identity);

        _turret.GetComponent<Turret>().turretAttackStyle = currentTurretClass.turretStyle;

        TurretAttack turretAttack = _turret.GetComponent<TurretAttack>();
        if (turretAttack != null)
        {
            turretAttack.attackParams = currentTurretLv.attackParams;
            turretAttack.InitiateTurret();
        }

        TurretMovement turretMovement = _turret.GetComponent<TurretMovement>();
        if (turretMovement != null) turretMovement.range = currentTurretLv.range;

        turret = _turret;

        GameObject buildEffect = (GameObject)Instantiate(currentTurretResource.listTurretBuildEffect[currentLv - 1], GetBuildPosition(), Quaternion.identity);

        Destroy(buildEffect, 3f);
    }
}
