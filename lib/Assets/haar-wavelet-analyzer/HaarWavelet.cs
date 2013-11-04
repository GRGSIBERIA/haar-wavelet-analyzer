using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HaarWavelet 
{
	float[][] diff;
	float[][] length;

	public HaarWavelet(int scale, float[] values)
	{
		int scale_size = (int)Mathf.Log(values.Length, 2);	// 2^nに桁落とし
		int size = 1 << (scale_size - 1);

		if (scale_size > scale)
			throw new IndexOutOfRangeException("入力信号に対してスケールが大きすぎます : " + scale_size.ToString() + " > " + scale.ToString());

		InitializeSpace(scale, size);
	}

	void InitializeSpace(int scale, int size)
	{
		diff = new float[scale][];
		length = new float[scale][];

		for (int i = 0; i < scale; i++)
		{
			this.diff[i] = new float[size];
			this.length[i] = new float[size];
			size >>= 1;
		}
	}
}
