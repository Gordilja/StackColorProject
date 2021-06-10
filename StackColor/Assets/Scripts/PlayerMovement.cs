using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int speed = 5;

    Transform pickUpmain;
    public Transform stackPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed );
        if (Input.GetKey("d")) 
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Points") 
        {
            Transform otherTransform = other.transform;
            Rigidbody otherRb = otherTransform.GetComponent<Rigidbody>();
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
                otherTransform.parent = stackPos;
            }
        }
    }
}
