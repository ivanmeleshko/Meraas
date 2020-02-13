using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalePoints : MonoBehaviour
{

    public List<Image> Points; 


    void Update()
    {
        foreach (Image img in Points)
        {
            //img.transform.localScale = Vector3.one *  Mathf.Sqrt(20f * Vector3.Distance(Camera.main.transform.position, img.transform.position)
            //    * Vector3.Distance(Camera.main.transform.position, img.transform.position)) / 800f;
            img.transform.localScale = Vector3.one * Mathf.Sqrt(Vector3.Distance(Camera.main.transform.position, img.transform.position)) / 20f;
        }
    }
}
