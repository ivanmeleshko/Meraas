using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//[RequireComponent(typeof(LineRenderer))]
public class Ring : MonoBehaviour
{
    [Range(0,256)]
    public int segments = 128;
    [Range(0, 500)]
    public float xradius = 50f;
    [Range(0, 500)]
    public float yradius = 50f;
    LineRenderer line;
    float x = 0.0f;
    float y = 0.0f;
    float z = 1.0f;


    void Start()
    {
        line = Settings.instance.ring;
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        Settings.instance.ring = line;
    }


    public Ring(LineRenderer _lr)
    {
        line = _lr;
    }


    public void CreatePoints()
    {
        float angle = 20f;
        Settings.instance.ring.startWidth = 5f;
        Settings.instance.ring.endWidth = 5f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            Settings.instance.ring.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }        
    }


    public void OnValueChanged(float value)
    {
        xradius = value;
        yradius = value;
        CreatePoints();
    }
}
