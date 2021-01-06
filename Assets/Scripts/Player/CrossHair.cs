using UnityEngine;
using System.Collections;
 
public class CrossHair : MonoBehaviour {
 
public Texture2D cursorImage;
private int cursorWidth = 64;
private int cursorHeight = 64;
private GameObject camera;
 
void Start() {
   camera = GameObject.Find("WeaponCamera");
   Cursor.visible = false;
 
}
 
 
void OnGUI() {
    // Vector3 position = transform.position;
    // position.z = position.z += 0.1f;
    // // Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.7F, 0));
    // Vector3 screenPos = Camera.main.transform.position;
    // screenPos.y = Screen.height - screenPos.y; //The y coordinate on screenPos is inverted so we need to set it straight

    // GUI.DrawTexture(new Rect(screenPos.x, screenPos.y, cursorWidth, cursorHeight), cursorImage);

 //Ray myRay = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.7F, 0));
 Vector3 CrossHairPosition = Camera.main.ViewportToScreenPoint(new Vector3(0.5F, 0.7F, 1f));
 CrossHairPosition.y = Screen.height - CrossHairPosition.y;        
 GUI.Label(new Rect(CrossHairPosition.x - (cursorWidth / 2), CrossHairPosition.y - (cursorHeight/ 2),cursorWidth,cursorHeight),cursorImage); //Crosshair is 2D texture
}
}