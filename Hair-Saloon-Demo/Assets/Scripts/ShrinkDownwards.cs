using UnityEngine;

public class ShrinkDownwards : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private float minYScale = 0.35f;
    private bool hideOnMinScale = false; // Controls whether to hide on min scale
    private bool isPressing = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnValidate()
    {
        enabled = isEnabled;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            isPressing = true;
            Vector3 pressPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pressPosition.z = 0;

            Collider2D[] hitColliders = Physics2D.OverlapPointAll(pressPosition);

            foreach (Collider2D hitCollider in hitColliders)
            {
                ShrinkDownwards shrinkObject = hitCollider.GetComponent<ShrinkDownwards>();
                if (shrinkObject != null && shrinkObject.enabled)
                {
                    shrinkObject.AdjustSize(pressPosition);
                }
            }
        }
        else
        {
            isPressing = false;
        }
    }

    public void AdjustSize(Vector3 pressPosition)
    {
        if (!isEnabled || !isPressing) return;

        Transform objectTransform = transform;
        Vector3 hairBottom = objectTransform.position - objectTransform.up * (objectTransform.localScale.y / 2);
        float newYScale = Mathf.Max(minYScale, Vector3.Dot(pressPosition - hairBottom, objectTransform.up));

        objectTransform.localScale = new Vector3(objectTransform.localScale.x, newYScale, objectTransform.localScale.z);
        objectTransform.position = hairBottom + objectTransform.up * (newYScale / 2);

        // Hide only if hideOnMinScale is true and scale is at min
        if (hideOnMinScale && newYScale <= minYScale)
        {
            Debug.Log("Shrink & Hide mode: Object reached minimum scale. Hiding Sprite.");
            spriteRenderer.enabled = false;
        }
    }

    public void SetHideOnMinScale(bool hide)
    {
        hideOnMinScale = hide;
        Debug.Log($"Hide on Min Scale set to: {hide}");
    }

    public void EnableSpriteRenderer()
    {
        if (spriteRenderer != null && !spriteRenderer.enabled)
        {
            Debug.Log("GrowUp activated: Re-enabling Sprite.");
            spriteRenderer.enabled = true;
        }
    }
}