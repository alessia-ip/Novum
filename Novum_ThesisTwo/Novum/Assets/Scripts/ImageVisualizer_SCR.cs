using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;

public class ImageVisualizer_SCR : MonoBehaviour
{

    public AugmentedImage Image;

    public GameObject Artist;
    public GameObject Orbit;
    public GameObject SpaceShip;
    public GameObject Campus;

    public GameObject A1P1;
    public GameObject A2P1;
    public GameObject A3P1;
    public GameObject A4P1;

    public GameObject A1P2;
    public GameObject A2P2;
    public GameObject A3P2;
    public GameObject A4P2;

    // Update is called once per frame
    void Update()
    {

        //Common
        if (Image.Name == "Artist")
        {
            Artist.SetActive(true);
            return;
        }

        if (Image.Name == "Campus")
        {
            Orbit.SetActive(true);
            return;
        }

        //Player One
        if (Image.Name == "A1P1")
        {
            A1P1.SetActive(true);
            return;
        }
        if (Image.Name == "A2P1")
        {
            A2P1.SetActive(true);
            return;
        }

        //Player Two
        if (Image.Name == "A1P2")
        {
            A1P2.SetActive(true);
            return;
        }
        if (Image.Name == "A2P2")
        {
            A2P2.SetActive(true);
            return;
        }

    }
}
