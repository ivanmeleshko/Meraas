using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPicker : MonoBehaviour
{

    Camera cam;
    Slider floorSlider;
    Transform floor4;


    private void Start()
    {
        cam = Camera.main;
        floorSlider = GameObject.Find("SliderFloor").GetComponent<Slider>();
        floor4 = GameObject.Find("Floor4").GetComponent<Transform>();
    }


    public void SelectObject()
    {
        ClosePopups();
        //RemoveRing();     

        if (Settings.instance.House != null)
        {
            Settings.instance.House.transform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(true);
        }

        Settings.instance.House = gameObject;

        if (Settings.instance.House.transform != null && !floor4.gameObject.active)
                            //Vector3.Distance(new Vector3(Settings.instance.House.transform.GetComponent<Renderer>().bounds.center.x,
                            //                               Settings.instance.House.transform.GetComponent<Renderer>().bounds.center.y + 20f,
                            //                               Settings.instance.House.transform.GetComponent<Renderer>().bounds.center.z),
                            //                               cam.transform.position) > 30f)
        {
            floorSlider.gameObject.SetActive(false);
            Settings.instance.objectSelected = true;
            Settings.instance.selectedVilla = new Flat();
            Settings.instance.selectedVilla = gameObject.GetComponent<Flat>();
            Settings.instance.orbitRotation = false;          
        }
    }


    private void ClosePopups()
    {
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
        foreach (GameObject p in popups)
        {
            p.SetActive(false);
        }
    }
}
