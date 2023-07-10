using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    UnityEvent placementUpdate;

    [SerializeField]
    GameObject visualObject;

    private bool isRotating = false;

    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        // Obtém a referência para o ARRaycastManager
        m_RaycastManager = GetComponent<ARRaycastManager>();

        // Cria uma instância de UnityEvent se não houver nenhuma
        if (placementUpdate == null)
            placementUpdate = new UnityEvent();

        // Adiciona o método DiableVisual() como ouvinte para o evento placementUpdate
        placementUpdate.AddListener(DiableVisual);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // Verifica se há um toque na tela e retorna a posição do toque
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                // Instancia o objeto na posição atingida pelo raio
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                // Reposiciona o objeto para a posição atingida pelo raio
                spawnedObject.transform.position = hitPose.position;
            }

            if (Input.touchCount == 2)
            {
                var touch1 = Input.GetTouch(0);
                var touch2 = Input.GetTouch(1);

                if (touch2.phase == TouchPhase.Began)
                {
                    isRotating = true;
                }
                else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
                {
                    isRotating = false;
                }

                if (isRotating)
                {
                    var prevTouchPosition1 = touch1.position - touch1.deltaPosition;
                    var prevTouchPosition2 = touch2.position - touch2.deltaPosition;

                    var prevTouchDelta = (prevTouchPosition2 - prevTouchPosition1).normalized;
                    var touchDelta = (touch2.position - touch1.position).normalized;

                    float rotationAngle = Mathf.Atan2(touchDelta.y, touchDelta.x) - Mathf.Atan2(prevTouchDelta.y, prevTouchDelta.x);
                    spawnedObject.transform.Rotate(Vector3.up, -rotationAngle * Mathf.Rad2Deg);
                }
            }

            // Chama o evento placementUpdate
            placementUpdate.Invoke();
        }
    }

    public void DiableVisual()
    {
        // Desativa o objeto visual
        visualObject.SetActive(false);
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
