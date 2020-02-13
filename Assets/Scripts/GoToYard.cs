using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToYard : MonoBehaviour
{
    public float Speed = 2f;
    //public Image imageCompas;
    Camera cam;
    static bool go;
    static bool gotDistance;
    float timeLeft;
    float damping = 5f;
    float distance = 10f;
    Vector3 yardCenter = new Vector3(41, -76, -202);


    void Start()
    {
        cam = Camera.main;
        timeLeft = 4 / Speed;
    }


    void Update()
    {
        //if (Input.GetMouseButtonDown(2))
        //{
        //    ToYard();
        //}

        if (go)
        {
            if (distance > 1f)
            {
                Settings.instance.interactive = false;
                cam.transform.localPosition = Vector3.Lerp(cam.transform.position, yardCenter, Time.deltaTime * Speed);
                cam.transform.LookAt(Settings.instance.House.GetComponent<Renderer>().bounds.center);
                distance = Vector3.Distance(cam.transform.position, yardCenter);
               
                //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 30, Time.deltaTime * damping);
            }
            else
            {
                go = false;
                distance = 10f;
                Settings.instance.interactive = true;
                //Settings.instance.cameraOffset = home - HouseTransform.position;
            }
        }
    }


    public void ToYard()
    {
        go = true;
        gotDistance = false;
        Settings.instance.readyToOrbitRotate = false;
        //Settings.instance.rotModeChanged = true;
        Settings.instance.orbitRotation = false;
        Settings.instance.yardNavigation = true;
    }

    
}
