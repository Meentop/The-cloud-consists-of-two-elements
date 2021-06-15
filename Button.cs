using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Mechanism mechanism;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Solid")
        {
            mechanism.On();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            mechanism.Off();
        }
    }
}
