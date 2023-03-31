using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Joystick Joystick;
    public float ForceValue;

    private void FixedUpdate() {
        Vector3 force = new Vector3(Joystick.Value.x, 0, Joystick.Value.y) * ForceValue;

        Rigidbody.AddForce(force);
    }
}
