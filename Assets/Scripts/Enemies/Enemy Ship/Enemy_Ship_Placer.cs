using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ship_Placer : MonoBehaviour
{
    public GameObject shipPrefab; // Prefab de la nave enemiga
    public GameObject shipPreviewPrefab; // Prefab para la visualizaci�n previa
    public string placementAreaTag = "PlacementArea"; // Tag para identificar las �reas v�lidas
    public LayerMask placementLayer; // Capa para las �reas de colocaci�n (opcional)

    private GameObject previewInstance; // Instancia actual de la visualizaci�n previa
    private bool isInsidePlacementArea = false; // Verifica si el rat�n est� dentro de un �rea v�lida

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition, placementLayer);

        if (hitCollider != null && hitCollider.CompareTag(placementAreaTag))
        {
            if (!isInsidePlacementArea)
            {
                isInsidePlacementArea = true;
                ShowPreview(mousePosition);
            }

            if (previewInstance != null)
            {
                // Actualizar la posici�n de la preview mientras el rat�n se mueve
                previewInstance.transform.position = mousePosition;
            }

            // Colocar la nave enemiga al hacer clic con el bot�n central del rat�n
            if (Input.GetMouseButtonDown(2))
            {
                PlaceShip(mousePosition);
            }
        }
        else
        {
            if (isInsidePlacementArea)
            {
                isInsidePlacementArea = false;
                HidePreview();
            }
        }
    }

    private void ShowPreview(Vector2 position)
    {
        // Instanciar el prefab de preview y conservar su rotaci�n original
        previewInstance = Instantiate(shipPreviewPrefab, position, shipPreviewPrefab.transform.rotation);
    }

    private void HidePreview()
    {
        // Destruir el prefab de preview si existe
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }
    }

    private void PlaceShip(Vector2 position)
    {
        // Instanciar la nave enemiga y conservar su rotaci�n original
        Instantiate(shipPrefab, position, shipPrefab.transform.rotation);
        Debug.Log("Nave colocada en: " + position);

        // Ocultar la preview despu�s de colocar la nave
        HidePreview();
    }
}
