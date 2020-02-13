using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiDrag : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    public GameObject Popup;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    Vector3 curPos = new Vector3();


    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
        Popup.transform.SetAsLastSibling();
    }


    public void OnDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);

        if (Input.mousePosition.x < 100 || Input.mousePosition.y < 100 
            || Input.mousePosition.x > Screen.width - 100 || Input.mousePosition.y > Screen.height - 100)
        {
            Popup.SetActive(false);
        }
    }


    public void OnMouseDown()
    {
        if (DoubleClick())
        {
            Maximize();
        }
        else
        {
            Popup.transform.SetAsLastSibling();
        }
    }


    private bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
        {
            clicked = 0;
        }
        return false;
    }


    public void ShowPopup()
    {
        Popup.SetActive(true);
        Popup.transform.SetAsLastSibling();
        if (Popup.name.Equals("PanelPopup"))
        {
            GameObject buttonSend = GameObject.Find("ButtonSend");
            buttonSend.SetActive(false);
        }
    }


    public void Maximize()
    {
        //if (Popup.name.Equals("PanelPhoto") || Popup.name.Equals("PanelFloorplan"))
        if (Popup.name.Equals("PanelFloorplan"))
        {
            GameObject maxButton = GameObject.Find("ButtonMax");          
            RectTransform panelRectTransform = Popup.GetComponent<RectTransform>();           

            if (panelRectTransform.sizeDelta.x < Screen.width)
            {
                curPos = Popup.transform.position;
                Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
                panelRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                if (Popup.name.Equals("PanelPhoto"))
                {
                    Image[] images = Popup.GetComponentsInChildren<Image>();
                    foreach (Image img in images)
                    {
                        if (img.tag.Equals("Photos"))
                        {
                            img.transform.SetPositionAndRotation(new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
                            RectTransform imageRectTransform = img.GetComponent<RectTransform>();
                            imageRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                        }
                    }
                }
               
                maxButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("compress");
            }
            else
            {
                switch (Popup.name)
                {
                    case "PanelFloorplan":
                        Popup.transform.SetPositionAndRotation(curPos, Quaternion.identity);
                        break;
                    case "PanelPhoto":
                        Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 3.8f, Screen.height / 3.5f, 0), Quaternion.identity);
                        break;
                    case "PanelVR360":
                        Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 1.35f, Screen.height / 1.4f, 0), Quaternion.identity);
                        break;
                    case "PanelStreetView":
                        Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 3.8f, Screen.height / 1.4f, 0), Quaternion.identity);
                        break;
                }
                panelRectTransform.sizeDelta = new Vector2(1000, 600);
                maxButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("maximize");
            }
        }
    }
}