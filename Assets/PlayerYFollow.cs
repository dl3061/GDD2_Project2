using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerYFollow : MonoBehaviour
{
    [Tooltip("The offset between the camera and the player.")]
    float offset = 7f;

    [Tooltip("The ratio to rotate it per player height.")]
    public float rotational_scale = 1f; 


    Quaternion initRotation;

    private void Start()
    {
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Singleton.ActivePlayer != null)
        {
            float player_y = GameManager.Singleton.ActivePlayer.transform.position.y;

            // Update the y position to focus the player
            Vector3 pos = transform.position;
            pos.y = player_y + offset;
            transform.position = pos;

            // Rotate along the x-axis of the camera to follow the player
            Quaternion rot = Quaternion.Euler(25, initRotation.eulerAngles.y, initRotation.eulerAngles.z);
            transform.rotation = rot;
        }
    }
}
