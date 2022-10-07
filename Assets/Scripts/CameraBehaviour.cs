using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform follow;
    public float startDist = 10.0f;
    public float maxDist = 25.0f;
    public float minDist = 10.0f;
    public float desiredDist = 0.0f;
    public Vector3 offset = new(0.5f, 0.0f, 0.0f);

    [Range(0.0f, 2.0f)]
    public float sensitivity = 1.0f;
    public float cameraBoundingBoxExtent = 0.3f;
    Vector3 angle;
    void Start()
    {
        desiredDist = ClampDist(desiredDist);
        angle = new();
    }

    // Update is called once per frame
    void Update()
    {
        var mouseDelta = Mouse.current.delta;
        float mouseX = mouseDelta.x.ReadValue() * sensitivity;
        float mouseY = mouseDelta.y.ReadValue() * sensitivity;

        float scroll = Input.mouseScrollDelta.y;

        desiredDist = ClampDist(desiredDist - scroll);

        UpdateLook(mouseX, mouseY);
    }
    void UpdateLook(float mouseX, float mouseY)
    {
        angle.x = Mathf.Clamp(angle.x - mouseY, -89, 89);
        angle.y = (angle.y + mouseX) % 360;

        Quaternion rotation = Quaternion.Euler(angle);

        var centre = follow.position + rotation * offset;

        // Ray ray = new(centre, rotation * Vector3.back * desiredDist);
        Debug.DrawRay(centre, rotation * Vector3.back * desiredDist);

        float finalDistance;

        if (!Physics.BoxCast(centre, Vector3.one * cameraBoundingBoxExtent, rotation * Vector3.back, out RaycastHit hit, rotation) || hit.distance > desiredDist)
        {
            finalDistance = desiredDist;
        }
        else
        {
            finalDistance = hit.distance;
        }

        float opacity = 1.0f;
        if (finalDistance < 1.0f)
        {
            opacity = 0.3f;
        }
        Renderer[] materialComponents = follow.GetComponentsInChildren<Renderer>();

        foreach (Renderer x in materialComponents)
        {
            print(x.material.name);
            var c = x.material.color;
            c.a = opacity;
            x.material.color = c;
        }

        transform.position = (rotation * Vector3.back * finalDistance) + centre;
        transform.rotation = rotation;
    }
    float ClampDist(float newDesired)
    {
        return Mathf.Clamp(newDesired, minDist, maxDist);
    }
}
