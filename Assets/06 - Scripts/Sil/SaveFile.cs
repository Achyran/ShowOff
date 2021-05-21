using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
	public int testNum;
	public bool testBool;
	public float[] position = new float[3];

	//constructor was used with serializing
	/*
	public SaveFile(Player player)
	{
		position = new float[3];
		position[0] = player.transform.position.x;
		position[1] = player.transform.position.y;
		position[2] = player.transform.position.z;
	}
	*/
}
