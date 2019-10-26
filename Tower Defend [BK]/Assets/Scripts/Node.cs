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

    //[HideInInspector]
    public GameObject turret;
    //[HideInInspector]
    public TurretClass turretBlueprintClass;
    //[HideInInspector]
    public TurretBlueprint turretBlueprint;
    //[HideInInspector][Tooltip("Is the level of the turret is maximized?")]
    public bool upgradable;
    // This lv can be setup to increase game's difficulty via playerpref or json
    //[HideInInspector]
    public int maxLv;


    Renderer rend;
    Color startColor;
    BuildManager buildManager;

    private void Start()
    {
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

    void BuildTurret(TurretClass turretClass)
    {
        upgradable = true;

        turretBlueprintClass = turretClass;

        turretBlueprint = turretClass.turretList[0];

        if (PlayerStats.money < turretBlueprint.cost)
        {
            Debug.Log("Not enoug money");
            return;
        }

        PlayerStats.SpendMoney(turretBlueprint.cost);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffect = (GameObject)Instantiate(turretBlueprint.buildEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(buildEffect, 5f);

        maxLv = turretBlueprintClass.turretList.Count;
    }

    public void UpgradeTurret()
    {
        if (!upgradable)
        {
            Debug.LogError("Maximized level or wrong design");
            return;
        }

        turretBlueprint = turretBlueprintClass.turretList[turretBlueprint.level + 1];
        if (turretBlueprint.level + 1 >= maxLv)
        {
            upgradable = false;
        }

        if (PlayerStats.money < turretBlueprint.cost)
        {
            Debug.Log("Not enoug money");
            return;
        }

        PlayerStats.SpendMoney(turretBlueprint.cost);

        // Destroy old turret
        Destroy(turret);

        // Building new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffect = (GameObject)Instantiate(turretBlueprint.prefab, GetBuildPosition(), Quaternion.identity);

        Destroy(buildEffect, 5f);
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
}
