using UnityEngine;
using System;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("maxLevel"));
		PlayerPrefs.SetInt ("LevelAtBeginning", PlayerPrefs.GetInt ("maxLevel"));
		PlayerPrefs.SetInt ("numPigs", 0);
		PlayerPrefs.SetInt ("numShards", 0);
    }
}