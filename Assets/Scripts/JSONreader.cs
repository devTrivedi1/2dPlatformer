using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONreader : MonoBehaviour
{
	public TextAsset textJson;

    [System.Serializable]
	public class Player
	{
		public string name;
		public int health;
		public int stamina;
	}
	[System.Serializable]
	public class PlayerList
	{
		public Player[] player;
	}

    public PlayerList myPlayerList = new PlayerList();

    void Start()
    {
        myPlayerList = JsonUtility.FromJson<PlayerList>(textJson.text);
    }

}
