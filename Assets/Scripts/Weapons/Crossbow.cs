using System;
using System.Collections;
using UnityEngine;

public class Crossbow : Weapon
{
    public Action OnAbilityAction;
    public Action OnBallistaDefaultShot;
    public Action OnBallistaSuperShot;

    public SkillManager skillsManager;

    [HideInInspector] public Vector3 target;
    //GameObject sentProjectile;
    //bool isSentProjectileDropped = true;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] GameObject superProjectile;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite superPowerSprite;
    [SerializeField] float coolDownTime;
    [SerializeField] int superShootsCount = 3;
    [SerializeField] public float superProjectileSpeed = 10f;
    [SerializeField] private CrossbowAbilityButton abilityButton;

    bool isReloading = false;
    bool isSuperPowerActivated = false;

    [Header("----------UPGRADING----------")]
    [SerializeField] private int currentDamageLevel;
    [SerializeField] public float projectileDamage;

    [SerializeField] private Transform FirstProjectileSpawnerTransform;
    [SerializeField] private Transform SecondProjectileSpawnerTransform;
    [SerializeField] private Transform ThirdProjectileSpawnerTransform;
    [SerializeField] private Transform FourthProjectileSpawnerTransform;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillsManager = FindObjectOfType<SkillManager>();
    }

    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.Crossbow)
        {
            abilityButtonUI.transform.localScale = new Vector3(buttonScale, buttonScale, 1);

            if (Application.isMobilePlatform)
            {
                Crosshair();
                CrosshairUnHide();
                Aim();
                if (!isReloading)
                {
                    if (Input.GetMouseButton(0))
                    {
                        target = crosshair.transform.position;

                        if (isSuperPowerActivated && superShootsCount > 0)
                        {
                            SuperShoot();
                        }
                        else
                        {
                            Shoot();
                        }
                    }
                }
            }
            else if (Input.GetMouseButton(1))
            {
                Crosshair();
                Aim();
                if (Input.GetMouseButton(0))
                {
                    target = crosshair.transform.position;

                    if (isSuperPowerActivated && superShootsCount > 0)
                    {
                        SuperShoot();
                    }
                    else
                    {
                        Shoot();
                    }
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                //CrosshairDisabled();
            }
        }
        else
        {
            abilityButtonUI.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (superShootsCount == 0)
        {
            isSuperPowerActivated = false;
            StartCoroutine(Cooldown());
        }
    }

    public override void Shoot()
    {
        if (isReloading)
        {
            return;
        }

        else if (skillsManager.crossbowPlusOneArrow)
        {
            CreateProjectile(projectile, FirstProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, 30));
        }
        else if (skillsManager.crossbowPlusTwoArrow)
        {
            CreateProjectile(projectile, SecondProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, -30));
        }
        else if (skillsManager.crossbowPlusThreeArrow)
        {
            CreateProjectile(projectile, ThirdProjectileSpawnerTransform.position, Quaternion.Euler(0, 0, 60));
        }
        else if (skillsManager.crossbowPlusFourArrow)
        {
            CreateAbilityProjectile(projectile, FirstProjectileSpawnerTransform.position, FirstProjectileSpawnerTransform.rotation, 30);
            CreateAbilityProjectile(projectile, SecondProjectileSpawnerTransform.position, SecondProjectileSpawnerTransform.rotation, -30);
            CreateAbilityProjectile(projectile, ThirdProjectileSpawnerTransform.position, ThirdProjectileSpawnerTransform.rotation, 30);
            CreateAbilityProjectile(projectile, FourthProjectileSpawnerTransform.position, FourthProjectileSpawnerTransform.rotation, -30);

            if (skillsManager.crossbowCanDoubleShot)
            {
                DoubleShoot();
            }
            else
            {
                CreateProjectile(projectile, projectileSpawnerTransform.position, projectileSpawnerTransform.rotation);
            }
        }

        if (skillsManager.crossbowCanDoubleShot)
        {
            DoubleShoot();
        }
        else
        {
            CreateProjectile(projectile, projectileSpawnerTransform.position, projectileSpawnerTransform.rotation);
        }

        isReloading = true;
        StartCoroutine(Reload());

        OnBallistaDefaultShot?.Invoke();
    }

    private void DoubleShoot()
    {
        CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, 0.5f, 0f), projectileSpawnerTransform.rotation);
        CreateProjectile(projectile, projectileSpawnerTransform.position + new Vector3(0f, -0.5f, 0f), projectileSpawnerTransform.rotation);
    }

    private void CreateAbilityProjectile(GameObject projectile, Vector3 spawnPosition, Quaternion rotation, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        rotation = Quaternion.Euler(0, 0, angleInDegrees);

        Vector3 launchDirection = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0.0f);

        var sentProjectile = GameObject.Instantiate(projectile, spawnPosition, rotation);
        sentProjectile.transform.rotation = rotation;
        sentProjectile.GetComponent<Rigidbody2D>().AddForce(launchDirection * projectileSpeed, ForceMode2D.Impulse);

        if (sentProjectile.TryGetComponent(out ArrowProjectile arrowProjectile))
        {
            arrowProjectile.damage = projectileDamage;
        }
    }

    private void CreateProjectile(GameObject projectile, Vector3 spawnPosition, Quaternion rotation)
    {
        var sentProjectile = GameObject.Instantiate(projectile, spawnPosition, rotation);
        sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * projectileSpeed, ForceMode2D.Impulse);

        if (sentProjectile.TryGetComponent(out ArrowProjectile arrowProjectile))
        {
            arrowProjectile.damage = projectileDamage;
        }
    }

    public void SuperShoot()
    {
        if (!isReloading)
        {
            OnAbilityAction?.Invoke();
            var sentProjectile = GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
            sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * superProjectileSpeed, ForceMode2D.Impulse);
            isReloading = true;
            superShootsCount--;
            StartCoroutine(Reload());

            OnBallistaSuperShot?.Invoke();
        }
    }

    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));

    }

    public void ActivatePower()
    {
        if (playerScript.activeGun == Player.Weapon.Crossbow)
        {
            isSuperPowerActivated = true;
            spriteRenderer.sprite = superPowerSprite;
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private IEnumerator Cooldown()
    {
        spriteRenderer.sprite = defaultSprite;

        yield return new WaitForSeconds(coolDownTime);

        superShootsCount = 3;
    }

    public float GetAbilityCooldown()
    {
        return coolDownTime;
    }

    public void SetDamage(float newDamage)
    {
        projectileDamage = newDamage;
    }

    public void HandleUpgrading(float newDamage, int newLevel)
    {
        currentDamageLevel = newLevel;
        projectileDamage = newDamage;
    }

    public void UpgradeDamageLevel()
    {
        currentDamageLevel++;
        projectileDamage += projectileDamage * 0.25F * currentDamageLevel;
    }

    public int GetCurrentDamageLevel()
    {
        return currentDamageLevel;
    }

    public float GetCurrentDamage()
    {
        return projectileDamage;
    }
}