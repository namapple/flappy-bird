using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    // speed of pipe holder
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy script of Pipe Holder, so it did not continue to move when bird died
        if (BirdController.instance != null)
        {
            // flag == 1 -> bird collied with pipe -> destrouy script
            if (BirdController.instance.flag == 1)
            {
                //destroy script of PipeHolder that is PipeController.cs
                Destroy(GetComponent<PipeController>());
            }
        }
        PipeMovement();   
    }

    // Make pipe to move
    void PipeMovement()
    {
        //get the current position of pipe holder
        Vector3 temp = transform.position;
        // set the pipe holder run backward: Time.deltatTime to make smooth transition
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }

    // When pipe move and collide with Destroy Pipe with tag 'Destroy' -> pipe gets destroyed
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Destroy")
        {
            Destroy(gameObject); // gammeObject is pointing to its holder, here is PipeController
            // Destroy(target.gameObject); -> pointing to the gameObject has tag 'Destroy' (Destroy Pipe)
        }
    }
}
