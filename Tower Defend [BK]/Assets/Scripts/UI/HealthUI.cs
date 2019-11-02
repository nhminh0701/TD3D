using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image currentBar;

    float maxBarValue;
    float changeValue;

    float currentFill;

    Quaternion cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = Camera.main.transform.rotation;
        currentFill = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation == cameraRotation) return;

        transform.rotation = cameraRotation;
    }

    public void AssignMaxBarVal(float value) { maxBarValue = value; }

    public void ChangeBarVal(float value)
    {
        currentFill += value / maxBarValue;

        if (currentFill < 0) currentFill = 0;

        currentBar.fillAmount = currentFill;
        
    }
}
