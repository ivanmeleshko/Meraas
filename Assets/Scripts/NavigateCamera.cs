using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigateCamera : MonoBehaviour
{

    public float Speed = 2.0f;
    private float timeLeft;
    private Camera cam;
    protected Transform HouseTransform;
    // protected Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    protected bool RotateAroundPlayer = false;
    protected float RotationsSpeed = 1.0f;
    public GameObject Popup;
    public float damping = 5f;
    private float boundsCenterY = -100f;
    private float downTime = 0.5f;
    //private float yaw = 0.0f;
    //private float degree = 0f;
    Image imageCompas;
    List<Light> pointLights;
    List<Light> spotLights;
    Slider sliderDayNight;
    Vector3 center, newPos;
    private GameObject buttonVillaName;
    private Vector3 firstpoint = new Vector3(0f, 1f, -10f);
    private Vector3 secondpoint = new Vector3(0f, 1f, -10f);


    void Start()
    {
        cam = Camera.main;
        timeLeft = 4 / Speed;

        if (HouseTransform != null)
        {
            Settings.instance.cameraOffset = cam.transform.position - HouseTransform.GetComponent<Renderer>().bounds.center;
        }

        Settings.instance.villas = new List<Flat>();
        GameObject[] gameObjs = GameObject.FindGameObjectsWithTag("Flat");

        foreach (GameObject o in gameObjs)
        {
            Settings.instance.villas.Add(o.GetComponent<Flat>());
        }

        buttonVillaName = GameObject.FindGameObjectWithTag("MPView");
        buttonVillaName.SetActive(false);

        imageCompas = GameObject.Find("ImageCompas").GetComponent<Image>();
    }


    void Update()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        //if (!Settings.IsPointerOverUIObject())
        if (Settings.instance.objectSelected)
        {
            HouseTransform = Settings.instance.House.transform;
            timeLeft -= Time.deltaTime;
            center = HouseTransform.GetComponent<Renderer>().bounds.center;

            if (timeLeft > 0)
            {
                Settings.instance.interactive = false;               
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(center.x + 8, center.y + 2f, center.z), Speed * Time.deltaTime);
                cam.transform.LookAt(center);
                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
            }
            else
            {
                Settings.instance.objectSelected = false;
                Settings.instance.interactive = true;
                timeLeft = 2f;
                Settings.instance.cameraOffset = cam.transform.position - center;
                boundsCenterY = center.y;// + 10f;
                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                Settings.instance.readyToOrbitRotate = true;
                Settings.instance.rotModeChanged = true;
                //SwitchLights();
                Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity);
                Popup.SetActive(true);

                GameObject.FindGameObjectWithTag("VillaInfo").GetComponent<Text>().text = Flat.VillaInfo(Settings.instance.selectedVilla);

                HouseTransform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(false);

                const string selectedColorHexAv = "#26B4B74B";
                const string selectedColorHexRes = "#62615F4B";
                const string selectedColorHexSold = "#B70F004B";
                string selectedColorHex = selectedColorHexAv;
                Color selectedColor;

                switch (Settings.instance.selectedVilla.availability)
                {
                    case Flat.Availability.Available:
                        selectedColorHex = selectedColorHexAv;
                        break;
                    case Flat.Availability.Reserved:
                        selectedColorHex = selectedColorHexRes;
                        break;
                    case Flat.Availability.Sold:
                        selectedColorHex = selectedColorHexSold;
                        break;
                }

                buttonVillaName.SetActive(true);

                if (ColorUtility.TryParseHtmlString(selectedColorHex, out selectedColor))
                {
                    buttonVillaName.GetComponent<Image>().color = selectedColor;
                }

                GameObject.FindGameObjectWithTag("VillaName").GetComponent<Text>().text = HouseTransform.name;
                GameObject buttonSend = GameObject.Find("ButtonSend");
                buttonSend.SetActive(false);
            }
        }
    }


    void LateUpdate()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        if (!Settings.IsPointerOverUIObject())
        {
            if (Settings.instance.readyToOrbitRotate && Settings.instance.interactive)
            {
                if (!Settings.instance.touchSupported)
                {
                    if (Input.GetMouseButtonDown(0)) RotateAroundPlayer = true;
                    if (Input.GetMouseButtonUp(0)) RotateAroundPlayer = false;

                    if (RotateAroundPlayer)
                    {
                        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
                        float posY = cam.transform.position.y - Input.GetAxis("Mouse Y") * RotationsSpeed * 0.2f;
                        Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                        newPos = HouseTransform.GetComponent<Renderer>().bounds.center + Settings.instance.cameraOffset;
                        newPos.y = Mathf.Clamp(posY, boundsCenterY + 3f, boundsCenterY + 8f);
                        cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);

                        if (LookAtPlayer || RotateAroundPlayer)
                        {
                            cam.transform.LookAt(HouseTransform.GetComponent<Renderer>().bounds.center);
                        }

                        imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                    }
                }
                else
                {
                    //if (RotateAroundPlayer)
                    if (Input.touchCount == 1)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            firstpoint = Input.GetTouch(0).position;
                        }
                        if (Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            secondpoint = Input.GetTouch(0).position;

                            Quaternion camTurnAngle = Quaternion.AngleAxis((secondpoint.x - firstpoint.x) * RotationsSpeed / 250f, Vector3.up);

                            float posY = cam.transform.position.y - (secondpoint.y - firstpoint.y) * RotationsSpeed * 0.002f;
                            Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                            Vector3 newPos = center + Settings.instance.cameraOffset;
                            //newPos.y = Mathf.Clamp(posY, -70f, -20f);
                            newPos.y = Mathf.Clamp(posY, boundsCenterY + 3f, boundsCenterY + 8f);

                            cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);// newPos;// 
                            cam.transform.LookAt(center);

                            imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                        }
                    }
                }
            }

            if (!Settings.instance.orbitRotation && Settings.instance.interactive)
            {
                if (Settings.instance.readyToOrbitRotate)
                {
                    if (Input.GetAxis("Mouse ScrollWheel") != 0)
                    {
                        float scrollDistance = Input.GetAxis("Mouse ScrollWheel") * 2;

                        if (Vector3.Distance(cam.transform.position, center) - scrollDistance > 5
                            && Vector3.Distance(cam.transform.position, center) - scrollDistance < 15)
                        {
                            cam.transform.position = Vector3.MoveTowards(cam.transform.position, center, scrollDistance);
                            newPos = cam.transform.position;
                            Settings.instance.cameraOffset = cam.transform.position - center;
                            cam.transform.LookAt(center);
                        }
                    }
                }

                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrePos - touchOnePrePos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                    float difference = (currentMagnitude - prevMagnitude) * SmoothFactor;

                    if (Vector3.Distance(cam.transform.position, center) - difference > 5
                      && Vector3.Distance(cam.transform.position, center) - difference < 15)
                    {
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, center, difference);
                        newPos = cam.transform.position;
                        Settings.instance.cameraOffset = cam.transform.position - center;
                    }
                }
            }
        }
    }

}