using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public Slider FreezeBar;
    public int FreezeAmount;
    public bool isFrozen;
    public Transform playerTransform { get; private set; }
    public Player playerScript { get; private set; }
    public GameObject crosshair;

    [SerializeField] public GameObject projectile;

    [SerializeField] public Transform projectileSpawnerTransform;
    [SerializeField] private List<SkillButton> skill;

    public abstract void Aim();

    public abstract void Shoot();

    public const float weaponRotationClamp = 60f;

    [SerializeField] protected GameObject abilityButtonUI;

    [Range(1, 2)]
    [SerializeField] protected float buttonScale;
    [SerializeField] public int currentDamageLevel;
    [SerializeField] public float projectileDamage;


    private void Start()
    {
        FreezeAmount = 0;
        FreezeBar.value = FreezeAmount;
        playerTransform = GameObject.Find("Player").transform;
        playerScript = playerTransform.GetComponent<Player>();
        crosshair = GameObject.Find("Crosshair");
        Crosshair();
        CrosshairHide();

        projectileSpawnerTransform = gameObject.transform.GetChild(0);
    }

    public void CrosshairUnHide()
    {
        crosshair.GetComponent<Renderer>().enabled = true;
    }

    public void CrosshairHide()
    {
        crosshair.GetComponent<Renderer>().enabled = false;
    }

    public void Crosshair()
    {
        crosshair.transform.position = new Vector3(Mathf.Clamp(Utils.GetMouseWorldPosition().x, -4.84f, 10f), Mathf.Clamp(Utils.GetMouseWorldPosition().y, -4.5f, 4.5f), 0);
    }
    
    public void UpgradeDamageLevel()
    {
        currentDamageLevel++;
        projectileDamage += 0.5F * currentDamageLevel;
    }

    public void Freeze()
    {
        if (!isFrozen)
        {
            FreezeAmount += 1;
            FreezeBar.value = FreezeAmount;

            if (FreezeAmount >= 3)
            {
                StopCoroutine(FrostTimer());
                StartCoroutine(FrozenStun());
            } 
            else
            {
                StartCoroutine(FrostTimer());
            }
        }
    }

    private IEnumerator FrostTimer()
    {
        yield return new WaitForSeconds(50.0f);
        FreezeAmount -= 1;
        FreezeBar.value = FreezeAmount;
    }

    public IEnumerator FrozenStun()
    {
        isFrozen = true;
        GetComponent<SpriteRenderer>().color = Color.blue;

        yield return new WaitForSeconds(1.5F);

        FreezeAmount = 0;
        FreezeBar.value = FreezeAmount;
        isFrozen = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}