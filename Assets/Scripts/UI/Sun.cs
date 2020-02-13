using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.PostProcessing;


public class Sun : MonoBehaviour
{

    Vector3 initPosition = new Vector3(-200, 600f, 200f);
    Vector3 initRotation = new Vector3(65f, 135f, 0f);
    Slider sunSlider;
    List<Light> pointLights;
    List<Light> spotLights;
    GameObject[] gameObjs;
    //    public PostProcessingProfile postProcProf;
    GameObject clouds;
    GameObject moon;


    void Start()
    {
        sunSlider = GameObject.Find("SliderDayNight").GetComponent<Slider>();
        //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("NightLight");
        //pointLights = new List<Light>();

        //foreach (GameObject o in gameObjects)
        //{
        //    pointLights.Add(o.GetComponent<Light>());
        //}

        //GameObject[] gObjs = GameObject.FindGameObjectsWithTag("HouseNightLight");
        //spotLights = new List<Light>();

        //foreach (GameObject o in gObjs)
        //{
        //    spotLights.Add(o.GetComponent<Light>());
        //}

        //gameObjs = GameObject.FindGameObjectsWithTag("WindowLight");
        //foreach (GameObject p in gameObjs)
        //{
        //    p.SetActive(false);
        //}

        //clouds = GameObject.Find("Clouds");

        //      var bloom = postProcProf.bloom.settings;
        //      postProcProf.bloom.enabled = false;

        //moon = GameObject.Find("Moon");
        //moon.SetActive(false);
    }


    private void Update()
    {
        //ChangeFogDoF();
    }


    public void OnValueChanged(float value)
    {
        transform.eulerAngles = initRotation;
        transform.position = initPosition;
        transform.RotateAround(initRotation, new Vector3(-1, 0, -1), value);
        transform.LookAt(initRotation);

        //if (Settings.instance.readyToOrbitRotate)
        //{
        //    RenderSettings.fogDensity = Mathf.Max(0.0012f, 0.004f * Mathf.Abs(value));
        //}
        //else
        //{
        //    RenderSettings.fogDensity = Mathf.Max(0.0006f, 0.002f * Mathf.Abs(value));
        //}


        if (value < -90f || value > 95f)
        {
            //moon.SetActive(true);

            //RenderSettings.fogColor = Color.black;


            //RenderSettings.sun.intensity = 0;
            //RenderSettings.ambientIntensity = 0;

            //var bloom = postProcProf.bloom.settings;
            //postProcProf.bloom.enabled = true;

            //foreach (Light l in pointLights)
            //{
            //    if (Settings.instance.House.transform != null && Vector3.Distance(
            //        l.transform.position, Settings.instance.House.transform.GetComponent<Renderer>().bounds.center) < 40f)
            //        l.intensity = 1;
            //}

            //foreach (Light l in spotLights)
            //{
            //    if (Settings.instance.House.transform != null &&
            //        Vector3.Distance(l.transform.position, Settings.instance.House.transform.GetComponent<Renderer>().bounds.center) < 40f)
            //        l.intensity = 0.5f;
            //}

            //foreach (GameObject p in gameObjs)
            //{
            //    //p.SetActive(true);
            //}

            //clouds.SetActive(false);
        }
        else
        {
            //RenderSettings.sun.intensity = 1f;


            //foreach (Light l in pointLights)
            //{
            //    l.intensity = 0;
            //}

            //foreach (Light l in spotLights)
            //{
            //    l.intensity = 0;
            //}

            //foreach (GameObject p in gameObjs)
            //{
            //    p.SetActive(false);
            //}

            //clouds.SetActive(true);

            RenderSettings.fogColor = new Color32(155, 186, 217, 255);
            RenderSettings.ambientIntensity = 1f;

            //var bloom = postProcProf.bloom.settings;
            //postProcProf.bloom.enabled = false;

            //moon.SetActive(false);
        }
    }


    private void ChangeFogDoF()
    {
        if (Settings.instance.rotModeChanged)
        {
            Settings.instance.rotModeChanged = false;
            //var DoF = postProcProf.depthOfField.settings;

            if (Settings.instance.readyToOrbitRotate)
            {
                //DoF.focalLength = 70f;
                RenderSettings.fogDensity = Mathf.Max(0.0012f, 0.004f * Mathf.Abs(sunSlider.value));
            }
            else
            {
                //DoF.focalLength = 30f;
                RenderSettings.fogDensity = Mathf.Max(0.0006f, 0.002f * Mathf.Abs(sunSlider.value));
            }

            //postProcProf.depthOfField.settings = DoF;
        }
    }
}