using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputCharacterMovement : MonoBehaviour
{
    CharacterMovement characterMovement;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            characterMovement.movementDirectionInput.x = Input.GetAxisRaw("Horizontal");
            characterMovement.movementDirectionInput.y = Input.GetAxisRaw("Vertical");
        }
    }
}
