using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    CharacterController cc;
    Camera cam;
    // Speed in metres per second.
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Quaternion camRotation = cam.transform.rotation;
            Vector3 eulerAngles = camRotation.eulerAngles;

            var playerDirection = Quaternion.Euler(new Vector3(0, eulerAngles.y, 0));

            var playerVelVector = playerDirection * Vector3.forward * speed;
            cc.SimpleMove(playerVelVector);
        }
        else
        {
            cc.SimpleMove(Vector3.zero);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print("Name of object hit: " + hit.collider.gameObject.name);
            }
            else
            {
                print("No hit.");
            }
        }
    }
}
