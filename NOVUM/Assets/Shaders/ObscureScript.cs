using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObscureScript : MonoBehaviour {

    Renderer m_ObjectRenderer;

    //referencing https://answers.unity.com/questions/316064/can-i-obscure-an-object-using-an-invisible-object.html

    void Start()
    {
        //Fetch the GameObject's Renderer component
        m_ObjectRenderer = GetComponent<Renderer>();
        //Change the GameObject's Material render queue so it renders after the obscuring object
        m_ObjectRenderer.material.renderQueue = 2002;
    }
}
