using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Formation")]
public class Formation : ScriptableObject
{
    [System.Serializable]
    public struct formationDetail
    {
        public Vector3 formationScaleDetails;
        public Vector3 formationPositionDetails;
    }

    public List<formationDetail> formationDetails;
}
