using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    //public Image healthBar;
    //bool TakingDamage = false;
    //bool Healing = false;

    //void Start()
    //{
    //}

    //public void TakeDamage(Unit attacker, Unit receiver)
    //{
    //    receiver.CurrentHealth -= attacker.damage;
    //    receiver.CurrentHealth = Mathf.Clamp(receiver.CurrentHealth, 0, 100);
    //    UpdateHealthBar(receiver.CurrentHealth);
    //}

    //public void Heal(Unit unit)
    //{
    //    unit.CurrentHealth += unit.healthRegeneration;
    //    unit.CurrentHealth = Mathf.Clamp(unit.CurrentHealth, 0, 100);
    //    UpdateHealthBar(unit.CurrentHealth);
    //}

    //void UpdateHealthBar(float currentHealth)
    //{
    //    healthBar.fillAmount = currentHealth / 100f;

    //    if (currentHealth <= 0)
    //    {
    //        Application.LoadLevel(Application.loadedLevel);
    //    }
    //}
    //void Update()
    //{
    //    if (TakingDamage)
    //    {
    //        TakeDamage(damage);
    //    }
    //    if (Healing)
    //    {
    //        Heal(health)
    //    }
    //}
}
