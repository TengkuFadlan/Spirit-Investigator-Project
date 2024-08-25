using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    CharacterMovement characterMovement;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        animator.SetFloat("Horizontal", characterMovement.movementDirectionInput.x);
        animator.SetFloat("Vertical", characterMovement.movementDirectionInput.y);
        animator.SetFloat("LastHorizontal", characterMovement.lastMovementDirection.x);
        animator.SetFloat("LastVertical", characterMovement.lastMovementDirection.y);
    }
}
