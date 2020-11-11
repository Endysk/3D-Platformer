using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //Layer of collision that layer "Player" is assigned.
    [SerializeField] private LayerMask playerMask;

    //Ground transform is touching player
    [SerializeField] private Transform checkGroundTransform;  
    
    //Player Alive
    private bool Alive = true;

    //Space key down?
    private bool spacePressed;

    //The camera following/moving from the player
    private float horizontalInput;

    //Rigidbody Component
    private Rigidbody rigidBodyComponent;

    //Number of Double jumps left
    float doubleJumpsLeft;

    //Determines how high you jump.
    private float jumpPower = 7f;

    //Shift Down?
    private bool shiftDown;

    private void Start()
    {
        doubleJumpsLeft = 0;
        //Sets rigidbody variable to the Rigidbody of host.
        rigidBodyComponent = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //If alive
        if (Alive == true)
        {

            //Check is space is pressed
            if (Input.GetKeyDown(KeyCode.Space))      //GetKey means if you hold down you keep jumping, currently tou cant
            {

                spacePressed = true;

            }

        }

        //A-D and Left-Right arrow key movement
        horizontalInput = Input.GetAxis("Horizontal");

    }

    //Called Every Physics Update (100 Times Per Second)
    private void FixedUpdate()
    {
        //Move the camera to go along with the player. Y is gravity. Z is camera follow speed/movement. X not needed
        rigidBodyComponent.velocity = new Vector3(0, rigidBodyComponent.velocity.y, horizontalInput);

        //Is the player touching the ground?
        if (Physics.OverlapSphere(checkGroundTransform.position, 0.1f, playerMask).Length == 0)
        {
            //Has 1 double jump left?
            if (doubleJumpsLeft >= 1)
            {

                if(spacePressed)
                {

                    //Increase jump power, and take away a double jump.
                    jumpPower *= 1.1f;
                    doubleJumpsLeft--;

                    //aply necessary forces.
                    rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    spacePressed = false;
                    //stop the code
                    return;

                }

            }
            //stop the code
            return;

        }
        //Self explanitory
        if (spacePressed)
        {
            jumpPower = 7f;
            
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            spacePressed = false;
        }

    }

    //If player touches a collider with a trigger
    private void OnTriggerEnter(Collider other)
    {
        
        //If the Player collides with a coin.
        if(other.gameObject.layer == 9)
        {

            //Destroy Coin after collecting it
            Destroy(other.gameObject);
            //Give half of a double jump to the player, it takes 2 to get one double jump.
            doubleJumpsLeft += .5f;

        }
        //if Win coin is touched
        if(other.gameObject.layer == 10)
        {

            //Destroy Win coin
            Destroy(other.gameObject);
            //Load next scene or restart
            SceneManager.LoadScene(0);

        }

    }

}
