using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // Keep track contructs built on and user input
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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

    void BuildTurret(TurretBlueprint blueprintTurret)
    {
        if (PlayerStats.money < blueprintTurret.cost)
        {
            Debug.Log("Not enoug money");
            return;
        }

        PlayerStats.SpendMoney(blueprintTurret.cost);

        GameObject _turret = (GameObject)Instantiate(blueprintTurret.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffect = (GameObject)Instantiate(blueprintTurret.buildEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(buildEffect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enoug money");
            return;
        }

        PlayerStats.SpendMoney(turretBlueprint.upgradeCost);

        // Destroy old turret
        Destroy(turret);

        // Building new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffect = (GameObject)Instantiate(turretBlueprint.ugradedBuildEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(buildEffect, 5f);

        isUpgraded = true;
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
        
        if(buildManager.HasEnoughMoney)
        {
            rend.material.color = hoverColor;
        } else
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
