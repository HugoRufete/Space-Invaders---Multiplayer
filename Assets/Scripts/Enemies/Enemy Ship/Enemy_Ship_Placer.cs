using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_Ship_Placer : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject shipPreviewPrefab;
    public string placementAreaTag = "PlacementArea";
    public LayerMask placementLayer;

    private GameObject previewInstance;
    private bool isInsidePlacementArea = false;

    // Cooldown variables
    public int maxShips = 3;
    public float cooldownTime = 5f;
    private int placedShips = 0;
    private bool isCooldown = false;
    public int shipsRemaining;

    // UI Variable for cooldown feedback
    public Image cooldownFillImage;
    public TMP_Text shipsRemaining_Text;

    private void Start()
    {
        shipsRemaining = maxShips;

        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 1;
        }
    }

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
        previewInstance = Instantiate(shipPreviewPrefab, position, shipPreviewPrefab.transform.rotation);
    }

    private void HidePreview()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }
    }

    private void PlaceShip(Vector2 position)
    {
        if (placedShips < maxShips)
        {
            Instantiate(shipPrefab, position, shipPrefab.transform.rotation);
            Debug.Log("Nave colocada en: " + position);

            placedShips++;
            shipsRemaining--;

            if (placedShips >= maxShips)
            {
                StartCoroutine(CooldownRoutine());
            }

            HidePreview();
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isCooldown = true;

        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 0;
        }

        float elapsedTime = 0f;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;

            if (cooldownFillImage != null)
            {
                cooldownFillImage.fillAmount = Mathf.Clamp01(elapsedTime / cooldownTime);
            }

            yield return null;
        }

        shipsRemaining = maxShips;
        placedShips = 0;
        isCooldown = false;

        if (cooldownFillImage != null)
        {
            // Ensure the fill is full after the cooldown
            cooldownFillImage.fillAmount = 1;
        }

        Debug.Log("Cooldown terminado. Puedes colocar más naves.");
    }
}
