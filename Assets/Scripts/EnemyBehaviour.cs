using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletOrigin;

    Vector3 moveDirection = Vector3.forward;

    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var difference = (player.transform.position - transform.position);
        var euler = transform.rotation.eulerAngles;
        euler.y = Quaternion.LookRotation(difference).eulerAngles.y;
        transform.rotation = Quaternion.Euler(euler);

        if (!GetComponent<Shared>().DoRayCollisionCheck())
        {
            moveDirection = -moveDirection;
        }

        cc.SimpleMove(moveDirection * 5.0f);
    }
}
