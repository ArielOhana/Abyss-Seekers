using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using static UnityEditor.Progress;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
namespace Assets
{
    public class GetAllButtons : MonoBehaviour
    {
    //    [SerializeField] Button ButtonOne;
    //    [SerializeField] Button ButtonTwo;
    //    [SerializeField] Button ButtonThree;
    //    [SerializeField] Button ButtonFour;
    //    [SerializeField] Button ButtonFive;
    //    [SerializeField] Button ButtonSix;
    //    [SerializeField] Button ButtonSeven;
    //    [SerializeField] Button ButtonEight;
    //    [SerializeField] Button ButtonNine;
    //    [SerializeField] Button PreviewImage;
    //    [SerializeField] Text NameText;
    //    [SerializeField] Text RarityText;
    //    [SerializeField] Text ArmorText;
    //    [SerializeField] Text OnlySetActiveNameText;
    //    [SerializeField] Text OnlySetActiveRarityText;
    //    [SerializeField] Text OnlySetActiveArmorText;
    //    [SerializeField] Text PriceText;
    //    [SerializeField] Text CurrentMoneyText;
    //    [SerializeField] Text OnlySetActivePriceText;
    //    [SerializeField] Text OnlySetActiveCurrentMoneyText;
    //    [SerializeField] Text NotificationsAboutBuy;
    //    [SerializeField] Text BuyText;
    //    [SerializeField] Button BuyButton;

    //    private AllItems allItems;
    //    private int displaynumber;
    //    private int ItemSelected;
    //    Button[] ButtonsArray;
    //    void Start()
    //    {
    //        SQLdb DBManager = new SQLdb();  // Create an instance of the SQLdb class
    //        DBManager.ReadJson();
    //        allItems = DBManager.GetAllItems();
    //        ButtonsArray = new Button[] { ButtonOne, ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine };
    //        for (int i = 0; i < ButtonsArray.Length; i++)
    //        {
    //            ButtonsArray[i].gameObject.SetActive(false);
    //        }
    //        PreviewImage.gameObject.SetActive(false);
    //        OnlySetActiveArmorText.gameObject.SetActive(false);
    //        OnlySetActiveNameText.gameObject.SetActive(false);
    //        OnlySetActiveRarityText.gameObject.SetActive(false);
    //        OnlySetActiveCurrentMoneyText.gameObject.SetActive(false);
    //        OnlySetActivePriceText.gameObject.SetActive(false);
    //        BuyText.gameObject.SetActive(false);
    //        BuyButton.gameObject.SetActive(false);
    //    }
    //    public Button[] GetButtonsArray()
    //    {
    //        return ButtonsArray;
    //    }
    //    // Update is called once per frame.
    //    void Update()
    //    {

    //    }
    //    public void Buy()
    //    {

    //    }
    //    public void DisplayClothes(int displaynumber) //Displaynumber 0 = helmets, 1 = bodyarmor, 2 = boots
    //    {
    //        this.displaynumber = displaynumber;
    //        if(displaynumber == 0)
    //       for(int i = 0;  i< ButtonsArray.Length; i++)
    //        {
    //            if(i<allItems.AllHelmets.Count)
    //            SetImageInButton(ButtonsArray[i], allItems.AllHelmets[i].Url);
    //                else
    //                {
    //                    ButtonsArray[i].gameObject.SetActive(false);
    //                }
    //        }
    //        if(displaynumber == 1)
    //            for (int i = 0; i < ButtonsArray.Length; i++)
    //            {
    //                if (i < allItems.AllBodyArmours.Count)
    //                    SetImageInButton(ButtonsArray[i], allItems.AllBodyArmours[i].Url);
    //                else
    //                {
    //                    ButtonsArray[i].gameObject.SetActive(false);
    //                }
    //            }
    //        if (displaynumber == 2)
    //            for (int i = 0; i < ButtonsArray.Length; i++)
    //            {
    //                if (i < allItems.AllBoots.Count)
    //                    SetImageInButton(ButtonsArray[i], allItems.AllBoots[i].Url) ;
    //                else
    //                {
    //                    ButtonsArray[i].gameObject.SetActive(false);
    //                }
    //            }

    //    }
    //    public void GoToTownHall()
    //    {
    //        SceneManager.LoadScene("townhall");
    //    }
    //    public void ClickedOnItem(int ItemNumber)
    //    {
    //        ItemSelected = ItemNumber;
    //        OnlySetActiveArmorText.gameObject.SetActive(true);
    //        OnlySetActiveNameText.gameObject.SetActive(true);
    //        OnlySetActiveRarityText.gameObject.SetActive(true);
    //        OnlySetActiveCurrentMoneyText.gameObject.SetActive(true);
    //        OnlySetActivePriceText.gameObject.SetActive(true);
    //        BuyText.gameObject.SetActive(true);
    //        BuyButton.gameObject.SetActive(true);

    //        if (displaynumber == 0)
    //        {
    //            Helmet selected = allItems.AllHelmets[ItemNumber];
    //            SetImageInButton(PreviewImage, selected.Url);
    //            NameText.text = selected.Name;
    //            ArmorText.text = selected.AdditionalArmour.ToString();
    //            PriceText.text = selected.Value.ToString();
    //            switch (selected.Rarity)
    //                {
    //                case 1:
    //                    {
    //                        RarityText.color = Color.gray;
    //                        RarityText.text = "Common";
    //                        break;
    //                    }
    //                case 2:
    //                    {
    //                        RarityText.color = new Color(0.8f, 0.6f, 1f);
    //                        RarityText.text = "Epic";
    //                        break;
                           
    //                    }
    //                    case 3:
    //                    {

    //                        RarityText.color = new Color(1.0f, 0.8f, 0.0f); ;
    //                        RarityText.text = "Legendary";
    //                        break;
    //                    }
    //                default:
    //                    {
    //                        RarityText.color = Color.gray;
    //                        RarityText.text = "Common";
    //                        break;
    //                    }

    //            }
    //        }
    //        if (displaynumber == 1)
    //        {
    //            Bodyarmour selected = allItems.AllBodyArmours[ItemNumber];
    //            SetImageInButton(PreviewImage, selected.Url);
    //            NameText.text = selected.Name;
    //            ArmorText.text = selected.AdditionalArmour.ToString();
    //            PriceText.text = selected.Value.ToString();
    //            switch (selected.Rarity)
    //            {
    //                case 1:
    //                    {
    //                        RarityText.color = Color.gray;
    //                        RarityText.text = "Common";
    //                        break;
    //                    }
    //                case 2:
    //                    {
    //                        RarityText.color = new Color(0.8f, 0.6f, 1f);
    //                        RarityText.text = "Epic";
    //                        break;

    //                    }
    //                case 3:
    //                    {

    //                        RarityText.color = new Color(1.0f, 0.8f, 0.0f); ;
    //                        RarityText.text = "Legendary";
    //                        break;
    //                    }
    //                default:
    //                    {
    //                        RarityText.color = Color.gray;
    //                        RarityText.text = "Common";
    //                        break;
    //                    }

    //            }
    //        }
    //        if (displaynumber == 2)
    //        {
    //            Boots selected = allItems.AllBoots[ItemNumber];
    //            SetImageInButton(PreviewImage, selected.Url);
    //            NameText.text = selected.Name;
    //            ArmorText.text = selected.AdditionalArmour.ToString();
    //            PriceText.text = selected.Value.ToString();
    //            switch (selected.Rarity)
    //            {
    //                case 1:
    //                    {
    //                        RarityText.color = Color.gray;
    //                        RarityText.text = "Common";
    //                        break;
    //                    }
    //                case 2:
    //                    {
    //                        RarityText.color = new Color(0.8f, 0.6f, 1f);
    //                        RarityText.text = "Epic";
    //                        break;

    //                    }
    //                case 3:
    //                    {

    //                        RarityText.color = new Color(1.0f, 0.8f, 0.0f); ;
    //                        RarityText.text = "Legendary";
    //                        break;
    //                    }
    //                default:
    //                    {
    //                        RarityText.color = Color.gray;
    //                        RarityText.text = "Common";
    //                        break;
    //                    }

    //            }
    //        }
    //    }
    //    public void SetImageInButton(Button button, string URL)
    //    {

    //        if (button != null)
    //        {
    //            bool ThereIsAnImage = false;
    //            // Access the Image component of the Button
    //            Image buttonImage = button.GetComponent<Image>();

    //            if (buttonImage != null)
    //            {
    //                // Call the SetImageFromImage method and assign the result to the button's Image component
    //                ThereIsAnImage = SetImageFromImage(buttonImage, URL, ThereIsAnImage);
    //            }
    //            else
    //            {
    //                Debug.LogError("The clicked button does not have an Image component.");
    //            }
    //            button.gameObject.SetActive(ThereIsAnImage);
    //        }
    //        else
    //        {
    //            Debug.LogError("The clicked GameObject does not have a Button component.");
    //        }
    //    }

    //    private bool SetImageFromImage(Image imageComponent, string imagePath, bool ThereIsAnImage)
    //    {
    //        if (File.Exists(imagePath))
    //        {
    //            // Load the image file as a byte array
    //            byte[] imageData = File.ReadAllBytes(imagePath);

    //            // Create a texture and load the image data
    //            Texture2D texture = new Texture2D(2, 2);
    //            texture.LoadImage(imageData);

    //            // Create a Sprite from the loaded texture
    //            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

    //            // Apply the sprite directly to the Image component
    //            imageComponent.sprite = sprite;
    //            ThereIsAnImage = true;
    //        }
    //        else
    //        {
    //            Debug.LogError("Image file not found at path: " + imagePath);
    //        }
    //        return ThereIsAnImage;
    //    }
    }
}
