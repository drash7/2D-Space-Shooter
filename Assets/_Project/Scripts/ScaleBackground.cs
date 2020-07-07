using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        float height = camera.orthographicSize * 2f; // because only half is taken
        float width = height * camera.aspect;
        float scale = width / height;
        transform.localScale = new Vector3(scale, scale, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
