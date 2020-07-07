using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true; 

    public static PlayerMoving instance; //unique instance of the script for easy access to the script
    public float speed = 1f;
    public bool tiltToMove = false;
    public float tiltSpeed = 10f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
    }

    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, set pc settings

          //TODO Put user controls here

#endif
            //if (Input.GetKey(KeyCode.A))
            //{
            //    transform.Translate(x: -speed * Time.deltaTime, y: 0, z: 0);
            //}

            //if (Input.GetKey(KeyCode.W))
            //{
            //    transform.Translate(x: 0, y: speed * Time.deltaTime, z: 0);
            //}

            //if (Input.GetKey(KeyCode.S))
            //{
            //    transform.Translate(x: 0, y: -speed * Time.deltaTime, z: 0);
            //}

            //if (Input.GetKey(KeyCode.D))
            //{
            //    transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);
            //}

            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            transform.Translate(horiz * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0f);

            //Vector3 direction = new Vector3(horiz, vert, 0f);
            //direction.Normalize();
            //direction.x;
            //direction.y;


#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

            if (tiltToMove) {
                horiz = Input.acceleration.x;
                vert = Input.acceleration.y;
                transform.Translate(horiz * tiltSpeed * Time.deltaTime, vert * tiltSpeed * Time.deltaTime, 0f);
            }

            if (Input.touchCount == 1) // if there is a touch
            {
                Touch touch = Input.touches[0];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
            }
#endif
            //transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
            //    (
            //    Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
            //    Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
            //    0
            //    );

            Vector3 min = mainCamera.ViewportToWorldPoint(Vector2.zero);
            Vector3 size = mainCamera.ViewportToWorldPoint(Vector2.one) - min;
            Rect worldBounds = new Rect(min.x, min.y, width:size.x, height:size.y);

            //// SIMPLE WRAPPING IN PIXEL SPACE
            //Vector3 pos = transform.position;
            //Vector2 pixelPosition = mainCamera.WorldToScreenPoint(transform.position);

            //if (pixelPosition.x > Screen.width)
            //    pos.x -= worldBounds.width;
            //if (pixelPosition.x < 0)
            //    pos.x += worldBounds.width;

            //if (pixelPosition.y > Screen.height)
            //    pos.y -= worldBounds.height;
            //if (pixelPosition.y < 0)
            //    pos.y += worldBounds.height;

            // SIMPLE WRAPPING IN VIEWPORT SPACE
            Vector3 pos = transform.position;
            Vector2 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

            if (viewportPosition.x > 1f)
                pos.x -= worldBounds.width;
            if (viewportPosition.x < 0)
                pos.x += worldBounds.width;

            if (viewportPosition.y > 1f)
                pos.y -= worldBounds.height;
            if (viewportPosition.y < 0)
                pos.y += worldBounds.height;

            // SIMPLE WRAPPING IN WORLD SPACE
            //Vector3 pos = transform.position;

            //if (pos.x > worldBounds.xMax)
            //    pos.x -= worldBounds.width;
            //if (pos.x < worldBounds.xMin)
            //    pos.x += worldBounds.width;

            //if (pos.y > worldBounds.yMax)
            //    pos.y -= worldBounds.height;
            //if (pos.y < worldBounds.yMin)
            //    pos.y += worldBounds.height;

        }
    }

    //setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }
}
