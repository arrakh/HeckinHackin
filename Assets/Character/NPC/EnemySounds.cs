using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    //[SerializeField] private AudioClip[] attackAudio;
    [SerializeField] private AudioClip[] damagedAudio;
    [SerializeField] private AudioClip[] deathAudio;

    private void OnEnable()
    {
        //enemy.OnAttack += AttackAudio;
        enemy.OnDamaged += DamagedAudio;
        enemy.OnDeath += DeathAudio;
    }

    private void OnDisable()
    {
        //enemy.OnAttack -= AttackAudio;
        enemy.OnDamaged -= DamagedAudio;
        enemy.OnDeath -= DeathAudio;
    }

    //private void AttackAudio() => Play(attackAudio);
    private void DamagedAudio() => Play(damagedAudio);
    private void DeathAudio() => Play(deathAudio);

    private void Play(AudioClip[] clip)
    {
        var rand = Random.Range(0, clip.Length - 1);
        AudioManager.Instance.Play3DSFX(clip[rand], transform.position, 1f, 20f);
    }
}
