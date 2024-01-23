using System.Collections;
using UnityEngine;
using System;

public class Mortar : Weapon
    {
        bool isReloading = false;

        public Action OnAbilityAction;

        public Action OnMortarShot;
        public SkillManager skillsManager;
        [HideInInspector] public Vector3 target;

        [Space]
        [Header("----------PROPERTIES----------")]
        [SerializeField] public float projectileSpeed = 10f;
        [SerializeField] float reloadTime = 2f;
        [SerializeField] public int blessing;

        [SerializeField] GameObject superProjectile;
        [SerializeField] GameObject smallProjectile;
        [SerializeField] GameObject BigProjectile;

        [SerializeField] float coolDownTime;
        [SerializeField] int superShootsCount = 3;
        [SerializeField] public float superProjectileSpeed = 10f;
        [SerializeField] private MortarAbilityButton abilityButton;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite defaultSprite;
        [SerializeField] Sprite ReloadingSprite;

        [Header("----------UPGRADING----------")]
        [SerializeField] private int currentDamageLevel;
        [SerializeField] public float projectileDamage;


        bool isSuperPowerActivated = false;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Aim()
        {
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
        }

    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.Mortar)
        {
            abilityButtonUI.transform.localScale = new Vector3(buttonScale, buttonScale, 1);

            if (Application.isMobilePlatform)
            {
                if (Utils.GetTouchedObject() == null || !Utils.GetTouchedObject().CompareTag("Weapon"))
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
            }
            else if (Input.GetMouseButton(1))
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
            if (Input.GetMouseButtonUp(1))
            {
                CrosshairHide();
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

    public void ActivatePower()
    {
        isSuperPowerActivated = true;
    }

    public override void Shoot()
    {
        if (isReloading)
        {
            return;
        }
        else if (skillsManager.bigYadroEnable)
        {
            GameObject.Instantiate(BigProjectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;
        }
        else if (skillsManager.smallYadroEnable)
        {
            GameObject.Instantiate(smallProjectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;
        }
        else
        {
            GameObject.Instantiate(projectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;
        }
        

        OnMortarShot?.Invoke();

        spriteRenderer.sprite = ReloadingSprite;
        StartCoroutine(Reload());
    }

        public void SuperShoot()
        {
            if (!isReloading)
            {
                OnAbilityAction?.Invoke();
                GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
                isReloading = true;
                superShootsCount--;
                StartCoroutine(Reload());

                OnMortarShot?.Invoke();
            }
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadTime);
            spriteRenderer.sprite = defaultSprite;
            isReloading = false;
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(coolDownTime);

            superShootsCount = 3;
        }

        public float GetAbilityCooldown()
        {
            return coolDownTime;
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
