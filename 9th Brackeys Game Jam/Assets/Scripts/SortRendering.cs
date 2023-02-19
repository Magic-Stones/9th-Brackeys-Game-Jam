using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortRendering : MonoBehaviour
{
    [SerializeField] private int sortingOrder = 5000;
    [SerializeField] private int offsetSorting = 1;
    [SerializeField] private bool runOnce = false;

    private Renderer sortRenderer;
    private float maxRunTimer = 0.1f;
    private float runTimer;

    void Awake()
    {
        sortRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        runTimer = maxRunTimer;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        runTimer -= Time.deltaTime;
        if (runTimer <= 0f)
        {
            runTimer = maxRunTimer;
            sortRenderer.sortingOrder = (int)(sortingOrder - transform.position.y - offsetSorting);
        }

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
