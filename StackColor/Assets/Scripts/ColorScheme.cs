using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScheme : MonoBehaviour
{
    public Color myColor;
    public Renderer[] myRen;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(myColor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetColor(Color colorin) 
    {
        myColor = colorin;
        for (int i=0; i < myRen.Length; i++) 
        {
            myRen[i].material.SetColor("_Color", myColor);
        }
    }
}
