using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera m_camera;
    public Transform target;
    private Vector3 offset = new Vector3(7, 7, -12);
    public bool nextobj;

    void Start()
    {
        nextobj = true;
        m_camera = GetComponent<Camera>();
        GetTargetByTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextobj == true)
        {
            GetTargetByTag("Player");
            if (target)
            {
                transform.position = target.transform.position + offset;
            }

            if (target == null)
                return;
        }
        else if (nextobj == false) 
        {
            GetTargetByTag("Parent");
            if (target)
            {
                transform.position = target.transform.position + offset;
            }

            if (target == null)
                return;
        }
      
    }

    /// Changes the target.
    public void ChangeTarget(Transform _target)
    {
        target = _target;
    }

    /// Gets the target by tag.
    public void GetTargetByTag(string _tag)
    {
        GameObject obj = GameObject.FindWithTag(_tag);
        if (obj)
        {
            ChangeTarget(obj.transform);
        }
        else
        {
            Debug.Log("Cant find object with tag " + _tag);
        }
    }
}
