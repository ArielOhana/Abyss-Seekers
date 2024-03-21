using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.5f);
    }

}
