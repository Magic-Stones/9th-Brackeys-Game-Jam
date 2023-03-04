using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSorting : MonoBehaviour
{
    [SerializeField] private int setSortingOrder = 5000;
    [SerializeField] private int sortOffset = 1;

    private const float _MAX_SORTING_TIMER = 0.1f;
    private float _runTimer;

    private Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _runTimer = _MAX_SORTING_TIMER;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SortPositionRendering();
    }

    private void SortPositionRendering()
    {
        _runTimer -= Time.deltaTime;
        if (_runTimer <= 0f)
        {
            _runTimer = _MAX_SORTING_TIMER;
            _renderer.sortingOrder = (int)(setSortingOrder - transform.position.y - sortOffset);
        }
    }
}
