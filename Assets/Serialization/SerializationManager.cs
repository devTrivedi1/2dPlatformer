using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectData
{

	public int id;
	public string name;
	public Vector3 position;
	public Vector3 rotation;
	public List<Components> components;

	public override string ToString()
	{
		var positionString = $"{position.x},{position.y},{position.z}";
		var rotationString = $"{rotation.x},{rotation.y},{rotation.z}";

		var data = $"{id};{name};{positionString};{rotationString};{true};";
		for (int i = 0; i < components.Count; i++)
		{
			data += (int)components[i] + " ";
		}

		return data.Trim();
	}
}

public enum Components
{
	rb, bx, sc
}

public class SerializationManager : MonoBehaviour
{
	public ObjectData[] objects;
	void Start()
	{
		string data = "";
		var objectList = data.Split("\n");

		foreach (var obj in objects)
		{
			data += obj + "\n";
		}
		Debug.Log(data);
		foreach (var item in data.Split("\n"))
		{
			if (item == "") continue;
			var properties = item.Split(";");
			int id = int.Parse(properties[0]);
			var name = properties[1];
			Vector3 position = GetVectorData(properties[2]);
			Vector3 rotation = GetVectorData(properties[3]);
			bool active = bool.Parse(properties[4]);
			GameObject obj;
			switch (id)
			{
				case 0:
					obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
					break;
				case 1:
					obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					break;
				case 2:
					obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
					break;
				default:
					obj = new();
					break;
			}
			var components = properties[5];
			foreach (var property in components.Split(" "))
			{
				try
				{
					Components c = (Components)int.Parse(property);
					switch (c)
					{
						case Components.rb:
							obj.AddComponent<Rigidbody>();
							break;
						case Components.bx:
							obj.AddComponent<BoxCollider>();
							break;
						case Components.sc:
							obj.AddComponent<SphereCollider>();
							break;
						default:
							break;
					}
				}
				catch
				{

				}
			}
			obj.transform.position = position;
			obj.transform.rotation = Quaternion.Euler(rotation);
			obj.name = name;
			obj.SetActive(active);
		}
	}
	public enum Components
	{
		rb, bx, sc
	}
	private static Vector3 GetVectorData(string str)
	{
		var VectorProp = str.Split(",");
		Vector3 vec;
		vec.x = float.Parse(VectorProp[0]);
		vec.y = float.Parse(VectorProp[1]);
		vec.z = float.Parse(VectorProp[2]);
		Vector3 position = vec;
		return vec;

	}

}
