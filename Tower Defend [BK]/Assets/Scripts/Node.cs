using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // Keep track contructs built on and user input
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

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
        buildManager.BuildTurretOn(this);
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
