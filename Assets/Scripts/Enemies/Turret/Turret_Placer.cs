using UnityEngine;
using System.Collections;

public class TurretPlacer : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject turretPreviewPrefab;
    public string placementAreaTag = "PlacementArea";
    public LayerMask placementLayer;

    private GameObject previewInstance;
    private bool isInsidePlacementArea = false;

    private int turretCount = 0; 
    private bool isCooldown = false; 

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition, placementLayer);

        if (hitCollider != null && hitCollider.CompareTag(placementAreaTag) && !isCooldown)
        {
            if (!isInsidePlacementArea)
            {
                isInsidePlacementArea = true;
                ShowPreview(mousePosition);
            }

            if (previewInstance != null)
            {
                previewInstance.transform.position = mousePosition;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTurret(mousePosition);
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
        previewInstance = Instantiate(turretPreviewPrefab, position, Quaternion.identity);
    }

    private void HidePreview()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }
    }

    private void PlaceTurret(Vector2 position)
    {
        if (turretCount < 3)
        {
            Instantiate(turretPrefab, position, Quaternion.identity);
            Debug.Log("Torreta colocada en: " + position);

            turretCount++;
            HidePreview();

            if (turretCount >= 3)
            {
                StartCoroutine(StartCooldown());
            }
        }
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        Debug.Log("Enfriamiento iniciado...");

        yield return new WaitForSeconds(5f); 

        turretCount = 0; 
        isCooldown = false;
        Debug.Log("Enfriamiento terminado. Se pueden colocar más torretas.");
    }
}
