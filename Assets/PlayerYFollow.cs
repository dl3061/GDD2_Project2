using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerYFollow : MonoBehaviour
{
    [Tooltip("The offset between the camera and the player.")]
    public float camHeightDistance = 7f;

    [Tooltip("The offset between the camera and the player.")]
    public float camLengthDistance = 7f;

    [Tooltip("The ratio to rotate it per player height.")]
    public float rotational_scale = 1f;

    [Tooltip("Pitch value for cam")]
    public float camPitch = 45.0f;


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
            Vector3 playerPos = GameManager.Singleton.ActivePlayer.transform.position;

            // Update the y position to focus the player
            Vector3 pos = transform.position;
            pos.y = playerPos.y + camHeightDistance;
            pos.z = playerPos.z - camLengthDistance;
            transform.position = pos;

            // Rotate along the x-axis of the camera to follow the player
            Quaternion rot = Quaternion.Euler(camPitch, initRotation.eulerAngles.y, initRotation.eulerAngles.z);
            transform.rotation = rot;
        }
    }
}
