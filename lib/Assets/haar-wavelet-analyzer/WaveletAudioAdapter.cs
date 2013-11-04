using UnityEngine;
using System.Collections;

public class WaveletAudioAdapter : MonoBehaviour {

	public AudioClip audio_clip;

	// Use this for initialization
	void Start () {
		int size = audio_clip.channels * audio_clip.samples;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
