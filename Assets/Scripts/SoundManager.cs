using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    private AudioSource audioSource;
    private AudioSource firegunAudioSource;

    [Header("----------VOLUME----------")]
    [Range(0f, 1f)]
    [SerializeField] private float ballistaShotVolume;
    [Range(0f, 1f)]
    [SerializeField] private float arrowHitVolume;
    [Range(0f, 1f)]
    [SerializeField] private float mortarShotVolume;
    [Range(0f, 1f)]
    [SerializeField] private float bombExplosionVolume;
    [Range(0f, 1f)]
    [SerializeField] private float firegunParticleVolume;
    [Range(0f, 1f)]
    [SerializeField] private float superBombMagnetEffectVolume;
    [Range(0f, 1f)]
    [SerializeField] private float firegunAbilityVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var crossbow = FindObjectOfType<Crossbow>();
        if (crossbow)
        {
            crossbow.OnBallistaDefaultShot += Crossbow_OnBallistaDefaultShot;
            crossbow.OnBallistaSuperShot += Crossbow_OnBallistaSuperShot;
        }


        var mortar = FindObjectOfType<Mortar>();
        if (mortar)
            mortar.OnMortarShot += Mortar_OnMortarShot;

        var firegun = FindObjectOfType<FireGun>();

        if (firegun)
        {
            firegun.OnFireGunStartShooting += FireGun_OnFireGunStartShooting;
            firegun.OnFireGunStopShooting += FireGun_OnFireGunStopShooting;
            firegunAudioSource = firegun.GetComponent<AudioSource>();
            firegun.OnAbilityAction += FireGun_OnAbilityAction;
        }


        var bombProjectile = FindObjectOfType<BombProjectile>();
        if (bombProjectile)
            bombProjectile.OnBombExplosion += BombProjectile_OnBombExplostion;

        var superBombProjectile = FindObjectOfType<SuperBombProjectile>();
        if (superBombProjectile)
            superBombProjectile.OnSuperBombActivation += SuperBombProjectile_OnSuperBombActivation;

        var arrowProjectile = FindObjectOfType<ArrowProjectile>();
        if (arrowProjectile)
            arrowProjectile.OnArrowHit += ArrowProjectile_OnArrowHit;

    }

    private void FireGun_OnAbilityAction()
    {
        PlaySound(audioClipsRefsSO.firegunAbilitySound, firegunAbilityVolume);
    }

    private void ArrowProjectile_OnArrowHit()
    {
        PlaySound(audioClipsRefsSO.arrowHitSound, arrowHitVolume);
    }

    private void BombProjectile_OnBombExplostion()
    {
        PlaySound(audioClipsRefsSO.bombExplosionSound, bombExplosionVolume);
    }

    private void SuperBombProjectile_OnSuperBombActivation()
    {
        PlaySound(audioClipsRefsSO.bombExplosionSound, bombExplosionVolume);
        PlaySound(audioClipsRefsSO.superBombExplosionSound, superBombMagnetEffectVolume);
    }

    private void FireGun_OnFireGunStopShooting()
    {
        firegunAudioSource.Pause();
    }

    private void FireGun_OnFireGunStartShooting()
    {
        firegunAudioSource.clip = audioClipsRefsSO.firegunParticleSound;
        firegunAudioSource.volume = firegunParticleVolume;
        firegunAudioSource.Play();
    }

    private void Mortar_OnMortarShot()
    {
        PlaySound(audioClipsRefsSO.mortarShotSound, mortarShotVolume);
    }

    private void Crossbow_OnBallistaSuperShot()
    {
        PlaySound(audioClipsRefsSO.ballistaAbilityShotSound, ballistaShotVolume);
    }

    private void Crossbow_OnBallistaDefaultShot()
    {
        PlaySound(audioClipsRefsSO.ballistaShotSound, ballistaShotVolume);
    }

    private void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], volume);
    }
}
