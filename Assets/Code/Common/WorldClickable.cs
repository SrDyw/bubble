using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WorldClickable : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private UnityEvent OnClick;

    public SharedDelegate OnClickDelegate;

    public UnityEvent OnClick1 { get => OnClick; set => OnClick = value; }


    // Update is called once per frame
    public virtual void Update()
    {
        // Detectar clic del mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (TryDetect())
            {
                OnClick1?.Invoke();
            }
        }
    }

    public bool TryDetect()
    {
        // Crear un rayo desde la cámara a la posición del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1f);

        // Verificar si el rayo intersecta con algún objeto en el mundo
        if (hit != null)
        {
            foreach (var h in hit)
            {
                if (h.collider == _collider)
                {
                    OnClickDelegate?.Invoke();

                    OnDetected();
                    return true;
                }
            }
        }
        return false;

    }

    public virtual void OnDetected() { }
}
