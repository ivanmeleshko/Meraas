using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeCamera : MonoBehaviour
{
    float speed = 2.0f;
    //public Image imageCompas;
    Camera cam;
    static bool go;
    float damping = 5f;
    float distance = 60f;
    Image imageCompas;
    Slider floorSlider;
    Vector3 home = new Vector3(-44, 22, -24);
    public Transform HouseTransform;
    

    void Start()
    {
        cam = Camera.main;
        imageCompas = GameObject.Find("ImageCompas").GetComponent<Image>();
        floorSlider = GameObject.Find("SliderFloor").GetComponent<Slider>();
    }


    void Update()
    {
        if (go)
        { 
            if (distance > 1f)
            {
                Settings.instance.interactive = false;
                cam.transform.position = Vector3.Lerp(cam.transform.position, home, Time.deltaTime * speed);
                cam.transform.LookAt(HouseTransform.GetComponent<Renderer>().bounds.center);
                distance = Vector3.Distance(cam.transform.position, home);

                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);

                Settings.instance.House.transform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(true);
                //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * damping);
                GameObject.FindGameObjectWithTag("MPView").gameObject.SetActive(false);
            }
            else
            {
                floorSlider.gameObject.SetActive(true);
                go = false;
                distance = 60f;
                Settings.instance.interactive = true;
                Settings.instance.cameraOffset = home - HouseTransform.GetComponent<Renderer>().bounds.center;
            }
        }
    }


    public void Home()
    {
        go = true;
        CenterCamera.walk = false;
        Settings.instance.readyToOrbitRotate = false;
        Settings.instance.rotModeChanged = true;
        Settings.instance.orbitRotation = true;
    }
}
