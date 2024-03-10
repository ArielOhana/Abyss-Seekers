using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WonStats : MonoBehaviour
{
    public Text xpText;
    public Text coinsText;
    public Text damageText;
    public Text MovementSpeedText;
    public Text EvdateRateText;
    public Text HealthRegText;
    public Text HitRateText;
    public Text CriticalChanText;
    public Text ArmourPenetText;
    public Text maxHealthTextText;
    public Text choiceLeftText;

    private int XP = 12;
    private int Coins = 12;
    private bool isLeveledUp = true;
    private int damage = 22;
    private int evadeRate = 30;
    private int movementSpeed = 3;
    private int healthReg = 22;
    private int hitRate = 30;
    private int critcalChange = 44;
    private int armourPent = 22;
    private int maxHealth = 32;
    private int choiceLeft = 3;
    private int clickCount = 0;
    public GameObject ContinueButton;

    void Start()
    {
        UpdateUI();

        Button damageButton = GameObject.Find("DamageButton").GetComponent<Button>();
        damageButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementDamage); });

        Button maxHealthButton = GameObject.Find("MaxHealthButton").GetComponent<Button>();
        maxHealthButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementMaxHealth); });

        Button moveSpeedButton = GameObject.Find("MovementSpeedButton").GetComponent<Button>();
        moveSpeedButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementMovementSpeed); });

        Button evadeRateButton = GameObject.Find("EvadeRateButton").GetComponent<Button>();
        evadeRateButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementEvadeRate); });

        Button healthRegButton = GameObject.Find("HealthRegButton").GetComponent<Button>();
        healthRegButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementHealthReg); });

        Button hitRateButton = GameObject.Find("HitRateButton").GetComponent<Button>();
        hitRateButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementHitRate); });

        Button criticalChanButton = GameObject.Find("CriticalChanButton").GetComponent<Button>();
        criticalChanButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementCriticalChan); });

        Button armourPenetButton = GameObject.Find("ArmourPenetButton").GetComponent<Button>();
        armourPenetButton.onClick.AddListener(() => { IncrementAndDecrement(IncrementArmourPenet); });

    }

    void Update()
    {
       
    }

    public void IncrementXP()
    {
        if (clickCount < 3)
        {
            XP++;
            UpdateUI();
        }
    }

    public void IncrementCoins()
    {
        if (clickCount < 3)
        {
            Coins++;
            UpdateUI();
        }
    }

    public void IncrementDamage()
    {
        if (clickCount < 3)
        {
            damage++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementMaxHealth()
    {
        if (clickCount < 3)
        {
            maxHealth++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementMovementSpeed()
    {
        if (clickCount < 3)
        {
            movementSpeed++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementEvadeRate()
    {
        if (clickCount < 3)
        {
            evadeRate++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementHealthReg()
    {
        if (clickCount < 3)
        {
            healthReg++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementHitRate()
    {
        if (clickCount < 3)
        {
            hitRate++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementCriticalChan()
    {
        if (clickCount < 3)
        {
            critcalChange++;
            clickCount++;
            UpdateUI();
        }
    }

    public void IncrementArmourPenet()
    {
        if (clickCount < 3)
        {
            armourPent++;
            clickCount++;
            UpdateUI();
        }
    }

    private void IncrementAndDecrement(UnityAction incrementMethod)
    {
        incrementMethod.Invoke(); 
        DecrementChoicePoints(); 
    }

    public void DecrementChoicePoints()
    {
        if (clickCount <= 3 && choiceLeft >= 0)
        {
            choiceLeft--;
            UpdateUI();
        }
    }


    void UpdateUI()
    {
        xpText.text = "XP: " + XP.ToString();
        coinsText.text = "Coins: " + Coins.ToString();
        damageText.text = "Damage: " + damage.ToString();
        MovementSpeedText.text = "Movement Speed: " + movementSpeed.ToString();
        EvdateRateText.text = "Evade Rate: " + evadeRate.ToString();
        HealthRegText.text = "Health Regeneration: " + healthReg.ToString();
        HitRateText.text = "Hit Rate: " + hitRate.ToString();
        CriticalChanText.text = "Critical Chance: " + critcalChange.ToString();
        ArmourPenetText.text = "Armour Penetration: " + armourPent.ToString();
        maxHealthTextText.text = "Max Health: " + maxHealth.ToString();
        choiceLeftText.text = "Points Remaining " + choiceLeft.ToString() ;
        if (clickCount >= 3)
        {
            ContinueButton.SetActive(true);
        }
        else { ContinueButton.SetActive(false);}
    }
}
