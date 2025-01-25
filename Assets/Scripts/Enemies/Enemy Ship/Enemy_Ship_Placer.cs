using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ship_Placer : MonoBehaviour
{
    public GameObject shipPrefab; // Prefab de la nave enemiga
    public GameObject shipPreviewPrefab; // Prefab para la visualización previa
    public string placementAreaTag = "PlacementArea"; // Tag para identificar las áreas válidas
    public LayerMask placementLayer; // Capa para las áreas de colocación (opcional)

    private GameObject previewInstance; // Instancia actual de la visualización previa
    private bool isInsidePlacementArea = false; // Verifica si el ratón está dentro de un área válida

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
                // Actualizar la posición de la preview mientras el ratón se mueve
                previewInstance.transform.position = mousePosition;
            }

            // Colocar la nave enemiga al hacer clic con el botón central del ratón
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
        // Instanciar el prefab de preview y conservar su rotación original
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
        // Instanciar la nave enemiga y conservar su rotación original
        Instantiate(shipPrefab, position, shipPrefab.transform.rotation);
        Debug.Log("Nave colocada en: " + position);

        // Ocultar la preview después de colocar la nave
        HidePreview();
    }
}
