
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicMovedToEvent : ClickedOnEvent
{
    // Lists to store objects you want to show when clicked
    public List<GameObject> speakToObjects;

  public override void OnClick()
  {
    Debug.Log("Sphere clicked: " + gameObject.name);

    // Activate all speakTo objects
    foreach (var obj in speakToObjects)
    {
      Collider col = obj.GetComponent<Collider>();
      if (col != null)
      {
        Debug.Log("Enabling collider for: " + obj.name);
        col.enabled = true;
      }
    }

  }

  public void DisableSpeakToObjects()
  {
    // Deactivate all speakTo objects
    foreach (var obj in speakToObjects)
    {
      Collider col = obj.GetComponent<Collider>();
      if (col != null)
      {
        Debug.Log("Disabling collider for: " + obj.name);
        col.enabled = false;
      }
    }
  }
}