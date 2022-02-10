using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        // Local -> World
        // transform.TransformDirection

        // World -> Local
        // InverseTransformDirection

        //if (Input.GetKey(KeyCode.W))
        //    transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.S))
        //    transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.A))
        //    transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.D))
        //    transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }
}
