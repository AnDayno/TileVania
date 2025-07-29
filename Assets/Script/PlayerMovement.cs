using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    void Start()
    {
        
    }

    void Update()
    {
         
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move Input: " + moveInput);
    }
}
