using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using static UnityEditor.Progress;

public class GetAllButtons : MonoBehaviour
{
    [SerializeField] static Button ButtonOne;
    [SerializeField] static Button ButtonTwo;
    [SerializeField] static Button ButtonThree;
    [SerializeField] static Button ButtonFour;
    [SerializeField] static Button ButtonFive;
    [SerializeField] static Button ButtonSix;
    [SerializeField] static Button ButtonSeven;
    [SerializeField] static Button ButtonEight;
    [SerializeField] static Button ButtonNine;

    public Button[] ButtonsArray = new Button[] { ButtonOne, ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine };
    void Start()
    {
        ButtonsArray = new Button[] { ButtonOne, ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine };
        for(int i =0;i<ButtonsArray.Length;i++)
        {
            SetImageInButton(ButtonsArray[i], $"Assets/Items/Icons/Armor/BodyArmor{i}.png");
        }
    }
    public Button[] GetButtonsArray()
    {
        return ButtonsArray;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImageInButton(Button button,string URL)
    {

        if (button != null)
        {
            bool ThereIsAnImage = false;
            // Access the Image component of the Button
            Image buttonImage = button.GetComponent<Image>();

            if (buttonImage != null)
            {
                // Call the SetImageFromImage method and assign the result to the button's Image component
                ThereIsAnImage = SetImageFromImage(buttonImage, URL,ThereIsAnImage);
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
