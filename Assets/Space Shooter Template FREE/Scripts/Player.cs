using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance; 

    private Vector3 _defaultSpawn;
    private bool tryingToRespawn;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    void Start()
    {
        _defaultSpawn = transform.position;
        //UnityEngine.Debug.Log("Initial position is " + transform.position);
        //UnityEngine.Debug.Log("Position stored is " + transform.position);
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        Destruction();
    }    

    //'Player's' destruction procedure
    void Destruction()
    {

        if (tryingToRespawn)
        {
            return;
        }

        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        // Destroy(gameObject);
        PlayerController.Instance.PlayerKilled();
    }

    public void Respawn()
    {
        //transform.position = _defaultSpawn;
        // UnityEngine.Debug.Log("Respawned at " + transform.position);
        StartCoroutine(TryRespawn());
    }

    IEnumerator TryRespawn()
    {
        // increase collider size to give ship buffer, in case of
        // incoming projectiles
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 size = collider.size;
        collider.size *= 1.5f;

        // collider detection changes only on FixedUpdates
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        // do not get destroyed while trying to respawn
        tryingToRespawn = true;

        // try the default position first
        gameObject.transform.position = _defaultSpawn;
        yield return wait;

        while (IsTouching())
        {
            Vector3 spawn = _defaultSpawn;
            spawn.x += UnityEngine.Random.Range(-60, 60);
            spawn.y += UnityEngine.Random.Range(-60, 60);
            gameObject.transform.position = spawn;
            yield return wait;
        }

        // reset collider size and restore vulnerability to make 
        // gameplay fair
        collider.size = size;
        yield return new WaitForSeconds(1);
        tryingToRespawn = false;
    }

    private bool IsTouching()
    {
        return gameObject.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy"));
    }
}
















