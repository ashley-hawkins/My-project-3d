using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shared : MonoBehaviour
{
    // Start is called before the first frame update
    Collider cd;
    [Range(0.0f, 1.0f)]
    public float extension = 0.2f;
    void Start()
    {
        cd = GetComponent<Collider>();
    }

    public bool DoRayCollisionCheck()
    {
        float rayLength = cd.bounds.extents.y + extension;

        Vector3[] positions =
        {
            cd.bounds.center,
            cd.bounds.center + new Vector3(cd.bounds.extents.x, 0.0f, 0.0f),
            cd.bounds.center - new Vector3(cd.bounds.extents.x, 0.0f, 0.0f),
            cd.bounds.center + new Vector3(0.0f, 0.0f, cd.bounds.extents.z),
            cd.bounds.center - new Vector3(0.0f, 0.0f, cd.bounds.extents.z)
        };

        RaycastHit hit;
        bool result = true;
        foreach (var pos in positions)
        {
            Color hitColor = Color.green;
            if (Physics.Raycast(pos, Vector3.down, out hit, rayLength))
            {
                hitColor = Color.red;
                print($"{tag} has collided with ${hit.collider.tag}");
            }
            else
            {
                result = false;
            }
            Debug.DrawRay(pos, Vector3.down * rayLength, hitColor, 5.0f);
        }

        print("Checking for collision");
        return result;
    }
}
