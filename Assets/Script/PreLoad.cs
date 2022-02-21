using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PreLoad : MonoBehaviour
{
    private VideoPlayer video;
    private float videoTime = 6f;
    private float time = 0;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        //videoTime = (float)video.clip.length;
        //Debug.Log(videoTime);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time>videoTime)
        {
            SceneManager.LoadScene(1);
        }
    }
}
