using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Events;
public class LogicScript : MonoBehaviour
{

    public Hero myHero;
    public GameObject? Circle;
    private GetAllButtons getAllButtons;
    private Button[] ButtonsArray;
    void Start()// Get all the data from database and import it to Classes
    {
        myHero = new Hero();
        myHero.InitializeStats(); // Initialize Stats separately
        this.ButtonsArray = getAllButtons.GetButtonsArray();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArmoryonClick(int item)
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        Button button = clickedObject.GetComponent<Button>();

        if (button != null)
        {
            // Access the Image component of the Button
            Image buttonImage = button.GetComponent<Image>();

            if (buttonImage != null)
            {
                // Call the SetImageFromImage method and assign the result to the button's Image component
                SetImageFromImage(buttonImage, $"Assets/Items/Icons/Armor/BodyArmor{item}.png");
            }
            else
            {
                Debug.LogError("The clicked button does not have an Image component.");
            }
        }
        else
        {
            Debug.LogError("The clicked GameObject does not have a Button component.");
        }
    }

    private void SetImageFromImage(Image imageComponent, string imagePath)
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
        }
        else
        {
            Debug.LogError("Image file not found at path: " + imagePath);
        }
    }

}


