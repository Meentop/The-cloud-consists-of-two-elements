using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeting : MonoBehaviour
{
    float speed = 3f;
    [SerializeField] Transform target;

    private void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector3 pos = target.position;
        pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.fixedDeltaTime);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
