using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TankBody : Unit
{
    [SerializeField] protected float speedRotation = 5f;

    private InputControls inputControls;

    private void Awake()
    {
        inputControls = new InputControls();
    }

    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    private void Update()
    {
        var dir = inputControls.Tank.Move.ReadValue<float>();
        Move(dir);

        var rotation = inputControls.Tank.Rotate.ReadValue<float>();
        Rotate(rotation);
    }

    protected void Move(float dir)
    {
        transform.position += transform.TransformDirection(Vector3.up) * dir * speed * Time.deltaTime;
    }

    private void Rotate(float rot)
    {
        float angle = rot * speedRotation * Time.deltaTime;

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + angle);
    }
}
