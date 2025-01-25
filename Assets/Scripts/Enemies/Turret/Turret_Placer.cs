using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI; // Necesario para manipular la imagen tipo fill

public class TurretPlacer : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject turretPreviewPrefab;
    public string placementAreaTag = "PlacementArea";
    public LayerMask placementLayer;

    private GameObject previewInstance;
    private bool isInsidePlacementArea = false;

    public int maxTurrets = 3;
    public int turretsRemaining = 3;
    public int cooldownTime = 5;
    private int turretCount = 0;
    private bool isCooldown = false;

    // UI Variables
    public TMP_Text turretsRemaining_Text;
    public Image cooldownFillImage; 

    private void Start()
    {
        turretsRemaining = maxTurrets;

        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 1;
        }
    }

    void Update()
    {
        turretsRemaining_Text.text = turretsRemaining.ToString();

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
        if (turretCount < maxTurrets)
        {
            Instantiate(turretPrefab, position, Quaternion.identity);
            Debug.Log("Torreta colocada en: " + position);

            turretsRemaining--;
            turretCount++;
            HidePreview();

            if (turretCount >= maxTurrets)
            {
                StartCoroutine(StartCooldown());
            }
        }
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        Debug.Log("Enfriamiento iniciado...");
        turretCount = 0;

        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 0;
        }

        float elapsedTime = 0;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            if (cooldownFillImage != null)
            {
                cooldownFillImage.fillAmount = Mathf.Clamp01(elapsedTime / cooldownTime);
            }
            yield return null;
        }

        turretsRemaining = maxTurrets;
        isCooldown = false;

        Debug.Log("Enfriamiento terminado. Se pueden colocar más torretas.");
    }
}
