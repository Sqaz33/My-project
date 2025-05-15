using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(CharacterController))]

public class NewBehaviourScript : MonoBehaviour
{

    CharacterController characterController;
    [SerializeField] float moveSpeed = 6f;
    Vector2 moveInput;
    Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        movement.x = moveInput.x + moveSpeed;
        characterController.Move(movement * Time.fixedDeltaTime);
    }
}
