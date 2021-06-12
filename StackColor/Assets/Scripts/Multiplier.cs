using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public float multiplierValue;
    public Color planeColor;
    public Renderer[] rends;

    // Start is called before the first frame update
    void Start()
    {
        SetColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Points") 
        {
            FindObjectOfType<GameManager>().Multiply(multiplierValue);
        }
    }

    void SetColor() 
    {
        for (int i = 0; i < rends.Length; i++) 
        {
            rends[i].material.SetColor("_Color", planeColor);
        }
    }
}
