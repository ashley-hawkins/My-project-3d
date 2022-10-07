using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    enum Movement
    {
        Walk,
        Grapple
    }
    Movement movement;
    CharacterController cc;
    Camera cam;
    // Speed in metres per second.
    public float speed = 5.0f;
    public float maxTurnSpeed = 20.0f;

    GrapplerBehaviour grappler;
    // Start is called before the first frame update
    void Start()
    {
        grappler = GetComponent<GrapplerBehaviour>();
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }
    void ProcessMovement()
    {
        switch (movement)
        {
            case Movement.Walk:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        Quaternion camRotation = cam.transform.rotation;
                        Vector3 eulerAngles = camRotation.eulerAngles;

                        var playerDirection = Quaternion.Euler(new Vector3(0, eulerAngles.y, 0));

                        var playerVelVector = playerDirection * Vector3.forward * speed;
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, eulerAngles.y, transform.rotation.eulerAngles.z);
                        cc.SimpleMove(playerVelVector);
                    }
                    else
                    {
                        cc.SimpleMove(Vector3.zero);
                    }
                    break;
                }
            case Movement.Grapple:
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        movement = Movement.Walk;
                        grappler.EndGrapple();
                    }
                    break;
                }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                print("Name of object hit: " + hit.collider.gameObject.name);
                movement = Movement.Grapple;
                grappler.StartGrapple(hit.point, hit.collider.gameObject);
            }
            else
            {
                print("No hit.");
            }
        }
    }
}
