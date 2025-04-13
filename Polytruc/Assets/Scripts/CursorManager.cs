using System;
using UnityEditor;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
  public LayerMask moveToMask; // Masque pour détecter les objets cliquables
  [SerializeField] private Texture2D defaultCursorTexture;
  [SerializeField] private Texture2D moveToCursorTexture;
  [SerializeField] private Texture2D turnLeftCursorTexture;
  [SerializeField] private Texture2D turnRightCursorTexture;

  private Vector2 cursorHotspot;

  void Start()
  {
    cursorHotspot = new Vector2(defaultCursorTexture.width / 2, defaultCursorTexture.height / 2);
    Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.Auto);

  }

  void Update()
  {


    Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.Auto);

    if (Functions.IsMouseOnLeft())
    {
      Cursor.SetCursor(turnLeftCursorTexture, cursorHotspot, CursorMode.Auto);
    }
    else if (Functions.IsMouseOnRight())
    {
      // Clic dans la zone droite
      Cursor.SetCursor(turnRightCursorTexture, cursorHotspot, CursorMode.Auto);
    }
    if (Functions.IsMouseOverObject(moveToMask, out RaycastHit hit))
    {
      Cursor.SetCursor(moveToCursorTexture, cursorHotspot, CursorMode.Auto);
    }
  }
}
