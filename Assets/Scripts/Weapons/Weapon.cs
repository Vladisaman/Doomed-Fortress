using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
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

    private void Start()
    {
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
}
