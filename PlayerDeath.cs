using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    int currentScene;

    Rigidbody rigidBodyPlayer;

    private void Start()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidBodyPlayer = GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {

        if(rigidBodyPlayer.position.y < 3)
        {

            SceneManager.LoadScene(currentScene);

        }

    }
}
