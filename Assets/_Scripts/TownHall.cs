using UnityEngine;

public class TownHall : MonoBehaviour
{
    private new Renderer renderer;  // Use the 'new' keyword to hide the inherited 'renderer'
    private Material spriteOutlineMaterial;
    private TextMesh textMesh;

    void Start()
    {
        // Ensure MeshRenderer component
        renderer = GetComponent<MeshRenderer>();

        if (renderer != null)
        {
            spriteOutlineMaterial = renderer.material;

            if (spriteOutlineMaterial != null)
            {
                // Ensure the outline effect is initially disabled
                DisableOutline();
            }
            else
            {
                Debug.LogError("SpriteOutline material is not assigned to the GameObject.");
            }
        }
        else
        {
            Debug.LogError("MeshRenderer component not found on the GameObject.");
        }

        // Ensure TextMesh component as a child
        textMesh = GetComponentInChildren<TextMesh>();
        if (textMesh != null)
            textMesh.gameObject.SetActive(false);
        else
            Debug.LogWarning("TextMesh component not found as a child of the GameObject.");
    }

    void OnMouseEnter()
    {
        // Enable the outline and text on mouse enter
        EnableOutline();
        if (textMesh != null)
            textMesh.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        // Disable the outline and text on mouse exit
        DisableOutline();
        if (textMesh != null)
            textMesh.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        // Handle mouse click if needed
    }

    private void EnableOutline()
    {
        // Assuming the outline is controlled by changing the material properties
        if (spriteOutlineMaterial != null)
        {
            spriteOutlineMaterial.SetFloat("_Outline", 0.002f); // Adjust the value as needed
        }
        else
        {
            Debug.LogError("SpriteOutline material is not assigned.");
        }
    }

    private void DisableOutline()
    {
        // Assuming the outline is controlled by changing the material properties
        if (spriteOutlineMaterial != null)
        {
            spriteOutlineMaterial.SetFloat("_Outline", 0f);
        }
        else
        {
            Debug.LogError("SpriteOutline material is not assigned.");
        }
    }
}
