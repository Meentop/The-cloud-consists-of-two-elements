using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Mechanism
{
    [SerializeField] float speed = 1.0f;

    [SerializeField] Vector3 point1, point2;
    Vector3 target;

    private void Start()
    {
        target = point1;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public override void Off()
    {
        target = point1;
    }

    public override void On()
    {
        target = point2;
    }
}
