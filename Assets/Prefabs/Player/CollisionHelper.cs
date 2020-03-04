using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHelper : MonoBehaviour {

    [Tooltip("The mask for the light elements")]
    public LayerMask lightMask = 8;

    [Tooltip("The mask for the darl=ing in the franxx elements")]
    public LayerMask darkMask = 9;

    private bool isTouchingGround;
    private bool isTouchingGroundLight;
    private bool isTouchingGroundDark;
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
        isTouchingGroundLight = false;
        isTouchingGroundDark = false;
        groundNormalAngle = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Check if touching ground
        UpdateIsTouchingGround();
    }

#region Public Functions

    public bool IsTouchingGround()
    {
        return isTouchingGround;
    }

    #endregion

    private void UpdateIsTouchingGround()
    {
        // Reset flags
        isTouchingGround = false;
        isTouchingGroundDark = false;
        isTouchingGroundLight = false;

        // Check if any of the colliders are touching the ground
        for (int i = 0; i < colliders.Count; i++)
        {
            // Get the position and distance
            Vector3 colPosition = colliders[i].bounds.center; // This is already in world space. Don't do complicated math on it.
            Vector3 colSize = Vector3.Scale(colliders[i].bounds.size, transform.localScale);

            float distance = colSize.y / 2.0f + bufferDist;

            // Perform the raycast(s) if necessary
            isTouchingGroundLight |= Physics.Raycast(colPosition, Vector3.down, distance, lightMask);
            isTouchingGroundDark |= Physics.Raycast(colPosition, Vector3.down, distance, darkMask);
        }

        isTouchingGround = isTouchingGroundLight || isTouchingGroundDark;
    }


}
