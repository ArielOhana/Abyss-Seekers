using Assets;
using TMPro;
using UnityEngine;
using Context;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.IO;

public class Inventory : MonoBehaviour
{
    private SQLdb DBManager;

    public TMP_Text coinsText;
    public TMP_Text xpText;
    public TMP_Text roleText;
    public TMP_Text levelText;
    public TMP_Text nameText;
    public Image playerSpriteRenderer ;
    public TMP_Text damageText;
    public TMP_Text armourText;
    public TMP_Text maxHealthText;
    public TMP_Text healthRegenText;
    public TMP_Text movementSpeedText;
    public TMP_Text evadeRateText;
    public TMP_Text hitRateText;
    public TMP_Text criticalChanceText;
    public TMP_Text armourPenetrationText;

    private void Start()
    {
        DBManager = new SQLdb();

        if (MainMenu.currentHero != null)
        {
            coinsText.text = $"Coins: {MainMenu.currentHero.Inventory.Coins}";
            xpText.text = $"XP: {MainMenu.currentHero.Xp}";
            roleText.text = $"Role: {MainMenu.currentHero.Role}";
            levelText.text = $"Level: {MainMenu.currentHero.Level}";
            nameText.text = $"Name: {MainMenu.currentHero.Name}";

            damageText.text = $"Damage: {MainMenu.currentHero.Stats.Dmg}";
            armourText.text = $"Armour: {MainMenu.currentHero.Stats.Armour}";
            maxHealthText.text = $"Max Health: {MainMenu.currentHero.Stats.MaxHealth}";
            healthRegenText.text = $"Health Regeneration: {MainMenu.currentHero.Stats.HealthRegeneration}";
            movementSpeedText.text = $"Movement Speed: {MainMenu.currentHero.Stats.MovementSpeed}";
            evadeRateText.text = $"Evade Rate: {MainMenu.currentHero.Stats.EvadeRate}";
            hitRateText.text = $"Hit Rate: {MainMenu.currentHero.Stats.HitRate}";
            criticalChanceText.text = $"Critical Chance: {MainMenu.currentHero.Stats.CriticalChance}";
            armourPenetrationText.text = $"Armour Penetration: {MainMenu.currentHero.Stats.ArmourPenetration}";

            switch (MainMenu.currentHero.Role.ToLower())
            {
                case "warrior":
                    SetImageFromImage(playerSpriteRenderer,"Assets/AssetsImage/Knight.png");
                    break;
                case "rogue":
                    SetImageFromImage(playerSpriteRenderer, "Assets/ImportedAssets/Backgrounds/Gothicvania Cemetery Artwork/Sprites/hero/hero-idle/hero-idle-4.png");
                    break;
                case "mage":
                    SetImageFromImage(playerSpriteRenderer, "Assets/ImportedAssets/Backgrounds/GothicVania Church/Sprites/Enemies/Wizard-Idle/wizard-idle-1.png");
                    break;
                case "elf":
                    SetImageFromImage(playerSpriteRenderer, "Assets/ImportedAssets/Backgrounds/Gothicvania Swamp Artwork/Sprites/Player/stand/stand.png");
                    break;
                case "orc":
                    SetImageFromImage(playerSpriteRenderer, "Assets/ImportedAssets/Backgrounds/GothicVania Church/Sprites/Player/kick1.png");
                    break;
                default:
                    Debug.LogWarning($"Unhandled role: {MainMenu.currentHero.Role}");
                    break;
            }
        }
        else
        {
            Debug.LogWarning($"Hero with nam not found in the database.");
        }
    }
    private void SetImageFromImage(Image imageComponent, string imagePath)
    {
        if (File.Exists(imagePath))
        {
            byte[] imageData = File.ReadAllBytes(imagePath);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogError("Image file not found at path: " + imagePath);
        }
    }
}
