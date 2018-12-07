using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AnimationControl : MonoBehaviour {

    public static AnimationControl instance;

    PlayableDirector playableDirector;
    public List<TimelineAsset> timelines;

	// Use this for initialization
	void Start () {

        instance = this;

        playableDirector = GetComponent<PlayableDirector>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playableDirector.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playableDirector.Pause();
        }
    }

    public void ChangePlayebleTimeline(int index)
    {
        playableDirector.Play(timelines[index]);
    }
}
