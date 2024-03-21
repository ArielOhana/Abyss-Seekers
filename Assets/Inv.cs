using Assets;
using TMPro;
using UnityEngine;
using Context;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Threading;

public class Inv : MonoBehaviour
{
    private SQLdb DBManager;

    public Text coinsText;
    public Text xpText;
    public Text roleText;
    public Text levelText;
    public Text nameText;
    public Image playerSpriteRenderer;
    public Text damageText;
    public Text armourText;
    public Text maxHealthText;
    public Text healthRegenText;
    public Text movementSpeedText;
    public Text evadeRateText;
    public Text hitRateText;
    public Text criticalChanceText;
    public Text armourPenetrationText;


    [SerializeField] Button ButtonOne;
    [SerializeField] Button ButtonTwo;
    [SerializeField] Button ButtonThree;
    [SerializeField] Button ButtonFour;
    [SerializeField] Button ButtonFive;
    [SerializeField] Button ButtonSix;
    [SerializeField] Button ButtonSeven;
    [SerializeField] Button ButtonEight;
    [SerializeField] Button ButtonNine;
    [SerializeField] Button ButtonTen;
    [SerializeField] Button ButtonEleven;
    [SerializeField] Button ButtonTwelve;
    [SerializeField] Button CurrentWeapon;
    [SerializeField] Button CurrentHelmet;
    [SerializeField] Button CurrentBodyArmor;
    [SerializeField] Button CurrentBoots;
    Button[] ButtonsArray;
    private int displaynumber;
    private void Start()
    {
        ButtonsArray = new Button[] { ButtonOne, ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine, ButtonTen, ButtonEleven, ButtonTwelve };
        DBManager = new SQLdb();

        if (MainMenu.currentHero != null)
        {
            GenerateTextFields();
            switch (MainMenu.currentHero.Role.ToLower())
            {
                case "warrior":
                    SetImageFromImage(playerSpriteRenderer, "Assets/AssetsImage/Knight.png");
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
        for (int i = 0; i < ButtonsArray.Length; i++)
        {
            if (ButtonsArray[i] != null)
            {
                ButtonsArray[i].gameObject.SetActive(false);
            }
        }
        SetImageInButton(CurrentWeapon, MainMenu.currentHero.Inventory.CurrentWeapon.Url);
        SetImageInButton(CurrentHelmet, MainMenu.currentHero.Inventory.CurrentHelmet.Url);
        SetImageInButton(CurrentBodyArmor, MainMenu.currentHero.Inventory.CurrentBodyarmour.Url);
        SetImageInButton(CurrentBoots, MainMenu.currentHero.Inventory.CurrentBoot.Url);
    }
    private void GenerateTextFields()
    {

        coinsText.text = $"Coins: {MainMenu.currentHero.Inventory.Coins}";
        xpText.text = $"XP: {MainMenu.currentHero.Xp}";
        roleText.text = $"Role: {MainMenu.currentHero.Role}";
        levelText.text = $"Level: {MainMenu.currentHero.Level}";
        nameText.text = $"Name: {MainMenu.currentHero.Name}";
        damageText.text = $"Damage: {MainMenu.currentHero.Stats.Dmg + MainMenu.currentHero.Inventory.CurrentWeapon.Damage}";
        armourText.text = $"Armour: {MainMenu.currentHero.Stats.Armour + MainMenu.currentHero.Inventory.SumAdditionalArmour()}";
        maxHealthText.text = $"Max Health: {MainMenu.currentHero.Stats.MaxHealth}";
        healthRegenText.text = $"HP Reg: {MainMenu.currentHero.Stats.HealthRegeneration}";
        movementSpeedText.text = $"Movement Speed: {MainMenu.currentHero.Stats.MovementSpeed}";
        evadeRateText.text = $"Evade Rate: {MainMenu.currentHero.Stats.EvadeRate}";
        hitRateText.text = $"Hit Rate: {MainMenu.currentHero.Stats.HitRate}";
        criticalChanceText.text = $"Critical Chance: {MainMenu.currentHero.Stats.CriticalChance + MainMenu.currentHero.Inventory.CurrentWeapon.CriticalDamage}";
        armourPenetrationText.text = $"Armour Penetration: {MainMenu.currentHero.Stats.ArmourPenetration}";

    }
    public void ClickedOnItem(int ItemNumber) //Displaynumber 0 = helmets, 1 = bodyarmor, 2 = boots, 3 = weapons
    {
        Inventory currentInv = MainMenu.currentHero.Inventory;
        if (displaynumber == 0)
        {
            MainMenu.currentHero.Inventory.CurrentHelmet = currentInv.Helmets[ItemNumber];
            SetImageInButton(CurrentHelmet, currentInv.Helmets[ItemNumber].Url);
        }
        if (displaynumber == 1)
        {
            MainMenu.currentHero.Inventory.CurrentBodyarmour = currentInv.Bodyarmours[ItemNumber];
            SetImageInButton(CurrentBodyArmor, currentInv.Bodyarmours[ItemNumber].Url);
        }
        if (displaynumber == 2)
        {
            MainMenu.currentHero.Inventory.CurrentBoot = currentInv.Boots[ItemNumber];
            SetImageInButton(CurrentBoots, currentInv.Boots[ItemNumber].Url);
        }
        if (displaynumber == 3)
        {
            MainMenu.currentHero.Inventory.CurrentWeapon = currentInv.Weapons[ItemNumber];
            SetImageInButton(CurrentWeapon, currentInv.Weapons[ItemNumber].Url);
        }
        GenerateTextFields();
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
    public void DisplayInventoryType(int displaynumber) //Displaynumber 0 = helmets, 1 = bodyarmor, 2 = boots, 3 = weapons
    {
        Inventory currentInv = MainMenu.currentHero.Inventory;
        if (currentInv != null)
        {
            Debug.Log(currentInv.Helmets[0].Name);
            this.displaynumber = displaynumber;
            if (displaynumber == 0)
                for (int i = 0; i < ButtonsArray.Length; i++)
                {
                    if (i < currentInv.Helmets.Count)
                        SetImageInButton(ButtonsArray[i], currentInv.Helmets[i].Url);
                    else
                    {
                        ButtonsArray[i].gameObject.SetActive(false);
                    }
                }
            if (displaynumber == 1)
                for (int i = 0; i < ButtonsArray.Length; i++)
                {
                    if (i < currentInv.Bodyarmours.Count)
                        SetImageInButton(ButtonsArray[i], currentInv.Bodyarmours[i].Url);
                    else
                    {
                        ButtonsArray[i].gameObject.SetActive(false);
                    }
                }
            if (displaynumber == 2)
                for (int i = 0; i < ButtonsArray.Length; i++)
                {
                    if (i < currentInv.Boots.Count)
                        SetImageInButton(ButtonsArray[i], currentInv.Boots[i].Url);
                    else
                    {
                        ButtonsArray[i].gameObject.SetActive(false);
                    }
                }
            if (displaynumber == 3)
                for (int i = 0; i < ButtonsArray.Length; i++)
                {
                    if (i < currentInv.Weapons.Count)
                        SetImageInButton(ButtonsArray[i], currentInv.Weapons[i].Url);
                    else
                    {
                        ButtonsArray[i].gameObject.SetActive(false);
                    }

                }
        }
    }
    public void SaveGame()
    {
        Hero newHero = MainMenu.currentHero;
        DBManager.SaveHero(newHero);
        SceneManager.LoadScene("townhall");
    }
    public void SetImageInButton(Button button, string URL)
    {

        if (button != null)
        {
            bool ThereIsAnImage = false;
            // Access the Image component of the Button
            Image buttonImage = button.GetComponent<Image>();

            if (buttonImage != null)
            {
                // Call the SetImageFromImage method and assign the result to the button's Image component
                ThereIsAnImage = SetImageFromImage(buttonImage, URL, ThereIsAnImage);
            }
            else
            {
                Debug.LogError("The clicked button does not have an Image component.");
            }
            button.gameObject.SetActive(ThereIsAnImage);
        }
        else
        {
            Debug.LogError("The clicked GameObject does not have a Button component.");
        }
    }

    private bool SetImageFromImage(Image imageComponent, string imagePath, bool ThereIsAnImage)
    {
        if (File.Exists(imagePath))
        {
            // Load the image file as a byte array
            byte[] imageData = File.ReadAllBytes(imagePath);

            // Create a texture and load the image data
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            // Create a Sprite from the loaded texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Apply the sprite directly to the Image component
            imageComponent.sprite = sprite;
            ThereIsAnImage = true;
        }
        else
        {
            Debug.LogError("Image file not found at path: " + imagePath);
        }
        return ThereIsAnImage;
    }
    private void Update()
    {
    }
}
