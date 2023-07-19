using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObjects : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * speed * Time.deltaTime);
    }
}
