using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    public Color newColor;

    // Start is called before the first frame update
    void Start()
    {
        Color tempColor = newColor;
        tempColor.a = 0.5f;
        Renderer ren = transform.GetChild(0).GetComponent<Renderer>();
        ren.material.SetColor("_Color", tempColor);
    }

    public Color GetColor() 
    {
        return newColor;
    }

    IEnumerator waitCollider() 
    {
        
        yield return new WaitForSeconds(2);
    }
}
