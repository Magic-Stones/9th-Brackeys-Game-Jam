using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> greatTrees;
    private CursedTree cursedTree;

    [SerializeField] private List<GameObject> spawners;

    public delegate void VoidDelegates();
    public VoidDelegates OnGameWin;
    private bool _activeOnGameWin = false;
    private bool _isCurseBroken = false;
    public VoidDelegates OnGameLose;
    private bool _activeOnGameLose = false;

    // Start is called before the first frame update
    void Start()
    {
        cursedTree = greatTrees[0].GetComponent<CursedTree>();

        cursedTree.OnTreeDestroy += () => _isCurseBroken = true;
        OnGameWin += BeginSprout;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCurseBroken && !_activeOnGameWin)
        {
            OnGameWin?.Invoke();
            _activeOnGameWin = true;
        }
    }

    private void BeginSprout()
    {
        greatTrees[0].SetActive(false);
        greatTrees[1].SetActive(true);

        spawners[0].SetActive(false);
        spawners[1].SetActive(true);
    }
}
