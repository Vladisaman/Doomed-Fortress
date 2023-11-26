using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WallBehavior : MonoBehaviour
{
    [SerializeField] private float _health;
    private Renderer _renderer;
    private int _blinkCount = 5;
    public BoxCollider2D ignoredCollider;
    [SerializeField] private GameObject myGameObject;
    [SerializeField] private GameManager gameManager;

    private DateTime PlayTimeMetric;

    [SerializeField] private Slider healthBar;

    private UI ui;

    // Start is called before the first frame update
    void Start()
    {
        ignoredCollider = myGameObject.GetComponentInChildren<IgnoredCollider>().GetComponent<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthBar.maxValue = _health;
        healthBar.value = _health;
        _renderer = GetComponent<Renderer>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, ignoredCollider.gameObject.layer, true);

        PlayTimeMetric = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
       if(_health < 0)
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
        TimeSpan secondsInGame = PlayTimeMetric - DateTime.Now;
        Dictionary<string, object> parameters = new Dictionary<string, object>() { { "time_seconds", secondsInGame.TotalSeconds } };
        AppMetrica.Instance.ReportEvent("one_game_play_time", parameters);
        AppMetrica.Instance.SendEventsBuffer();

        Time.timeScale = 0.0f;
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
