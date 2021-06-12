using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Color colorPick;

    // Start is called before the first frame update
    void Start()
    {
        Renderer ren = GetComponent<Renderer>();
        ren.material.SetColor("_Color", colorPick);
    }

    public Color GetColor() 
    {
        return colorPick;
    }
}
