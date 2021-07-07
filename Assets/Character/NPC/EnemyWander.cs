using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyWander : NetworkBehaviour
{
    [SerializeField] private float idleDurationMin = 2f;
    [SerializeField] private float idleDurationMax = 6f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float wanderRadius = 5f;
    [SerializeField] private Animator animator;

    private Coroutine idleCo;
    private Coroutine walkCo;

    private Vector3 movementDir;

    [Server]
    private void Start()
    {
        idleCo = StartCoroutine(Idle(1f));
    }

    IEnumerator Idle(float duration)
    {
        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(duration);
        if (walkCo != null) StopCoroutine(walkCo);
        walkCo = StartCoroutine(WalkTo(transform.position.RandomCircle(wanderRadius)));
    }

    IEnumerator WalkTo(Vector3 position)
    {
        movementDir = position - transform.position;

        animator.SetBool("IsWalking", true);
        animator.SetFloat("DirX", movementDir.x);
        animator.SetFloat("DirY", movementDir.y);

        var t = idleDurationMax;

        while (Vector3.Distance(position, transform.position) > 0.1f)
        {
            transform.position += movementDir.normalized * Time.deltaTime * walkSpeed;
            yield return new WaitForEndOfFrame();
        }

        if (idleCo != null) StopCoroutine(idleCo);
        idleCo = StartCoroutine(Idle(Random.Range(idleDurationMin, idleDurationMax)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (walkCo != null) StopCoroutine(walkCo);
            if (idleCo != null) StopCoroutine(idleCo);
            StartCoroutine(Idle(1f));
        }
    }
}
