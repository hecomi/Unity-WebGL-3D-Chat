using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerManager : MonoBehaviour 
{
	public GameObject networkPlayerPrefab;

	private Dictionary<string, NetworkPlayerController> players_ = 
		new Dictionary<string, NetworkPlayerController>();
	static private readonly char[] Delimiter = new char[] {','};

	void Awake()
	{
		Application.ExternalEval("socket.isReady = true;");
	}

	void Move(string argsStr) 
	{
		var args = argsStr.Split(Delimiter);
		GetPlayer(args[0]).Move(new Vector3(
			float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3])));
	}

	void Rotate(string argsStr) 
	{
		var args = argsStr.Split(Delimiter);
		GetPlayer(args[0]).Rotate(new Quaternion(
			float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4])));
	}

	void Talk(string argsStr)
	{
		var args = argsStr.Split(Delimiter);
		GetPlayer(args[0]).Talk(args[1]);
	}

	NetworkPlayerController GetPlayer(string id)
	{
		return players_.ContainsKey(id) ? players_[id] : CreatePlayer(id);
	}

	NetworkPlayerController CreatePlayer(string id)
	{
		var obj = Instantiate(networkPlayerPrefab) as GameObject;
		obj.name = id;
		var player = obj.GetComponent<NetworkPlayerController>();
		players_.Add(id, player);
		return player;
	}

	void DestroyPlayer(string id)
	{
		var player = GetPlayer(id);
		player.Talk("[LOGOUT] Good by!");
		players_.Remove(id);
		Destroy(player.gameObject, 3f);
	}
}
