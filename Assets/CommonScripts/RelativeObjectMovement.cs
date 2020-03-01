using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This monobehavior will be responsible for moving objects along the z plane in a speed
/// indicated by the GameManager, if applicable.
/// 
/// This script will be kept generic and will not necessitate the need for a rigidbody.
/// Instead, it may zero-out any velocity produced by a rigidbody. 
/// The affected velocity will only be in the direction of the z-plane. 
/// </summary>
public class RelativeObjectMovement : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        // Get the speed
        float velocity = 0f;
        if (GameManager.Singleton != null)
        {
            velocity = GameManager.Singleton.GetCurrentScrollSpeed();
        }

        // Check if there's a rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // If there is a rigidbody
            // Get the curr velocity
            Vector3 vel = rb.velocity;
            
            // Set it
            vel.z = velocity;
            rb.velocity = vel;
        }
        else
        {
            // If there isn't, update the position manually
            // Get the curr position
            Vector3 pos = transform.position;

            // Calc the next position;
            pos.z += velocity * Time.deltaTime;

            // Set position.
            transform.position = pos;
        }

    }
}
