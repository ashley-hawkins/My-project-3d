using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplerBehaviour : MonoBehaviour
{
    CharacterController cc;
    LineRenderer lr;
    public Transform origin;

    bool grappling = false;
    Vector3 grappleTo;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, origin.position);
        if (grappling)
        {
            Vector3 movementDirection = (grappleTo - transform.position).normalized;
            print(movementDirection);
            cc.Move(15 * movementDirection * Time.deltaTime);
        }
    }

    public void StartGrapple(Vector3 pos, GameObject attachedObject)
    {
        grappleTo = pos;
        lr.enabled = true;
        lr.SetPosition(1, pos);
        grappling = true;
    }
    public void EndGrapple()
    {
        lr.enabled = false;
        grappling = false;
    }
}
