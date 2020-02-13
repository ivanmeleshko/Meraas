using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterCamera : MonoBehaviour
{

    public float Speed = 2.0f;
    public Image imageCompas;
    public Transform TransformLookAt, TransformLookAtAdd;
    Camera cam;
    static bool go;
    static bool gotDistance;
    float timeLeft;
    float damping = 5f;
    float distance = 60f;
    Vector3 center = new Vector3(230, -3.5f, -70);
    Vector3 villa99 = new Vector3(140, -4.5f, -94);
    public static bool walk = false;

    void Start()
    {
        cam = Camera.main;
        timeLeft = 4 / Speed;
    }


    void Update()
    {
        if (go)
        {
            //timeLeft -= Time.deltaTime;

            //if (timeLeft > 0)
            //{
            if (distance > 1)
            {
                Settings.instance.interactive = false;

                cam.transform.position = Vector3.Lerp(cam.transform.position, center, Time.deltaTime);
                cam.transform.LookAt(TransformLookAt);
                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);

                //if (!gotDistance)
                //{
                distance = Vector3.Distance(cam.transform.position, center);
                gotDistance = true;
                //}

                Settings.instance.House.transform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MPView").gameObject.SetActive(false);
                //distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
                //distance = Mathf.Clamp(distance, minFov, maxFov);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 70, Time.deltaTime * damping);

                //}
            }
            else
            {
                Settings.instance.interactive = true;
                go = false;
                distance = 100;
                walk = true;
            }
            Settings.instance.interactive = true;
            imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
        }
    }


    //void LateUpdate()
    //{
    //    if (walk)
    //    {
    //        if (distance > 5)
    //        {
    //            //Settings.instance.interactive = false;

    //            float coef = Mathf.Max(Mathf.Sqrt(distance) + 5f);
    //            cam.transform.position = Vector3.Lerp(cam.transform.position, villa99, Time.deltaTime / coef );
    //            //cam.transform.LookAt(TransformLookAtAdd);
    //            imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);

    //            //if (!gotDistance)
    //            //{
    //            distance = Vector3.Distance(cam.transform.position, villa99);
    //            gotDistance = true;
    //            //}

    //            Settings.instance.House.transform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(true);
    //            GameObject.FindGameObjectWithTag("MPView").gameObject.SetActive(false);
    //            //distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
    //            //distance = Mathf.Clamp(distance, minFov, maxFov);
    //            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 70, Time.deltaTime * damping);

    //            //}
    //        }
    //        else
    //        {
    //            //Settings.instance.interactive = true;
    //            walk = false;
    //            distance = 100;
    //        }

    //        imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
    //    }
    //}





    public void GoToCenter()
    {
        go = true;
        gotDistance = false;
        Settings.instance.readyToOrbitRotate = false;
        Settings.instance.rotModeChanged = true;
        Settings.instance.orbitRotation = false;
    }

}
