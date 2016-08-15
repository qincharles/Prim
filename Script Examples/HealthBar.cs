using UnityEngine;
using System;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    Rect bgRect;
    Rect healthRect;

    public Texture2D healthBar;
    public Texture2D bgBar;

    private float fullHealth;
    private float remainingHealth;

    private int barOutlineSize;
    private int barStartHoriz;
    private int barWidth;
	public int barHeight = 40;
	private int barStartVert;

    void Start()
    {
        //initialize instance vars
        barOutlineSize = 2;
        barWidth = Screen.width;
   
        barStartVert = 0;//Screen.height - barOutlineSize;
        barStartHoriz = 0;

        //initialize health bar
        bgRect = new Rect(barStartHoriz, barStartVert, barWidth, barHeight);
        healthRect = new Rect(barStartHoriz + barOutlineSize, barStartVert + barOutlineSize, barWidth - (barOutlineSize*2), barHeight - (barOutlineSize*2));

        //set constants
        fullHealth = PlayerPrefs.GetInt("maxLevel");
        remainingHealth = PlayerPrefs.GetInt("Level");

    }

    void Update()
    {

    }

    void OnGUI()
    {
		useGUILayout = false;

        //update the health bar
        remainingHealth = PlayerPrefs.GetInt("Level");

        GUI.DrawTexture(bgRect, bgBar);
        GUI.DrawTexture(healthRect, healthBar);

		bgRect = new Rect(barStartHoriz, barStartVert, barWidth, barHeight);
		healthRect = new Rect(barStartHoriz + barOutlineSize, barStartVert + barOutlineSize, barWidth - (barOutlineSize*2), barHeight - (barOutlineSize*2));
        healthRect.width = (remainingHealth * (barWidth - (barOutlineSize*2)) * 1.0f) / fullHealth;     

    }

}
