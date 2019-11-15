using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float speed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.Normalize(direction * Time.deltaTime) * speed);
        //transform.localEulerAngles(Vector3)
    }
}
