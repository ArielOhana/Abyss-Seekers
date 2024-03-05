using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public LogicScript logicScript;
    // Start is called before the first frame update
    void Start()
    {
        logicScript = FindObjectOfType<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            logicScript.myHero.Stats.Heal();
            Debug.Log(logicScript.myHero.Stats.ToString());
            GameObject objToMove = GameObject.Find("Circle");

            // Check if the GameObject is found
            if (objToMove != null)
            {
                // Move the GameObject upward on the Y-axis
                float moveAmount = 1.0f; // Adjust this value based on how much you want to move
                objToMove.transform.position += new Vector3(0f, moveAmount, 0f);
            }
            else
            {
                Debug.LogError("GameObject not found!");
            }
        }
    }
}
