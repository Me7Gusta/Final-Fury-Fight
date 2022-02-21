using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1f;
    public float xSmooth = 10f;
    //public float ySmooth = 8f;

    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        //float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        //targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    private bool CheckXMargin()
    {
        return (transform.position.x - player.position.x) < xMargin;
    }
}
