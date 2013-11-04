using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class HaarWavelet 
{
	/// <summary>
	/// 差分と高さをまとめておくためのクラス
	/// </summary>
	public class ScalePack
	{
		float[] diff;
		float[] length;

		public int Scale { get; private set; }
		public int Size { get; private set; }
		public float[] Diff
		{
			get { return diff; }
		}
		public float[] Length
		{
			get { return length; }
		}
		
		public ScalePack(int size, int scale)
		{
			Scale = scale;
			Size = size;
			diff = new float[size];
			length = new float[size];
		}
	}

	float[] values = null;
	ScalePack[] packs;
	int scale;

	public HaarWavelet(int scale, float[] values, bool squared = true)
	{
		int scale_size = (int)Mathf.Log(values.Length, 2);	// 2^nに桁落とし
		int size = 1 << (scale_size - 1);

		this.values = new float[values.Length];

		if (scale_size > scale)
			throw new IndexOutOfRangeException("入力信号に対してスケールが大きすぎます : " + scale_size.ToString() + " > " + scale.ToString());

		if (!squared)	// 入力信号がn^2じゃない場合，自動的に正規化する
		{
			for (int i = 0; i < values.Length; i++)
			{
				values[i] = values[i] * values[i];	// sqrt(n^2)
			}
		}
		
		this.scale = scale;
		Array.Copy(values, 0, this.values, 0, size);
		InitializeSpace(scale, size);
	}

	void InitializeSpace(int scale, int size)
	{
		packs = new ScalePack[scale];

		int now = size;
		for (int i = 0; i < scale; i++)
		{
			packs[i] = new ScalePack(now, scale);
			now >>= 1;
		}
	}

	ScalePack ComputeScale(int depth_of_scale)
	{
		int prev = depth_of_scale - 1;
		for (int i = 0; i < packs[depth_of_scale].Size; i++)
		{
			int i2 = i * 2;
			int i21 = i * 2 + 1;
			packs[depth_of_scale].Diff[i] = packs[prev].Length[i2] - packs[prev].Length[i21];	// 自乗したらあかん
			packs[depth_of_scale].Length[i] = (packs[prev].Length[i2] + packs[prev].Length[i21]) * 0.5f;
		}
		return packs[depth_of_scale];
	}

	public ScalePack[] ComputeScales(int scale)
	{
		if (!(scale > 0) || scale > this.scale) 
			throw new IndexOutOfRangeException("スケールは 1 以上，" + scale.ToString() + " より小さい値を指定して下さい．");

		ScalePack[] scales = new ScalePack[scale];

		scales[0] = packs[0];
		for (int i = 1; i < scale; i++)
			scales[i] = ComputeScale(i);

		return scales;
	}

	public ScalePack[] ComputeScales()
	{
		return ComputeScales(scale);
	}
}
