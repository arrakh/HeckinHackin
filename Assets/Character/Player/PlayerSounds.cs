using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private AudioClip[] attackAudio;
    [SerializeField] private AudioClip[] damagedAudio;
    [SerializeField] private AudioClip[] deathAudio;

    private void OnEnable()
    {
        player.OnAttack += AttackAudio;
        player.OnDamaged += DamagedAudio;
        player.OnDeath += DeathAudio;
    }

    private void OnDisable()
    {
        player.OnAttack -= AttackAudio;
        player.OnDamaged -= DamagedAudio;
        player.OnDeath -= DeathAudio;
    }

    private void AttackAudio() => Play(attackAudio);
    private void DamagedAudio() => Play(damagedAudio);
    private void DeathAudio() => Play(deathAudio);

    private void Play(AudioClip[] clip)
    {
        var rand = Random.Range(0, clip.Length - 1);
        if (player.hasAuthority) AudioManager.Instance.PlaySFX(clip[rand], 1f);
        else AudioManager.Instance.Play3DSFX(clip[rand], transform.position, 1f, 20f);
    }
}
