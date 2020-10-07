using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    public GameObject PractiseNode;

    public Node currentNode;

    public float GhostSpeed = 3.9f;

   void Update()
    {
        if(transform.position != PractiseNode.transform.position)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, PractiseNode.transform.position, GhostSpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPosition);
        }
    }
}
