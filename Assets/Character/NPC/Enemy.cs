using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Enemy : NetworkBehaviour, IDamageable
{
    [SerializeField] private Transform floatingTextPos;
    [SerializeField] private NetworkIdentity networkIdentity;
    [SerializeField] private float maxHealth = 100;

    public Action<float, float> OnHealthUpdate;
    public Action OnDeath;
    public Action OnDamaged;

    private GameObject lastDamager;

    [SyncVar(hook = nameof(OnHealthChanged))] 
    private float health;

    private void Start()
    {
        health = maxHealth;
        OnHealthUpdate?.Invoke(0, 100);
    }

    public void Damage(float damage, GameObject damager)
    {
        SetHealth(health - damage);
        lastDamager = damager;
        RPC_Damaged();
    }

    [ClientRpc]
    void RPC_Damaged()
    {
        VFXManager.Instance.SpawnHit(transform.position, true);
        OnDamaged?.Invoke();
    }

    private void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);
        if (health == 0) DeathFunc();
    }

    private void DeathFunc()
    {
        var p = lastDamager.GetComponent<PlayerScript>();
        if (p != null) p.coin++;

        RPC_DeathFunc();

        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    void RPC_DeathFunc()
    {
        VFXManager.Instance.SpawnHit(transform.position, false);
        OnDeath?.Invoke();
    }

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

    public float GetCurrentHealth() => health;
    public float GetMaxHealth() => maxHealth;
}
