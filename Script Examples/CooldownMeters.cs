using UnityEngine;
using System;

[ExecuteInEditMode]
public class CooldownMeters : MonoBehaviour
{
		Rect imageIconRect;
		Rect imageBgRect;
		Rect imageBarRect;
		Rect imageLabelRect;
		Rect imageControlRect;
		Rect pushIconRect;
		Rect pushBgRect;
		Rect pushBarRect;
		Rect pushLabelRect;
		Rect pushControlRect;
		Rect shockIconRect;
		Rect shockBarRect;
		Rect shockControlRect;
		public Texture2D loadingBar;
		public Texture2D loadedBar;
		public Texture2D infiniteLoadedBar;
		public Texture2D imageIcon;
		public Texture2D pushIcon;
		public Texture2D shockIcon;
		public Texture2D imageControlIcon;
		public Texture2D pushControlIcon;
		public Texture2D shockControlIcon;
		public GUISkin guiSkin;
		private float imageCooldownTime;
		private float pushCooldownTime;
		public float xOffset = 3.5f, yOffset = -10f;
		private float imageCooldownLeft;
		private float pushCooldownLeft;
		private float barOutlineSize;
		private float barStartHoriz;
		private float barWidth;
		private float barHeight;
		private float barStartVert1;
		private float barStartVert2;
		private float barStartVert3;
		private float textStart;
		private float textWidth;
		private float imageStart;
		private float imageWidth;
		private float controlImageStart;

		void Start ()
		{
				imageBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert1) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));
				pushBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert2) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));

		}

		void Update ()
		{

		}

		void OnGUI ()
		{
				//initialize instance vars
				barStartHoriz = Screen.width - (barWidth + textWidth + barOutlineSize) + xOffset;
				barOutlineSize = 2;
				barWidth = 128;
				barHeight = 24;
				barStartVert1 = 96 - yOffset;
				barStartVert2 = 64 - yOffset;
				barStartVert3 = 32 - yOffset;
				textStart = barWidth + barStartHoriz + barOutlineSize + xOffset;
				textWidth = 32;
				imageStart = barStartHoriz - (imageWidth + barOutlineSize);
				imageWidth = 35;
				controlImageStart = imageStart - (barOutlineSize + imageWidth);
		
				//initialize image status bar
				imageIconRect = new Rect (imageStart, Screen.height - barStartVert1, imageWidth, barHeight);
				imageBgRect = new Rect (barStartHoriz, Screen.height - barStartVert1, barWidth, barHeight);
				
				imageLabelRect = new Rect (textStart, Screen.height - barStartVert1, textWidth, barHeight);
				imageControlRect = new Rect (controlImageStart, Screen.height - barStartVert1, imageWidth, barHeight);
		
				//initialize push status bar
				pushIconRect = new Rect (imageStart, Screen.height - barStartVert2, imageWidth, barHeight);
				pushBgRect = new Rect (barStartHoriz, Screen.height - barStartVert2, barWidth, barHeight);
				
				pushLabelRect = new Rect (textStart, Screen.height - barStartVert2, textWidth, barHeight);
				pushControlRect = new Rect (controlImageStart, Screen.height - barStartVert2, imageWidth, barHeight);
		
				//initialize shock bar
				shockIconRect = new Rect (imageStart, Screen.height - barStartVert3, imageWidth, barHeight);
				shockBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert3) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));
				shockControlRect = new Rect (controlImageStart, Screen.height - barStartVert3, imageWidth, barHeight);
		
				//set constants
				imageCooldownTime = PlayerPrefs.GetFloat ("imageCooldownTime");
				pushCooldownTime = PlayerPrefs.GetFloat ("pushCooldownTime");
				
				//update the image bar

				imageCooldownLeft = PlayerPrefs.GetFloat ("imageCooldownLeft");

				GUI.DrawTexture (imageBarRect, loadingBar);
				GUI.DrawTexture (imageIconRect, imageIcon);
				GUI.DrawTexture (imageControlRect, imageControlIcon);

				if (imageCooldownLeft > 0) {//still in cooldown
						GUI.Label (imageLabelRect, Math.Round (imageCooldownLeft, 1).ToString (), guiSkin.customStyles [18]);
						GUI.DrawTexture (imageBarRect, loadingBar);
						imageBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert1) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));
						imageBarRect.width = (imageCooldownTime - imageCooldownLeft) * imageBgRect.width / imageCooldownTime;
				} else {//can use ability
						GUI.Label (imageLabelRect, "");
						GUI.DrawTexture (imageBarRect, loadedBar);
			imageBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert1) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));
						imageBarRect.width = 124;
				}

				//update the push bar

				pushCooldownLeft = PlayerPrefs.GetFloat ("pushCooldownLeft");

				GUI.DrawTexture (pushBarRect, loadingBar);
				GUI.DrawTexture (pushIconRect, pushIcon);
				GUI.DrawTexture (pushControlRect, pushControlIcon);

				if (pushCooldownLeft > 0) {//still in cooldown
			GUI.Label (pushLabelRect, Math.Round (pushCooldownLeft, 1).ToString (), guiSkin.customStyles [18]);
						GUI.DrawTexture (pushBarRect, loadingBar);
			pushBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert2) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));
						pushBarRect.width = (pushCooldownTime - pushCooldownLeft) * pushBgRect.width / pushCooldownTime;
				} else {//can use ability
						GUI.Label (pushLabelRect, "");
						GUI.DrawTexture (pushBarRect, loadedBar);
			pushBarRect = new Rect (barStartHoriz + barOutlineSize, (Screen.height - barStartVert2) + barOutlineSize, barWidth - (2 * barOutlineSize), barHeight - (2 * barOutlineSize));
						pushBarRect.width = 124;
				}

				//draw the shock bar
				GUI.DrawTexture (shockBarRect, infiniteLoadedBar);
				GUI.DrawTexture (shockIconRect, shockIcon);
				GUI.DrawTexture (shockControlRect, shockControlIcon);
		}

}
