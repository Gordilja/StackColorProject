using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Color myColor;
    public Renderer[] myRen;
    public Rigidbody rb;

    int speed = 15;
    int value;

    public float forwardForce;
    public float addForce;
    public float reduceForce;

    public static Action<float> Kick;

    public bool playing;
    bool move;
    bool end;

    Transform pickUpmain;
    public Transform stackPos;

    private void Start()
    {
        playing = false;
        SetColor(myColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            forwardForce -= reduceForce * Time.deltaTime;
            if (forwardForce < 0)
                forwardForce = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (end)
            {
                forwardForce += addForce;
            }
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            if (end)
                return;

            if (playing == false) 
            {
                playing = true;
                FindObjectOfType<GameManager>().startPanel.SetActive(false);
            }  
        }
        movePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "EnterFinish")
        {
            Time.timeScale = 0.5f;
            FindObjectOfType<GameManager>().taptap.SetActive(true);
            Debug.Log(other.tag);
            playing = false;
            end = true;
            move = true;
        }

        if (other.tag == "Finish")
        {
            Time.timeScale = 1;   
            move = false;
            rb.isKinematic = true;
            FindObjectOfType<GameManager>().taptap.SetActive(false);
            Launch();
            StartCoroutine(restartGame());
        }

        if (end)
            return;

        if (other.tag == "Points")
        {
            Transform otherTransform = other.transform;
            if (myColor == otherTransform.GetComponent<ObjectPickup>().colorPick)
            {
                value += 1;
            }
            else
            {
                value--;
                //FindObjectOfType<GameManager>().scoreUp(value);
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
            //Renderer otherRen = otherTransform.GetComponent<Renderer>();
            otherRb.isKinematic = true;
            other.enabled = false;

            if (pickUpmain == null)
            {
                pickUpmain = otherTransform;
                pickUpmain.position = stackPos.position;
                pickUpmain.parent = stackPos;
                pickUpmain.tag = "Parent";
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

    void Launch() 
    {
        //Camera.main.GetComponent<CameraMove>().ChangeTarget(pickUpmain);
        FindObjectOfType<CameraMove>().nextobj = false;
        Kick(forwardForce);
    }
    void movePlayer() 
    {
        if (playing)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < Screen.width / 2 && transform.position.x > -3.3f)
                        transform.Translate(Vector3.left * speed * Time.deltaTime);
                    if (touch.position.x > Screen.width / 2 && transform.position.x < 3.3f)
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
            }

            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            if (Input.GetKey("d") && transform.position.x < 3.3f)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (Input.GetKey("a") && transform.position.x > -3.3f)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        else if (move == true) 
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    IEnumerator restartGame() 
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<GameManager>().restartPanel.SetActive(true);
    }
}
