using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollidableObjects : MonoBehaviour
{
    protected Collider2D z_Collider;
    [SerializeField]
    protected ContactFilter2D z_Filter;
    protected List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    protected bool isColliding = false;

    protected virtual void Start()
    {
        z_Collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);

        isColliding = false;

        foreach (var o in z_CollidedObjects)
        {
            isColliding = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            HandleInteraction();
        }

        z_CollidedObjects.RemoveAll(o => !o.IsTouching(z_Collider));
    }

    

    protected virtual void HandleInteraction()
    {
        Debug.Log("Default interaction behavior. No scene specified.");
    }

    protected virtual void OnGUI()
    {
        if (isColliding)
        {
            GUIStyle labelStyle = GUI.skin.label;
            labelStyle.fontSize = 40;

            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 200, 400, 80), "Press E to interact", labelStyle);
        }
    }
}
