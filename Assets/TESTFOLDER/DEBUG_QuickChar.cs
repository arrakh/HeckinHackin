using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DEBUG_QuickChar : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 movement;
    [SerializeField] private float moveSpeed = 5f;

    public Vector2 lastDirection;
    public bool isMoving = false;

    [Client]
    private void Start()
    {
        if (!hasAuthority) return;
        CameraManager.Instance.SetFollow(this.gameObject);
    }

    void Update()
    {
        if (!hasAuthority) return; 

        DetectInput();
        AnimationUpdate();
    }

    private void DetectInput()
    {

        //Movement Input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        isMoving = false;

        if(movement.magnitude > 0.1)
        {
            isMoving = true;
            lastDirection = movement;
        }

        transform.position += (Vector3)movement.normalized * moveSpeed * Time.deltaTime;

    }

    private void AnimationUpdate()
    {
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MovementX", lastDirection.x);
        animator.SetFloat("MovementY", lastDirection.y);
    }
}
