using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Apply this script to the parent camera object
//Using the parent object helps prevent some weird camera movement and jittering
public class MoveCamera : MonoBehaviour
{
    public Transform playerHead;

    void Update()
    {
        transform.position = playerHead.transform.position;
    }
}
