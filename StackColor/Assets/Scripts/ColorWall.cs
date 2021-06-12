using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    public Color newColor;
    Collider mainCol;

    // Start is called before the first frame update
    void Start()
    {
        Color tempColor = newColor;
        tempColor.a = 0.5f;
        Renderer ren = transform.GetChild(0).GetComponent<Renderer>();
        ren.material.SetColor("_Color", tempColor);
        mainCol = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(waitCollider());
        }    
    }

    public Color GetColor() 
    {
        return newColor;
    }

    IEnumerator waitCollider() 
    {
        mainCol.enabled = !GetComponent<Collider>();
        yield return new WaitForSeconds(2);
        mainCol.enabled = true;
    }
}
