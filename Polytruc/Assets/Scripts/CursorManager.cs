using System;
using UnityEditor;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
  public LayerMask moveToMask; // Masque pour détecter les objets cliquables
  public LayerMask speakToMask; // Masque pour détecter les objets cliquables


  [SerializeField] private Texture2D defaultCursorTexture;
  [SerializeField] private Texture2D moveToCursorTexture;
    [SerializeField] private Texture2D speakToCursorTexture;
  [SerializeField] private Texture2D turnLeftCursorTexture;
  [SerializeField] private Texture2D turnRightCursorTexture;

  private Vector2 cursorHotspotCenter;
  private Vector2 cursorHotspotTopLeft;

  void Start()
  {
    cursorHotspotCenter = new Vector2(defaultCursorTexture.width / 2, defaultCursorTexture.height / 2);
    cursorHotspotTopLeft = new Vector2(0, 0);

    Cursor.SetCursor(defaultCursorTexture, cursorHotspotCenter, CursorMode.Auto);

  }

  void Update()
  {

    Cursor.SetCursor(defaultCursorTexture, cursorHotspotTopLeft, CursorMode.Auto);

    if (Functions.IsMouseOnLeft())
    {
      Cursor.SetCursor(turnLeftCursorTexture, cursorHotspotCenter, CursorMode.Auto);
    }
    else if (Functions.IsMouseOnRight())
    {
      // Clic dans la zone droite
      Cursor.SetCursor(turnRightCursorTexture, cursorHotspotCenter, CursorMode.Auto);
    }
    else if (Functions.IsMouseOverObject(moveToMask, out RaycastHit hit))
    {
      Cursor.SetCursor(moveToCursorTexture, cursorHotspotCenter, CursorMode.Auto);
    }
    else if (Functions.IsMouseOverObject(speakToMask, out RaycastHit hit2))
    {
      Cursor.SetCursor(speakToCursorTexture, cursorHotspotCenter, CursorMode.Auto);
    }
  }
}
