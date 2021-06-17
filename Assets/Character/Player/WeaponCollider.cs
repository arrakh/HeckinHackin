using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private Mirror.NetworkIdentity id;
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(!id.isServer + " / " + (collision.gameObject == transform.parent.gameObject));
        if (!id.isServer) return;
        if (collision.gameObject == transform.parent.gameObject) return;

        var target = collision.GetComponent<IDamageable>();

        target.Damage(damage, transform.parent.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
