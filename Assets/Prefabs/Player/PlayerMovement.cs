using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        uint clock = time;
        float timer = time.DeltaTime;
        vector3 startPos;
        vector3 endPos;
        uint charRoute = 0;

        vector3 leftPosition = vector3(-10.0f,0.0f,0.0f);
        vector3 centerPosition = vector3(0.0f, 0.0f, 0.0f);
        vector3 rightPosition = vector3(10.0f,0.0f,0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        float percent = 0f;
        vector3 currentPos = vector3.lerp(startPos, endPos, percent);
    }
}
