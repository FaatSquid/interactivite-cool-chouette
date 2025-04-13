using UnityEngine;

public static class Functions
{
  public static bool IsMouseOnLeft()
  {
    float screenWidth = Screen.width;
    float clickX = Input.mousePosition.x;
    return clickX < screenWidth * Constants.SCREEN_TURN_AREA_RATIO;
  }

  public static bool IsMouseOnRight()
  {
    float screenWidth = Screen.width;
    float clickX = Input.mousePosition.x;
    return clickX > screenWidth * (1 - Constants.SCREEN_TURN_AREA_RATIO);
  }

  public static bool IsMouseOverObject(LayerMask objMask, out RaycastHit hit)
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    return Physics.Raycast(ray, out hit, Mathf.Infinity, objMask);
  }


}