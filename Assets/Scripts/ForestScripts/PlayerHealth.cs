using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; 
    public float hitsPerLife = 3; 
    public Image[] hearts;
    public DamageEffect damageEffect; 
    private int currentHealth;
    private float currentHits;
    private bool isPlayingContinuousDamageEffect = false;
    public GameObject gameOverPanel;

    public AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        currentHits = 0;
        UpdateHealthUI();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHits += damage;

        if (currentHits >= hitsPerLife)
        {
            currentHealth -= 1;
            currentHits = 0;
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        damageEffect.TriggerDamageEffect();

        if (currentHealth == 1 && !isPlayingContinuousDamageEffect)
        {
            StartCoroutine(PlayContinuousDamageEffect());
        }
    }

    IEnumerator PlayContinuousDamageEffect()
    {
        isPlayingContinuousDamageEffect = true;

        while (currentHealth == 1)
        {
            damageEffect.TriggerDamageEffect();
            yield return new WaitForSeconds(1f); 
        }

        isPlayingContinuousDamageEffect = false;
    }

void UpdateHealthUI()
{
    for (int i = 0; i < hearts.Length; i++)
    {
        hearts[i].gameObject.SetActive(i < currentHealth);
    }

    int activeHearts = 0;
    for (int i = 0; i < hearts.Length; i++)
    {
        if (hearts[i].gameObject.activeSelf)
        {
            activeHearts++;
        }
    }

    currentHealth = activeHearts;

    if (currentHealth > 1 && isPlayingContinuousDamageEffect)
    {
        StopCoroutine(PlayContinuousDamageEffect());
        isPlayingContinuousDamageEffect = false;
    }
}

    public void RecoverHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealthUI();
            audioSource.Play();
        }
    }




    void Die()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
