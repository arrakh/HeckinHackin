using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class DEBUG_QuickChar : NetworkBehaviour, IDamageable
{
    [SerializeField] private Transform floatingTextPos;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkAnimator netAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 movement;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float runStaminaDrain = 20f;
    [SerializeField] private float staminaRecoveryRate = 10f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private Canvas playerCanvas;

    public Vector2 lastDirection;
    public bool hasInitData = false;
    public bool isMoving = false;
    public Action<float, float> OnHealthUpdate;
    public Action<float, float> OnStaminaUpdate;
    public Action<PlayerData> OnDataUpdate;

    [SyncVar] private bool isAttacking = false;
    [SyncVar] private bool isRunning = false;
    [SyncVar] private bool canMove = true;
    [SyncVar(hook = nameof(OnHealthChanged))] public float health;
    [SyncVar(hook = nameof(OnStaminaChanged))] public float stamina;
    [SyncVar(hook = nameof(OnPlayerDataChanged))] public PlayerData data;

    [Client]
    private void Start()
    {
        playerCanvas.worldCamera = CameraManager.Instance.GetCamera();

        if (hasAuthority)
        {
            CameraManager.Instance.SetFollow(this.gameObject);
        }
        else
        {
            staminaBar.SetActive(false);
        }
    }

    void Update()
    {
        AuthorityUpdate();
        ServerUpdate();
    }

    private void AuthorityUpdate()
    {
        //AuthorityFunction
        if (!hasAuthority) return;

        DetectInput();
        AnimationUpdate();
    }

    private void ServerUpdate()
    {
        //ServerFunction
        if (!isServer) return;

        StaminaFunction();
    }

    public void Initialize(PlayerData data)
    {
        this.data = data;
        health = maxHealth;
        stamina = maxStamina;
        hasInitData = true;
    }

    private void DetectInput()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.RightControl)) CMD_AddHealth(-10);
        if (Input.GetKeyDown(KeyCode.RightShift)) CMD_AddHealth(10);

        //Movement Input
        MovementInput();

        //Combat Input
        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            CMD_Attack();
            netAnimator.SetTrigger("Attack");
            Debug.Log("Attack");
        }

        if (Input.GetButtonDown("Run")) RunMovement(true);
        if (Input.GetButtonUp("Run")) RunMovement(false);

    }

    private void MovementInput()
    {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        isMoving = false;

        if (!canMove) return;
        if (movement.magnitude > 0.1)
        {
            isMoving = true;
            lastDirection = movement;
        }

        transform.position += (Vector3)movement.normalized * (isRunning ? runSpeed : walkSpeed) * Time.deltaTime;
    }

    [Command]
    private void RunMovement(bool isKeyDown)
    {
        isRunning = isKeyDown;
        animator.SetFloat("MoveSpeed" , isKeyDown ? 1.7f : 1f);
    }

    private void AnimationUpdate()
    {
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MovementX", lastDirection.x);
        animator.SetFloat("MovementY", lastDirection.y);
    }

    private void StaminaFunction()
    {
        if (isRunning)
        {
            stamina -= runStaminaDrain * Time.deltaTime;
        }
        else
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    [Command]
    private void CMD_Attack()
    {
        //check if can attack

        StartCoroutine(Cooldown_Attack(0.4f));
    }

    [ClientRpc]
    private void RPC_Attack()
    {
        //RPC attack to non owner
        if (hasAuthority) return;
    }

    IEnumerator Cooldown_Attack(float duration)
    {
        isAttacking = true;
        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    [Command]
    public void CMD_AddHealth(float value) => CMD_SetHealth(health + value);

    [Command]
    public void CMD_SetHealth(float value) => health = Mathf.Clamp(value, 0, maxHealth);

    public void SetIsAttacking(int value)
    {
        if (!isServer) return;
        isAttacking = value == 0 ? false : true; 
    }

    public void SetCanMove(int value)
    {
        if (!isServer) return;
        canMove = value == 0 ? false : true;
    }

    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => health;
    public float GetMaxStamina() => maxStamina;
    public float GetCurrentStamina() => stamina;

    private void OnHealthChanged(float oldval, float newval)
    {
        OnHealthUpdate?.Invoke(oldval, newval);

        var diff = newval - oldval;
        Debug.Log(diff);
        VFXManager.Instance.SpawnFloatingText
            (
            diff.ToString(), 
            diff > 0 ? Color.green : Color.red, 
            floatingTextPos.position, 
            16,
            UnityEngine.Random.Range(1f, 2f),
            UnityEngine.Random.Range(0.5f, 1f)
            );
    }

    private void OnStaminaChanged(float oldval, float newval)
    {
        OnStaminaUpdate?.Invoke(oldval, newval);
    }

    private void OnPlayerDataChanged(PlayerData oldval, PlayerData newval)
    {
        OnDataUpdate?.Invoke(newval);
    }

    public void Damage(float damage, GameObject damager)
    {
        CMD_AddHealth(-damage);
    }
}
