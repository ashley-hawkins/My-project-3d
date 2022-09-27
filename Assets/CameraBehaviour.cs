using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Follow;
    public float maxDist = 10.0f;
    Vector3 angle;
    BoxCollider myCollider;
    Rigidbody rb;
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        angle = new();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        UpdateLook(mouseX, mouseY);
    }
    void UpdateLook(float mouseX, float mouseY)
    {
        angle.x -= mouseY;
        angle.y += mouseX;

        Quaternion rotation = Quaternion.Euler(angle);

        Ray ray = new(Follow.position, rotation * Vector3.back * maxDist);
        Debug.DrawRay(Follow.position, rotation * Vector3.back * maxDist);
        RaycastHit hit;

        float finalDistance;
        if (!Physics.Raycast(ray, out hit) || hit.distance > maxDist)
        {
            finalDistance = maxDist;
        }
        else
        {
            finalDistance = hit.distance;
        }

        rb.Move((rotation * Vector3.back * finalDistance) + Follow.position, rotation);
    }
}
