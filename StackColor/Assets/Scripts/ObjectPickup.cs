using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Color colorPick;
    public Rigidbody objectRB;
    public Collider objectCol;

    private void OnEnable()
    {
        PlayerControler.Kick += ObjKick;
    }

    private void OnDisable()
    {
        PlayerControler.Kick -= ObjKick;
    }

    private void ObjKick(float forceKick) 
    {
        transform.parent = null;
        objectCol.enabled = true;
        objectRB.isKinematic = false;
        objectRB.useGravity = true;
        objectRB.AddForce(new Vector3(0, forceKick, forceKick));
    }
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
    private void Update()
    {
        if (transform.position.y < -3) 
        {
            Destroy(gameObject);
        }
    }
}
