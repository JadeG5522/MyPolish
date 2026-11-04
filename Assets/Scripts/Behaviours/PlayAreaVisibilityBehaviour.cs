using UnityEngine;

public class PlayAreaVisibilityBehaviour : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private bool[] initiallyEnabled;
    private bool hasAppliedVisibility;
    private bool wasInsidePlayArea;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        initiallyEnabled = new bool[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            initiallyEnabled[i] = spriteRenderers[i].enabled;
        }

        UpdateVisibility(true);
    }

    private void OnEnable()
    {
        if (spriteRenderers != null)
        {
            UpdateVisibility(true);
        }
    }

    private void Update()
    {
        UpdateVisibility(false);
    }

    private void UpdateVisibility(bool forceUpdate)
    {
        bool isInsideVisibilityArea = GenerateWorldBehaviour.IsPositionInVisibilityArea(transform.position);
        if (!forceUpdate && hasAppliedVisibility && isInsideVisibilityArea == wasInsidePlayArea)
        {
            return;
        }

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].enabled = isInsideVisibilityArea && initiallyEnabled[i];
        }

        wasInsidePlayArea = isInsideVisibilityArea;
        hasAppliedVisibility = true;
    }
}