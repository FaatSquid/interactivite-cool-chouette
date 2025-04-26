using System;
using Unity.VisualScripting;
using UnityEngine;

public class PointClickCamera : MonoBehaviour
{
    public Transform cameraTarget; // Objet auquel la caméra est attachée
    public LayerMask moveToMask; // Masque pour détecter les objets cliquables
    public LayerMask speakToMask;
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float tiltSpeed = 5f; // Vitesse de l'inclinaison
    public float panSpeed = 5f; // Vitesse du panoramique
    public float rotatingSpeed = 0.1f; // Vitsse pan lorsque clique sur bords ecran
    public Transform playerTransform; // Transform du joueur
    public float rotationAngle = 15f; // Angle de rotation

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool rotating = false;

    private static readonly float[] ANGLES = { 0, 90, 180, -90 };
    private int current_angle_index = 0;
    private BasicMovedToEvent movedToEvent = null;
   

    private void rotate_left()
    {
        current_angle_index -= 1;
        if (current_angle_index < 0)
        {
            current_angle_index = 3;
        }
    }

    private void rotate_right()
    {
        current_angle_index = (current_angle_index + 1) % 4;
    }

    void Start()
    {
        if (cameraTarget != null)
        {
            transform.position = cameraTarget.position;
        }
        targetPosition = transform.position; // Initialiser la position cible
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Functions.IsMouseOnLeft())
        {
            rotate_left();
            rotating = true;
        }
        else if (Input.GetMouseButtonDown(0) && Functions.IsMouseOnRight())
        {
            rotate_right();
            rotating = true;
        }

        // Si la rotation n'a pas été déclenchée, vérifier le déplacement
        if (!rotating && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Functions.IsMouseOverObject(moveToMask, out hit))
            {
                if(movedToEvent){
                    movedToEvent.DisableSpeakToObjects(); // Désactiver les objets speakTo
                }

                targetPosition = hit.transform.position; // Déplacer vers l'objet cliqué
                movedToEvent = hit.transform.GetComponent<BasicMovedToEvent>();
                isMoving = true;
            }
            else if (Functions.IsMouseOverObject(speakToMask, out hit))
            {
                hit.transform.GetComponent<ClickedOnEvent>().OnClick();
            }
        }
        

        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false; // Arrêter le mouvement une fois arrivé
                if (movedToEvent != null)
                {
                    movedToEvent.OnClick();
                }
            }
        }

        // Récupère la position de la souris dans l'écran
        Vector2 mousePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        // Calcul de l'inclinaison et du panoramique basé sur la position de la souris
        float tiltAmount = (mousePos.y - 0.5f) * tiltSpeed; // Inclinaison (haut-bas)
        float panAmount = (mousePos.x - 0.5f) * panSpeed; // Panoramique (gauche-droite)


        float panRotatingAngle = ANGLES[current_angle_index];
        if (rotating)
        {
            float targetYAngle = ANGLES[current_angle_index];
            float currentYAngle = transform.rotation.eulerAngles.y;
            // Convertir les angles dans l'intervalle [0, 360] avant d'utiliser Mathf.LerpAngle
            if (targetYAngle < 0)
            {
                targetYAngle += 360;
            }
            if (currentYAngle < 0)
            {
                currentYAngle += 360;
            }

            panRotatingAngle = Mathf.LerpAngle(currentYAngle, targetYAngle, rotatingSpeed * Time.deltaTime);

            if (Math.Abs(currentYAngle + panAmount - targetYAngle) < 5f)
            {
                rotating = false;
            }
        }

        // Appliquer le mouvement à la caméra
        transform.rotation = Quaternion.Euler(-tiltAmount, panAmount + panRotatingAngle, 0);
    }
}
