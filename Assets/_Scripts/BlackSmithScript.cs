using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Context;
namespace Assets
{
    public class BlackSmithScript : MonoBehaviour
    {
        [SerializeField] Button ButtonOne;
        [SerializeField] Button ButtonTwo;
        [SerializeField] Button ButtonThree;
        [SerializeField] Button ButtonFour;
        [SerializeField] Button ButtonFive;
        [SerializeField] Button ButtonSix;
        [SerializeField] Button ButtonSeven;
        [SerializeField] Button ButtonEight;
        [SerializeField] Button ButtonNine;
        [SerializeField] Button PreviewImage;
        [SerializeField] Text NameText;
        [SerializeField] Text RarityText;
        [SerializeField] Text CriticalDamageText;
        [SerializeField] Text OnlySetActiveNameText;
        [SerializeField] Text OnlySetActiveRarityText;
        [SerializeField] Text OnlySetActiveCriticalDamageText;
        [SerializeField] Text PriceText;
        [SerializeField] Text CurrentMoneyText;
        [SerializeField] Text OnlySetActivePriceText;
        [SerializeField] Text OnlySetActiveCurrentMoneyText;
        [SerializeField] Text NotificationsAboutBuy;
        [SerializeField] Text BuyText;
        [SerializeField] Button BuyButton;
        [SerializeField] Text OnlySetActiveRangeText;
        [SerializeField] Text OnlySetActiveDamageText;
        [SerializeField] Text DamageText;
        [SerializeField] Text RangeText;

        private AllItems allItems;
        private int displaynumber;
        private int ItemSelected;
        Button[] ButtonsArray;
        void Start()
        {
            SQLdb DBManager = new SQLdb();
            DBManager.ReadJson();
            allItems = BackendFunctions.GetAllItems();
            ButtonsArray = new Button[] { ButtonOne, ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine };

            
            for (int i = 0; i < ButtonsArray.Length; i++)
            {
                if (ButtonsArray[i] != null)
                {
                    ButtonsArray[i].gameObject.SetActive(false);
                }
            }

            PreviewImage.gameObject.SetActive(false);
            OnlySetActiveCriticalDamageText.gameObject.SetActive(false);
            OnlySetActiveNameText.gameObject.SetActive(false);
            OnlySetActiveRarityText.gameObject.SetActive(false);
            OnlySetActiveCurrentMoneyText.gameObject.SetActive(false);
            OnlySetActivePriceText.gameObject.SetActive(false);
            BuyText.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(false);
            OnlySetActiveRangeText.gameObject.SetActive(false);
            OnlySetActiveDamageText.gameObject.SetActive(false);
            DamageText.gameObject.SetActive(false);
            RangeText.gameObject.SetActive(false);
            CurrentMoneyText.text = MainMenu.currentHero.Inventory.Coins.ToString();
            CurrentMoneyText.gameObject.SetActive(false);
        }
        public Button[] GetButtonsArray()
        {
            return ButtonsArray;
        }
        // Update is called once per frame.
        void Update()
        {

        }
        public void Buy()
        {
          
                        Weapon AttemptToBuy = new Weapon(allItems.AllWeapons[ItemSelected + 9*displaynumber].Name, allItems.AllWeapons[ItemSelected + 9 * displaynumber].Damage, allItems.AllWeapons[ItemSelected + 9 * displaynumber].CriticalDamage, allItems.AllWeapons[ItemSelected + 9 * displaynumber].Range, allItems.AllWeapons[ItemSelected + 9 * displaynumber].Value, allItems.AllWeapons[ItemSelected + 9 * displaynumber].Rarity, allItems.AllWeapons[ItemSelected + 9 * displaynumber].Url);
                        if (AttemptToBuy.Value <= MainMenu.currentHero.Inventory.Coins)
                        {
                            MainMenu.currentHero.Inventory.Coins -= AttemptToBuy.Value;
                            MainMenu.currentHero.Inventory.Weapons.Add(AttemptToBuy);
                            NotificationsAboutBuy.text = "Added new weapon " + AttemptToBuy.Name;
                            CurrentMoneyText.text = MainMenu.currentHero.Inventory.Coins.ToString();

                        }
                        else
                        {
                            NotificationsAboutBuy.text = "You are too broke for " + AttemptToBuy.Name;
                        }
                     
        }
        public void DisplayWeapon(int displaynumber) //Displaynumber 0 = melee, 1 = heavyweapons, 2 = bows
        {
            this.displaynumber = displaynumber;
                for (int i = 0; i < ButtonsArray.Length; i++)
                {
                    if (i + displaynumber * 9 < allItems.AllWeapons.Count)
                        SetImageInButton(ButtonsArray[i], allItems.AllWeapons[i + displaynumber * 9].Url);
                    else
                    {
                        ButtonsArray[i].gameObject.SetActive(false);
                    }
                }

        }
        public void GoToTownHall()
        {
            SceneManager.LoadScene("townhall");
        }
        public void ClickedOnItem(int ItemNumber)
        {
            ItemSelected = ItemNumber;
            OnlySetActiveCriticalDamageText.gameObject.SetActive(true);
            OnlySetActiveNameText.gameObject.SetActive(true);
            OnlySetActiveRarityText.gameObject.SetActive(true);
            OnlySetActiveCurrentMoneyText.gameObject.SetActive(true);
            OnlySetActivePriceText.gameObject.SetActive(true);
            BuyText.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(true);
            OnlySetActiveRangeText.gameObject.SetActive(true);
            OnlySetActiveDamageText.gameObject.SetActive(true);
            DamageText.gameObject.SetActive(true);
            RangeText.gameObject.SetActive(true);
            CurrentMoneyText.gameObject.SetActive(true);


            Weapon selected = allItems.AllWeapons[ItemNumber + displaynumber * 9];
            SetImageInButton(PreviewImage, selected.Url);
            NameText.text = selected.Name;
            CriticalDamageText.text = selected.CriticalDamage.ToString();
            PriceText.text = selected.Value.ToString();
            DamageText.text = selected.Damage.ToString();
            if (selected.Range <= 1)
                RangeText.text = "Close";
            else
            {
                RangeText.text = $"Long ({selected.Range})";
            }
            switch (selected.Rarity)
            {
                case 1:
                    {
                        RarityText.color = Color.gray;
                        RarityText.text = "Common";
                        break;
                    }
                case 2:
                    {
                        RarityText.color = new Color(0.8f, 0.6f, 1f);
                        RarityText.text = "Epic";
                        break;

                    }
                case 3:
                    {

                        RarityText.color = new Color(1.0f, 0.8f, 0.0f); ;
                        RarityText.text = "Legendary";
                        break;
                    }
                default:
                    {
                        RarityText.color = Color.gray;
                        RarityText.text = "Common";
                        break;
                    }


            }

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
    }
}
