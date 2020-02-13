using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{

    Camera cam;
    Vector3 dragOrigin;
    public float dragSpeed = 10f;
    public Image imageCompas;
    private bool go;
    private bool gotDistance;
    private float pinDistance = 100;
    public List<GameObject> pins;
    public static Vector3 camRotation;
    
    


    void Start()
    {
        cam = Camera.main;
    }


    public void Go()
    {
        //if (Input.touchCount == 1)
        //{      
            go = true;
            Settings.instance.readyToOrbitRotate = false;
            Settings.instance.orbitRotation = false;
        //}
    }


    public void OnMouseDown()
    {
        go = true;
        Settings.instance.readyToOrbitRotate = false;
        Settings.instance.orbitRotation = false;
    }


    public void OnMouseUp()
    {
        //go = true;
        //Settings.instance.readyToOrbitRotate = false;
        //Settings.instance.orbitRotation = false;
    }


    void LateUpdate()
    {
        if (!Settings.IsPointerOverUIObject())
        {
            if (go)
            {
                if (pinDistance > 2)
                {
                    Settings.instance.interactive = false;
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, transform.position, 1);// Vector3.Lerp(cam.transform.position, transform.position, Time.deltaTime);
                    //cam.transform.LookAt(TransformLookAt);                   
                    pinDistance = Vector3.Distance(cam.transform.position, new Vector3 (transform.position.x, transform.position.y + 1.5f, transform.position.z));
                    imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
                }
                else
                {
                    foreach (GameObject pin in pins)
                    {
                        pin.SetActive(true);
                    }
                    transform.gameObject.SetActive(false);
                    
                    Settings.instance.interactive = true;
                    pinDistance = 100;
                    go = false;
                    camRotation = cam.transform.eulerAngles;
                }

                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
            }
        }
    }

}
