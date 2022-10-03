using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Follow;
    public float maxDist = 10.0f;
    public Vector2 offset = new(0.5f, 0.0f);
    public float clipAdjustDist;
    Vector3 angle;
    void Start()
    {
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

        var centre = Follow.position + rotation * (Vector3.right * offset.x + Vector3.up * offset.y);

        Ray ray = new(centre, rotation * Vector3.back * maxDist);
        Debug.DrawRay(centre, rotation * Vector3.back * maxDist);
        RaycastHit hit;

        float finalDistance;
        Vector3 clipAdjustment = Vector3.zero;
        if (!Physics.Raycast(ray, out hit) || hit.distance > maxDist)
        {
            finalDistance = maxDist;
        }
        else
        {
            finalDistance = hit.distance;
            clipAdjustment = hit.normal * clipAdjustDist;
        }

        transform.position = (rotation * Vector3.back * finalDistance) + centre + clipAdjustment;
        transform.rotation = rotation;
    }
}
