using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Formation")]
public class Formation : ScriptableObject
{
    public List<Vector3[]> formationDetails;
}
