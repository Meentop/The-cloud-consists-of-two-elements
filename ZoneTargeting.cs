using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTargeting : MonoBehaviour
{
    [SerializeField] string playerTag;
    Transform player;

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag(playerTag).Length == 1)
        {
            player = GameObject.FindGameObjectWithTag(playerTag).transform;
            transform.position = player.position;
        }
        else
        {
            transform.position = new Vector3(100, 100, transform.position.z);
        }
    }
}
