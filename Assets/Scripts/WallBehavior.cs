using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WallBehavior : MonoBehaviour
{
    public SkillManager skillManager;
    public Collision2D collision;
    [SerializeField] public float _health;
    [SerializeField] public float time;
    [SerializeField] public float maxhealth;
    private Renderer _renderer;
    private int _blinkCount = 5;
    public BoxCollider2D ignoredCollider;
    [SerializeField] private GameObject myGameObject;
    [SerializeField] private GameManager gameManager;

    private DateTime PlayTimeMetric;

    [SerializeField] private Slider healthBar;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        _health = maxhealth;
        ignoredCollider = myGameObject.GetComponentInChildren<IgnoredCollider>().GetComponent<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthBar.maxValue = _health;
        healthBar.value = _health;
        _renderer = GetComponent<Renderer>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, ignoredCollider.gameObject.layer, true);

        PlayTimeMetric = DateTime.Now;
    }

    void Update()
    {
        _health += time * Time.deltaTime;
        if (_health > maxhealth)
        {
            _health = maxhealth;
        }
        healthBar.value = _health;

        if (_health < 0 && isAlive)
        {
            Die();
        }
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;
        healthBar.value = _health;
        StartCoroutine(BlinkCoroutine());
    }

    private void Die()
    {
        isAlive = false;

        TimeSpan secondsInGame = PlayTimeMetric - DateTime.Now;
        Dictionary<string, object> parameters = new Dictionary<string, object>() { { "time_seconds", secondsInGame.TotalSeconds } };
        AppMetrica.Instance.ReportEvent("one_game_play_time", parameters);
        AppMetrica.Instance.SendEventsBuffer();

        Time.timeScale = 0;
        gameManager.GameOver();
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            _renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            _renderer.material.color = Color.gray;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
