using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitRotation : MonoBehaviour
{

    Camera cam;
    protected float RotationsSpeed = 2.0f;
    public float SmoothFactor = 0.5f;
    //protected Vector3 _cameraOffset;
    public Transform HouseTransform;
    public Image imageCompas;
    public bool RotateAroundPlayer;
    private Vector3 firstpoint = new Vector3(0f, 1f, -10f);
    private Vector3 secondpoint = new Vector3(0f, 1f, -10f);


    void Start()
    {
        cam = Camera.main;
        Settings.instance.cameraOffset = cam.transform.position - HouseTransform.position;
        cam.transform.LookAt(HouseTransform.position);
    }


    void Update()
    {
        Orbit();

        //if (Input.GetMouseButtonDown(0)) RotateAroundPlayer = true;
        //if (Input.GetMouseButtonUp(0)) RotateAroundPlayer = false;

        if (!Settings.IsPointerOverUIObject())
        {
            /*if (RotateAroundPlayer && Settings.instance.orbitRotation && Settings.instance.interactive)
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);

                float posY = cam.transform.position.y - Input.GetAxis("Mouse Y") * RotationsSpeed * 5f;
                Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                Vector3 newPos = HouseTransform.position + Settings.instance.cameraOffset;
                newPos.y = Mathf.Clamp(posY, 25f, 200f);

                cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
                cam.transform.LookAt(HouseTransform.position);

                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
            }*/

            if (Settings.instance.orbitRotation && Settings.instance.interactive)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        firstpoint = Input.GetTouch(0).position;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(0).position;

                        Quaternion camTurnAngle = Quaternion.AngleAxis((secondpoint.x - firstpoint.x) * RotationsSpeed / 150f, Vector3.up);

                        float posY = cam.transform.position.y - (secondpoint.y - firstpoint.y) * RotationsSpeed / 50f;
                        Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                        Vector3 newPos = HouseTransform.position + Settings.instance.cameraOffset;
                        newPos.y = Mathf.Clamp(posY, 25f, 200f);

                        cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
                        //cam.transform.position = newPos;// Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
                        cam.transform.LookAt(HouseTransform.position);
                        imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
                    }
                }
            }
        }
    }

    private void Orbit()
    {
        if (Settings.instance.orbitRotation && Settings.instance.interactive)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(200 * Time.deltaTime * RotationsSpeed / 500f, Vector3.up);

            float posY = cam.transform.position.y - Time.deltaTime * RotationsSpeed / 100f;
            Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

            Vector3 newPos = HouseTransform.position + Settings.instance.cameraOffset;
            newPos.y = Mathf.Clamp(posY, -50f, 300f);

            cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
            cam.transform.LookAt(HouseTransform.position);

            imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y + 135f);
        }
    }
}
