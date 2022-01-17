using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;
    public float bounceForce;

    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flyClip, pingClip, diedClip;

    private bool isAlive; // default value is false
    private bool didFlap;

    private GameObject spawner;

    public float flag = 0;
    // Start is called before the first frame update
    void Awake() // dung de khoi tao
    {
        isAlive = true; // set gia tri true ban dau, vi mac dinh = false
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MakeInstance();
        spawner = GameObject.Find("Pipe Spawner");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this; // this points to nearest func/class, here is class BirdController 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BirdMovement();
    }

    void BirdMovement()
    {
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false; // click only once
                // init force when user click
                // x -> move, y -> jump, we need bird jump -> no use x
                // even x is not used, still pass current x
                myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
                // play sound effects, use PlayOneShot
                audioSource.PlayOneShot(flyClip);
            }
        }
        // su kien nhan chuot trai
        //if (Input.GetMouseButtonDown(0)) {}
        if (myBody.velocity.y > 0)
        {
            float angle = 0;
            // khi cham xuong muot hon
            angle = Mathf.Lerp(0, 90, myBody.velocity.y / 7);
            // xoay dau chim khi nhay len
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (myBody.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (myBody.velocity.y < 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -90, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    //use public func with button
    public void FlapButton()
    {
        didFlap = true;
    }

    // Bird successfully go through spaces between two pipes -> play sound
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            audioSource.PlayOneShot(pingClip);
        }
    }

    //Bird collied with Pipe or Ground -> dead
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Pipe" || target.gameObject.tag == "Ground")
        {
            flag = 1;
            // destroy spawner to not spawn pipe after bird died
            Destroy(spawner);
            audioSource.PlayOneShot(diedClip);
            anim.SetTrigger("Died"); // because we set Trigger Condition at Animator
        }
    }
}
