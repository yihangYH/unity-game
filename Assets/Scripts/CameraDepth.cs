using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDepth : MonoBehaviour
{
   
    void Update()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }
}
