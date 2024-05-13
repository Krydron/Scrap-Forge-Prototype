using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody rigidbody;
    [SerializeField] float speed = 1.0f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigidbody.velocity = transform.TransformDirection(new Vector3(moveInput.x * speed, rigidbody.velocity.y, moveInput.y * speed));
    }

    void OnMove(InputValue input)
    {
        Debug.Log("Move");
        moveInput = input.Get<Vector2>();
    }
}
