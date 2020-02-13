using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLink : MonoBehaviour
{

    void Start()
    {
        DisplayVillas();
    }


    private void Update()
    {
        if (Settings.instance.eventChanged)
        {
            Settings.instance.eventChanged = false;

            if (Settings.instance.displayedVillas != null)
                Settings.instance.displayedVillas.Clear();

            Settings.instance.displayedVillas = new List<Flat>();

            foreach (Flat villa in Settings.instance.villas)
            {
                if (Settings.instance.available)
                {
                    if (villa.availability == Flat.Availability.Available)
                        Settings.instance.displayedVillas.Add(villa);
                }
                if (Settings.instance.reserved)
                {
                    if (villa.availability == Flat.Availability.Reserved)
                        Settings.instance.displayedVillas.Add(villa);
                }
                if (Settings.instance.sold)
                {
                    if (villa.availability == Flat.Availability.Sold)
                        Settings.instance.displayedVillas.Add(villa);
                }
            }

            List<Flat> tempList = new List<Flat>(Settings.instance.displayedVillas);

            foreach (Flat villa in tempList)
            {
                if (!Settings.instance.townHouse)
                {
                    if (villa.fType == Flat.FlatType.DoubleRoom)
                        Settings.instance.displayedVillas.Remove(villa);
                }
                if (!Settings.instance.twinhome)
                {
                    if (villa.fType == Flat.FlatType.DoubleBedroom)
                        Settings.instance.displayedVillas.Remove(villa);
                }
                //if (!Settings.instance.luxury)
                //{
                //    if (villa.fType == Flat.FlatType.DoubleBedroom)
                //        Settings.instance.displayedVillas.Remove(villa);
                //}
            }

            tempList = new List<Flat>(Settings.instance.displayedVillas);

            foreach (Flat villa in tempList)
            {
                if (villa.price < Settings.instance.minPrice || villa.price > Settings.instance.maxPrice ||
                    villa.surface < Settings.instance.minSurface / 12 || villa.surface > Settings.instance.maxSurface / 12)
                {
                    Settings.instance.displayedVillas.Remove(villa);
                }
            }

            UpdateLinks();
        }            
    }


    public void DisplayVillas()
    {
        if (Settings.instance.displayedVillas == null)
        {
            Settings.instance.displayedVillas = new List<Flat>();
        }

        foreach (Flat villa in Settings.instance.villas)
        {
            if (villa.unitId.Equals(gameObject.name))
            {
                if (villa.availability == Flat.Availability.Available 
                       && villa.fType  == Flat.FlatType.DoubleRoom 
                       && villa.price   < BudgetSliderMax.Value * 1000000 && villa.price > BudgetSliderMin.Value * 1000000
                       && villa.surface < SquareSliderMax.Value /12 && villa.surface > SquareSliderMin.Value / 12)
                {
                    Settings.instance.displayedVillas.Add(villa);
                    Image link = gameObject.GetComponentInChildren<Image>();
                    link.gameObject.SetActive(true);
                }
                else
                {
                    //Settings.displayedVillas.Remove(villa);
                    Image link = gameObject.GetComponentInChildren<Image>();
                    link.gameObject.SetActive(false);
                }
            }
        }
    }


    public void UpdateLinks()
    {
        DisableLinks();

        foreach (Flat villa in Settings.instance.displayedVillas)
        {
            GameObject go = GameObject.Find(villa.name);
            Image link = go.GetComponentInChildren<Image>(true);
            link.gameObject.SetActive(true);
        }
    }


    private void DisableLinks()
    {
        foreach (Flat villa in Settings.instance.villas)
        {
            GameObject go = GameObject.Find(villa.name);
            Image link = go.GetComponentInChildren<Image>(true);
            link.gameObject.SetActive(false);
        }
    }

}
