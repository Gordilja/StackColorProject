using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Color myColor;
    public Renderer[] myRen;

    int speed = 5;
    int value;

    Transform pickUpmain;
    public Transform stackPos;

    private void Start()
    {
        SetColor(myColor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed );
        if (Input.GetKey("d") && transform.position.x < 3.3f) 
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey("a") && transform.position.x > -3.3f)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Points")
        {
            Transform otherTransform = other.transform;
            if (myColor == otherTransform.GetComponent<ObjectPickup>().GetColor())
            {
                value++;
            }
            else
            {
                value--;
                FindObjectOfType<GameManager>().scoreUp(value);
                Destroy(other.gameObject);
                if (pickUpmain != null) 
                {
                    if (pickUpmain.childCount >= 1)
                    {
                        pickUpmain.position -= Vector3.up * pickUpmain.GetChild(pickUpmain.childCount - 1).localScale.y;
                        Destroy(pickUpmain.GetChild(pickUpmain.childCount - 1).gameObject); 
                    }
                    else 
                    {
                        Destroy(gameObject);
                    }
                }
                return;
            }
            
            Debug.Log(other.tag);

            Rigidbody otherRb = otherTransform.GetComponent<Rigidbody>();
            Renderer otherRen = otherTransform.GetComponent<Renderer>();
            //otherRen.material.SetColor("_Color", GetComponent<ColorScheme>().myColor);
            otherRb.isKinematic = true;
            other.enabled = false;

            if (pickUpmain == null)
            {
                pickUpmain = otherTransform;
                pickUpmain.position = stackPos.position;
                pickUpmain.parent = stackPos;
            }
            else
            {
                pickUpmain.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = stackPos.position;
                otherTransform.parent = pickUpmain;
            }
            FindObjectOfType<GameManager>().scoreUp(value);
        }
        if (other.tag == "Wall")
        {
            SetColor(other.GetComponent<ColorWall>().GetColor());
        }
    }

    public void SetColor(Color colorin)
    {
        myColor = colorin;
        for (int i = 0; i < myRen.Length; i++)
        {
            myRen[i].material.SetColor("_Color", myColor);
        }
    }
}
