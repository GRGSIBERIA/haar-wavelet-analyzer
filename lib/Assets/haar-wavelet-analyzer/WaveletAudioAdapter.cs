using UnityEngine;
using System.Collections;

public class WaveletAudioAdapter : MonoBehaviour {

	public Texture2D buffer;
	public AudioClip audio_clip;
	public int offset = 0;
	public int channel = 1;

	int width;
	//int height;
	int scale;
	float[] all_data;
	float[] data;

	// Use this for initialization
	void Start () {
		width = buffer.width;
		//height = buffer.height;
		all_data = new float[audio.clip.samples];
		data = new float[width];


		scale = (int)Mathf.Log(width, 2);

		//audio.GetOutputData(all_data, 0);
	}

	void GetSamples()
	{
		//for (int i = 0; i < width; i++)
		//{
		//	data[i] = all_data[offset + i];
		//}
	}

	// Update is called once per frame
	void Update()
	{
		//Validate();		// データチェック
		//GetSamples();	// ここでdataにwidthだけ入力する

		//HaarWavelet wavelet = new HaarWavelet(scale, data, false);
		//var packs = wavelet.ComputeScales();
		//Coloring(packs);

		        if (Input.GetKeyDown(KeyCode.Return)) {
            audio.Stop();
            audio.Play();
        }
        Debug.Log(audio.timeSamples);
    

	}

	void Coloring(HaarWavelet.ScalePack[] packs)
	{
		/*
		for (int i = 0; i < packs.Length; i++)
		{
			for (int j = 0; j < packs[i].Size - 1; j++)
			{
				Debug.DrawLine(new Vector3(j, packs[i].Diff[j]), new Vector3(j + 1, packs[i].Diff[j + 1]));
			}
		}
		*/
		int cnt = 0;
		for (int i = 0; i < all_data.Length - 1; i += 440)
		{
			const float d = 1f / 440f;
			Debug.DrawLine(new Vector3(cnt * 0.01f, all_data[i] * 100), new Vector3((cnt + 1) * 0.01f, all_data[i + 1] * 100));
			cnt++;
		}

		cnt = 0;
		for (int i = 0; i < all_data.Length; i++)
			if (all_data[i] == 0f) cnt++;
		Debug.Log(cnt);
	}

	void Validate()
	{
		if (offset < 0) 
			offset = 0;
		else if (offset > audio_clip.samples - 1024) 
			offset = audio_clip.samples - 1024;

		if (channel < 1)
			channel = 1;
		else if (channel > audio_clip.channels)
			channel = audio_clip.channels;
	}
}
