using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

	public static void SaveData(Player player)
	{
		//Binary formatter, replaced by playerprefs
		/*
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.man";
		FileStream stream = new FileStream(path, FileMode.Create);

		SaveFile file = new SaveFile(player);
		
		formatter.Serialize(stream, file);
		stream.Close();
		*/

		PlayerPrefs.SetFloat("posX", player.transform.position.x);
		PlayerPrefs.SetFloat("posY", player.transform.position.y);
		PlayerPrefs.SetFloat("posZ", player.transform.position.z);

	}

	public static SaveFile LoadData()
	{
		//binary formatter, replaced by playerprefs
		/*
		string path = Application.persistentDataPath + "/player.man";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			SaveFile file = formatter.Deserialize(stream) as SaveFile;
			stream.Close();


			return file;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
		*/

		SaveFile file = new SaveFile();

		if (PlayerPrefs.HasKey("posX"))
		{
			file.position[0] = PlayerPrefs.GetFloat("posX");
			file.position[1] = PlayerPrefs.GetFloat("posY");
			file.position[2] = PlayerPrefs.GetFloat("posZ");

			return file;
		}
		else
		{
			Debug.LogError("Save file not found.");
			return null;
		}
	}

		
   
}
