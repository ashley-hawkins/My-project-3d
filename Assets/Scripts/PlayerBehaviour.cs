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
    // Max speed in metres per second.
    public float speed = 10.0f;
    // acceleration in metres per second per second
    public float accel = 10.0f;

    public float maxTurnSpeed = 20.0f;

    // gravity measured in multiples of g
    public float gravity = 1.0f;

    public Vector3 vel;

    GrapplerBehaviour grappler;
    // Start is called before the first frame update
    void Start()
    {
        vel = Vector3.zero;
        grappler = GetComponent<GrapplerBehaviour>();
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Shared>().DoRayCollisionCheck();
        ProcessMovement();
    }
    void ProcessMovement()
    {
        Quaternion camRotation = cam.transform.rotation;
        Vector3 camEuler = camRotation.eulerAngles;

        var playerDirection = Quaternion.Euler(new Vector3(0, camEuler.y, 0));

        switch (movement)
        {
            case Movement.Walk:
                {
                    Vector3 accelAngle = Vector3.zero;
                    vel.y -= 9.8f * gravity * Time.deltaTime;

                    if (Input.GetKey(KeyCode.W))
                    {
                        accelAngle += Vector3.forward;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        accelAngle += Vector3.left;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        accelAngle += Vector3.right;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        accelAngle += Vector3.back;
                    }
                    else if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Shared>().DoRayCollisionCheck())
                    {

                    }

                    if (accelAngle.magnitude > 0)
                    {
                        Quaternion accelAngleQuaternion = Quaternion.LookRotation(accelAngle);
                        transform.rotation = accelAngleQuaternion * Quaternion.Euler(transform.rotation.eulerAngles.x, camEuler.y, transform.rotation.eulerAngles.z);
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
