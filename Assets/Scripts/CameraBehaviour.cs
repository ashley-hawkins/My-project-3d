using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform follow;
    public float maxDist = 10.0f;
    public float cameraBoundingBoxExtent = 0.3f;
    Vector3 angle;



    private Vector3 gm_centre;
    private Vector3 gm_end;
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

        // Vector3 centre = follow.position + (rotation * Vector3.forward * cameraBoundingBoxExtent);
        Vector3 centre = follow.position;
        //float actualMaxDist = maxDist + cameraBoundingBoxExtent;
        float actualMaxDist = maxDist;
        Debug.DrawRay(centre, rotation * Vector3.back * actualMaxDist);
        RaycastHit hit;
        bool didHit = false;


        float finalDistance;
        Vector3 clipAdjustment = Vector3.zero;
        if (!(didHit = Physics.BoxCast(centre, Vector3.one * cameraBoundingBoxExtent, rotation * Vector3.back, out hit)) || hit.distance > actualMaxDist)
        {
            finalDistance = maxDist;
        }
        else
        {
            finalDistance = hit.distance - (actualMaxDist - maxDist);
        }

        gm_centre = centre;
        if (didHit)
        {
            gm_end = centre + rotation * Vector3.back * hit.distance;
        }

        transform.position = (rotation * Vector3.back * finalDistance) + follow.position;
        transform.rotation = rotation;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gm_centre, Vector3.one * cameraBoundingBoxExtent * 2);
        Gizmos.DrawWireCube(gm_end, Vector3.one * cameraBoundingBoxExtent * 2);
    }
}
