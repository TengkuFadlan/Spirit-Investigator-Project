using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector2 movementDirectionInput;
    public Vector2 lastMovementDirection = Vector2.down;
    public float moveSpeed;

    Rigidbody2D rigidBody2D;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 normalizedMovementDirection = movementDirectionInput.normalized;

        rigidBody2D.velocity = normalizedMovementDirection * moveSpeed;

        if (normalizedMovementDirection != Vector2.zero)
            lastMovementDirection = normalizedMovementDirection;
    }
}
