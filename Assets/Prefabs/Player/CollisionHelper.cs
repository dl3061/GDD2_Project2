using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHelper : MonoBehaviour {

    [Tooltip("The mask for the light elements")]
    public LayerMask lightMask = 8;

    [Tooltip("The mask for the darl=ing in the franxx elements")]
    public LayerMask darkMask = 9;

    private bool isTouchingGround;
    private Vector2 groundNormalAngle;

    // Colliders holds this game object's colliders
    private List<Collider> colliders;

    // Buffer dist is added to a collider's width/height when performing a raycast
    public float bufferDist = .05f * 0.8f;

    // Step dist is used to test multiple x for vertical raycasts and multiple y for horizontal raycasts
    public float stepDist = 0.01f;

    private void Awake()
    {
        colliders = new List<Collider>(transform.GetComponentsInChildren<Collider>());
    }

    // Use this for initialization
    void Start ()
    {
        isTouchingGround = false;
        groundNormalAngle = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Check if touching ground
        isTouchingGround = UpdateIsTouchingGround();
    }

#region Public Functions

    public bool IsTouchingGround()
    {
        return isTouchingGround;
    }

    #endregion

    private bool UpdateIsTouchingGround()
    {
        bool retVal = false;

        // Check if touching ground
        for (int i = 0; i < colliders.Count; i++)
        {
            // Check if any collider is touching the ground
            Vector3 colOffset = Vector3.Scale(colliders[i].bounds.center, transform.localScale);
            Vector3 colSize = Vector3.Scale(colliders[i].bounds.size, transform.localScale);

            float distance = colSize.y / 2f + bufferDist;
            Vector3 position = transform.position + new Vector3(colOffset.x, colOffset.y);


            bool hitGround = Physics.Raycast(position, Vector3.down, distance, lightMask);

            /*
            // Check if touching slanted ground 
            if (!hitGround)
            {
                float groundDistAngle = Mathf.Sqrt(2 * distance * distance);

                hitGround = Physics.Raycast(position, Vector3.down, groundDistAngle, lightMask);
            }
            */

            retVal = hitGround;

            // Break if true
            if (retVal)
                return retVal;

        }

        Debug.Log("Hitting ground?: " + retVal);

        return retVal;
    }


}
