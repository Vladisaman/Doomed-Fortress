using System;
using UnityEngine;

public class FireGun : Weapon
{
    public Action OnAbilityAction;

    public Action OnFireGunStartShooting;
    public Action OnFireGunStopShooting;

    public SkillManager skillsManager;

    [SerializeField] ParticleSystem fireParticle;
    [SerializeField] ParticleSystem leftFireParticle;
    [SerializeField] ParticleSystem rightFireParticle;
    [SerializeField] ParticleSystem BlackFireParticle;
    [SerializeField] PolygonCollider2D fireCollider;
    [SerializeField] PolygonCollider2D leftfireCollider;
    [SerializeField] PolygonCollider2D rightfireCollider;
    [SerializeField] PolygonCollider2D BlackFireCollider;
    [SerializeField] BoxCollider2D burn;
    private string NAME_OF_WEAPON = "FIREGUN";

    [Header("----------DOT PROPERTIES----------")]
    [SerializeField] public float dotDamage;
    [SerializeField] public int dotTicks;
    [SerializeField] public float dotDelay;

    [Header("----------ULT PROPERTIES----------")]
    [SerializeField] private float percentOfSmallEnemies;
    [SerializeField] private float percentOfBigEnemies;
    [SerializeField] private float abilityCooldown;
    [SerializeField] private FiregunAbilityButton abilityButton;
    [SerializeField] private GameObject explosionPrefab;

    [Header("----------UPGRADING----------")]
    [SerializeField] private int currentDamageLevel;
    [SerializeField] public float damage;

    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }

    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.FireGun) {
            abilityButtonUI.transform.localScale = new Vector3(buttonScale, buttonScale, 1);
            if (Application.isMobilePlatform)
            {
                if (Utils.GetTouchedObject() == null || !Utils.GetTouchedObject().CompareTag("Weapon"))
                {
                    Aim();
                    if (Input.GetMouseButtonDown(0))
                    {
                        Shoot();
                    }
                }
            } 
            else if (Input.GetMouseButton(1)) {
                Aim();
                if (Input.GetMouseButtonDown(0)) {
                    Shoot();
                }
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
                StopShoot();
            }
        } 
        else {
            abilityButtonUI.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (playerScript.activeGun != Player.Weapon.FireGun) {
            StopShoot();
        }
    }

    public override void Shoot()
    {
        if (skillsManager.fireGunleftFireEnable)
        {
            leftFireParticle.Play();
            leftfireCollider.enabled = true;
        }
        if(skillsManager.fireGunrightFireEnable)
        {
            rightFireParticle.Play();
            rightfireCollider.enabled = true;
        }
        if (skillsManager.BlackFire)
        {
            BlackFireParticle.Play();
            fireParticle.Stop();
            fireCollider.enabled = true;
            BlackFireCollider.enabled = true;
            burn.enabled = true;
        }
        else
        {
            fireParticle.Play();
            fireCollider.enabled = true;
        }
        OnFireGunStartShooting?.Invoke();

    }

    public void StopShoot()
    {
        if (skillsManager.fireGunleftFireEnable)
        {
            leftFireParticle.Stop();
            leftfireCollider.enabled = false;
        }
        if (skillsManager.fireGunrightFireEnable)
        {
            rightFireParticle.Stop();
            rightfireCollider.enabled = false;
        }
        if (skillsManager.BlackFire)
        {
            BlackFireParticle.Stop();
            BlackFireCollider.enabled = false;
            burn.enabled = false;
        }

        fireParticle.Stop();
        fireCollider.enabled = false;

        OnFireGunStopShooting?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            if (collision.tag == "Enemy With Shield") {
                if (collision.GetComponent<ShieldEnemy>().isShieldAlive == false) {
                    collision.GetComponent<ShieldEnemy>().TakeDamage(damage * Time.fixedDeltaTime, NAME_OF_WEAPON);

                    if (!collision.GetComponent<FireDot>()) {
                        collision.gameObject.AddComponent<FireDot>();
                    }
                }
            } else {
                collision.GetComponent<Enemy>().TakeDamage(damage * Time.fixedDeltaTime, NAME_OF_WEAPON);

                if (!collision.GetComponent<FireDot>()) {
                    collision.gameObject.AddComponent<FireDot>();
                }
            }
        }
    }

    public float GetAbilityCooldown()
    {
        return abilityCooldown;
    }

    public void FireGunAbility()
    {
        if (playerScript.activeGun == Player.Weapon.FireGun) {

            OnAbilityAction?.Invoke();
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies) {
                if (enemy.GetComponent<ShieldEnemy>() != null || enemy.GetComponent<StoneEnemy>() != null) {
                    float onePercentHealth = enemy.maxHealth / 100;
                    enemy.health -= onePercentHealth * percentOfBigEnemies;
                } else {
                    float onePercentHealth = enemy.maxHealth / 100;
                    enemy.health -= onePercentHealth * percentOfSmallEnemies;
                }
            }

            Vector3 explosionPosition = new Vector3(2.5f, 0.25f, 0f);
            Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        }
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void HandleUpgrading(float newDamage, int newLevel)
    {
        currentDamageLevel = newLevel;
        damage = newDamage;
        dotDamage = damage * 0.05F;
    }

    public void UpgradeDamageLevel()
    {
        currentDamageLevel++;
        damage += damage * 0.25F * currentDamageLevel;
        dotDamage = damage * 0.05F;
    }

    public int GetCurrentDamageLevel()
    {
        return currentDamageLevel;
    }

    public float GetCurrentDamage()
    {
        return damage;
    }

    public float GetCurrentDotDamage()
    {
        return dotDamage;
    }
}
