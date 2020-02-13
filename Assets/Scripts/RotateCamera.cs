﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RotateCamera : MonoBehaviour
{

    public Camera cam;
    public float speedH = 3.0f;
    public float speedV = 3.0f;
    public float damping = 5f;
    private float yaw = 30.0f;
    private float pitch = 10.0f;
    private bool rotationDisabled = true;
    private float minRotX = -89, maxRotX = 89f;
    private float xAngle = -111.0f;
    private float yAngle = 7.0f;
    private float xAngTemp = 0.0f;
    private float yAngTemp = 0.0f;
    private Vector3 firstpoint = new Vector3(0f, 1f, -10f);
    private Vector3 secondpoint = new Vector3(0f, 1f, -10f);




    void LateUpdate()
    {
        // Mouse rotation
        /*if (Input.GetMouseButtonDown(0)) rotationDisabled = false;
        if (Input.GetMouseButtonUp(0)) rotationDisabled = true;

        if (!rotationDisabled)
        {

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                xAngle -= speedH * Input.GetAxis("Mouse X");
                yAngle += speedV * Input.GetAxis("Mouse Y");
                yAngle = Mathf.Clamp(yAngle, -90f, 90f);
                cam.transform.eulerAngles = new Vector3(Mathf.LerpAngle(cam.transform.eulerAngles.x, yAngle, Time.deltaTime * damping),
                                                        Mathf.LerpAngle(cam.transform.eulerAngles.y, xAngle, Time.deltaTime * damping),
                                                        0.0f);
            }
        }*/

        if (!Settings.instance.objectSelected && !Settings.instance.readyToOrbitRotate
                && !Settings.instance.orbitRotation && Settings.instance.interactive)
        {
            //Check count touches
            if (Input.touchCount == 1)
            {

                //Touch began, save position
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //xAngle = transform.eulerAngles.y;
                    //yAngle = transform.eulerAngles.x;
                    firstpoint = Input.GetTouch(0).position;
                    xAngTemp = xAngle;
                    yAngTemp = yAngle;
                }
                //Move finger
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    secondpoint = Input.GetTouch(0).position;
                    xAngle = xAngTemp - (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                    yAngle = Mathf.Clamp(yAngTemp + (secondpoint.y - firstpoint.y) * 90.0f / Screen.height, minRotX, maxRotX);
                    //Rotate camera
                    //transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                    transform.eulerAngles = new Vector3(yAngle, xAngle, 0.0f);
                }
            }
        }
    }

}
