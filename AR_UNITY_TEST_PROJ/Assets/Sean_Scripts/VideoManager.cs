using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VideoManager : MonoBehaviour
{
    //hold a reference to the script that will make the video play in the unity scene
    [SerializeField]private DefaultTrackableEventHandler arRenderer;
    //holds the reference to the trackable behavior that determines if the AR detects the image
    private TrackableBehaviour behaviorCheck;

    //holds the game object that will play the video in AR, turning it on when detecting an object
    [SerializeField]private UnityEngine.Video.VideoPlayer video;

    //check or uncheck this if you want the video to keep looping in AR
    public bool Loop;
    //tracks to see if the video has played already once
    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        //check to see if there is a reference to the script
        if (arRenderer == null)
            Debug.Log("Forgot to put in the script.");

        //check to see if there is a reference to the video player component
        if (video == null)
            Debug.Log("Forgot to put in the video.");
        else
            video.Stop();
    }

    bool childIsRendered()
    {
        bool isRendered = false;
        var renderObject = GetComponentsInChildren<Renderer>();

        foreach(var components in renderObject)
        {
            //found that it is not rendered
            if (components.enabled == false)
            {
                isRendered = false;
                return isRendered;
            }
        }

        //we are safe to say that the object is rendered
        isRendered = true;

        return isRendered;
    }

    // Update is called once per frame
    void Update()
    {   
        //checks if the object is rendered on the screen
        if (childIsRendered())
        {
            hasPlayed = true;
            video.Play();
            StartCoroutine("WaitForSecondsRealTime", video.clip.length);
            
            //check to see if the video should loop or stop
            if (!Loop && hasPlayed)
                video.Stop();
        }
        //the object is not rendered, thus the video should not play
        else
        {
            hasPlayed = false;
            video.Stop();
        }
    }
}
