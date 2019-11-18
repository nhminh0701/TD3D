using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The Object relocate to the target Node position and switch on/off the world canvas
/// Attach to the UI element which display the turret option when the node containing
/// the turret is selected
/// </summary>
public class NodeUI : MonoBehaviour
{
    public static NodeUI instance;

    public GameObject ui;
    public GameObject upgradeButton;
    public Text upgradeCost;
    public Text price;
    
    [SerializeField] Transform targetTransform;
    Vector3 targetPos;
    Vector3 defaultPos;

    Node target;

    private void Awake()
    {
        instance = this;
        defaultPos = ui.transform.position;
        targetPos = targetTransform.position;
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        

        if (_target.upgradable)
        {
            if (!upgradeButton.activeInHierarchy) { upgradeButton.SetActive(true); }
            upgradeCost.text = "UPGRADE\n$" + _target.turretBlueprintClass.turretList[_target.turretBlueprint.level].cost.ToString();
        }
        
        price.text = "SELL\n$" + _target.turretBlueprint.price.ToString();

        if (!_target.upgradable)
        {
            upgradeButton.SetActive(false);
        }
        ui.transform.DOMove(targetPos, 0.5f).SetEase(Ease.OutElastic);
    }

    public void Hide()
    {
        if (!upgradeButton.activeInHierarchy) { upgradeButton.SetActive(true); }
        if (Mathf.Abs((transform.position - defaultPos).magnitude) < 0.1) return; 
        ui.transform.DOMove(defaultPos, 0.5f).SetEase(Ease.OutElastic);
    }

    public void UpgradeTurret()
    {
        if (target.reachMaxLV) upgradeButton.SetActive(false);
        target.UpgradeTurret();

        price.text = "SELL\n$" + target.turretBlueprint.price.ToString();
    }

    public void SellTurret()
    {
        target.SellTurret();
        Hide();
    }
}
