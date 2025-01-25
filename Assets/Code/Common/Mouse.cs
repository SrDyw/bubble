using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var mouse = Input.mousePosition;
        mouse.z = Camera.main.nearClipPlane; // Establece la distancia de la cámara

        // Convierte las coordenadas del mouse a coordenadas del mundo
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouse);

        // Establece la posición del objeto
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, Camera.main.nearClipPlane);
        

    }
}
