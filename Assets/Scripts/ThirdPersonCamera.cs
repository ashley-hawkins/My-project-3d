using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    Quaternion angle;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        angle = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
