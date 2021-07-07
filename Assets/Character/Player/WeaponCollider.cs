using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private PlayerScript player;

    private List<IDamageable> targets = new List<IDamageable>();

    private void Start()
    {
        player.OnAttack += Attack;
    }

    private void Update()
    {
        if (player.isMoving)
        {
            Vector2 dir = player.lastDirection;

            if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                transform.rotation = Quaternion.Euler(-45, 0, dir.x > 0 ? 90f : 270f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(-45, 0, dir.y > 0 ? 180f : 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && collision.gameObject != player.gameObject) targets.Add(damageable);
        if (damageable is Enemy)
        {
            Debug.Log("Is Enemy");
            ((Enemy)damageable).OnDeath += delegate { targets.Remove(damageable); };
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (targets.Contains(damageable)) targets.Remove(damageable);
    }

    //Should only be called by server
    public void Attack()
    {
        if (targets.Count <= 0) return;
        foreach (var target in targets.ToArray())
        {
            if (target == null) continue;
            target.Damage(player.GetDamage(), transform.parent.gameObject);
            Debug.Log(((MonoBehaviour)target).name);
        }
    }

}
