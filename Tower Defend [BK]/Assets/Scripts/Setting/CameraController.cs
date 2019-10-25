using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] float panSpeed = 30;
    [SerializeField] float scrollSpeed = 5f;
    [SerializeField] [Range(0, 1)] float pandBorderThickness = 0.01f;

    [SerializeField] float minValue = 15f;
    [SerializeField] float maxValue = 138f;

    [Tooltip("For android and IOS")]
    [SerializeField] Vector3 touchStart;
    bool doMovement = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //StandardloneControl();
        if (GameManager.gameIsEnded)
        {
            this.enabled = false;
            return;
        }
        SmartPhoneControl();
    }

    private void SmartPhoneControl()
    {
        // Ref https://www.youtube.com/watch?v=0G4vcH9N0gc
        MoveCameraOnSP();
        ZoomCameraOnSP();
    }

    private void ZoomCameraOnSP()
    {
        if (Input.touchCount == 2)
        {

            Touch touch_0 = Input.GetTouch(0);
            Vector3 touch_0prevPos = touch_0.position - touch_0.deltaPosition;
            Touch touch_1 = Input.GetTouch(1);
            Vector3 touch_1prevPos = touch_1.position - touch_1.deltaPosition;

            float currDistance = (touch_1.position - touch_0.position).magnitude;
            float prevDistance = (touch_1prevPos - touch_0prevPos).magnitude;

            float change = currDistance - prevDistance;

            Vector3 pos = transform.position;
            pos.y -= Math.Sign(currDistance - prevDistance) * Time.deltaTime * scrollSpeed;
            pos.y = Mathf.Clamp(pos.y, minValue, maxValue);

            transform.position = pos;
        }
    }

    private void MoveCameraOnSP()
    {

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = Input.mousePosition - touchStart;

                Vector3 moveVector = new Vector3(direction.x, 0, direction.y);

                if (moveVector.magnitude > 5f)
                    transform.Translate(moveVector.normalized * Time.deltaTime * panSpeed, Space.World);
            }

    }

    private void StandardloneControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement) { return; }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - pandBorderThickness))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= Screen.height * (pandBorderThickness))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= Screen.width * (pandBorderThickness))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width * (1 - pandBorderThickness))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * Time.deltaTime * 100;
        pos.y = Mathf.Clamp(pos.y, minValue, maxValue);
        transform.position = pos;
    }
}
