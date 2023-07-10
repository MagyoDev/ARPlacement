using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager; // Gerenciador de Raycast AR
    [SerializeField]
    private GameObject visual; // Objeto visual do indicador de posicionamento

    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>(); // Obtém a referência para o ARRaycastManager
        visual = transform.GetChild(0).gameObject; // Obtém o objeto visual do indicador de posicionamento a partir do filho

        visual.SetActive(false); // Oculta o indicador de posicionamento visual
    }

    void Update()
    {
        // Lança um raio a partir do centro da tela
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // Se atingirmos uma superfície de plano de RA, atualize a posição e rotação do indicador
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            // Ativa o visual do indicador se estiver desativado
            if (!visual.activeInHierarchy)
                visual.SetActive(true);
        }
    }
}
