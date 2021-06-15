using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] GameObject eKey;
    [SerializeField] Mechanism mechanism;
    SpriteRenderer sprite;
    bool isOn = false, isActive = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
            OnArm();
    }

    public void Activation()
    {
        eKey.SetActive(true);
        isActive = true;
    }

    public void Deactivation()
    {
        eKey.SetActive(false);
        isActive = false;
    }

    void OnArm()
    {
        if(!isOn)
        {
            isOn = true;
            sprite.flipX = true;
            mechanism.On();
        }
        else
        {
            isOn = false;
            sprite.flipX = false;
            mechanism.Off();
        }
    }
}
