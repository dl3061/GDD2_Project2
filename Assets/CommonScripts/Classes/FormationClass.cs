using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Formation")]
public class Formation : ScriptableObject
{
    [SerializeField]
    int lanes;
    [System.Serializable]
    public struct formationDetail
    {
        public List<Polarity> polarities;
        public Vector3 formationScaleDetails;
        public Vector3 formationPositionDetails;
    }

    public List<formationDetail> formationDetails;


    /// <summary>
    /// Calculates the length of the formation along the z-axis.
    /// </summary>
    /// <returns></returns>
    public float GetLength()
    {
        float minZ = float.PositiveInfinity;
        float maxZ = float.NegativeInfinity;

        foreach (formationDetail piece in formationDetails)
        {
            // Get the starting and end z corrdinates of this piece
            float currMinZ = piece.formationPositionDetails.z - (piece.formationScaleDetails.z / 2f);
            float currMaxZ = piece.formationPositionDetails.z + (piece.formationScaleDetails.z / 2f);

            // Save it to the edges of the overall formation
            if (currMinZ < minZ || float.IsPositiveInfinity(minZ))
                minZ = currMinZ;

            if (currMaxZ > maxZ || float.IsNegativeInfinity(maxZ))
                maxZ = currMaxZ;
        }

        float length = maxZ - minZ;
        if (length < 0f)
            length = (-1f) * length;

        return length;
    }
}
