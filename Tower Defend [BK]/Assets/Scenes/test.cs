using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] Transform trackingObject;

    Quaternion originalRotation;

    private void Start()
    {
        originalRotation = trackingObject.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        trackingObject.rotation = originalRotation;
        Debug.Log(trackingObject.position);
    }
}
