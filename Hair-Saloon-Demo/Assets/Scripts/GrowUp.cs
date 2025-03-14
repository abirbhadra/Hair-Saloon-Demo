using UnityEngine;

public class GrowUp : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private float maxGrowth = 2f;
    [SerializeField] private float growthSpeed = 1f;

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
                GrowUp growObject = hitCollider.GetComponent<GrowUp>();
                if (growObject != null && growObject.enabled)
                {
                    // If hidden, enable sprite before growing
                    if (!growObject.spriteRenderer.enabled)
                    {
                        growObject.spriteRenderer.enabled = true;
                    }

                    growObject.Grow();
                }
            }
        }
        else
        {
            isPressing = false;
        }
    }

    public void Grow()
    {
        if (!isEnabled || !isPressing) return;

        float newHeight = transform.localScale.y + growthSpeed * Time.deltaTime;
        if (newHeight <= maxGrowth)
        {
            transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
            transform.position += transform.up * (growthSpeed * Time.deltaTime / 2);
        }
    }
}
