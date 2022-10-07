using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletOrigin;

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

        cc.SimpleMove(Vector3.zero);
    }
}
