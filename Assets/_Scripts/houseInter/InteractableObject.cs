using Assets;
using Context;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableObject : CollidableObjects
{
    [SerializeField]
    private string interactableSceneName = "Menu";
    [SerializeField]
    private string interactMessage = "Press E to enter";
    public SQLdb DBManager;
    
    protected override void HandleInteraction()
    {
        Debug.Log("Teleporting to the scene: " + interactableSceneName);
        SceneManager.LoadScene(interactableSceneName);
    }

    protected override void OnGUI()
    {
        if (isColliding)
        {
            GUIStyle labelStyle = GUI.skin.label;
            labelStyle.fontSize = 40;

            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 150, 600, 50), interactMessage, labelStyle);
        }
    }
}
