using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Start and Update function
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    private static GameAssets _asset;

    public static GameAssets asset
    {
        get
        {
            if (_asset == null)
                _asset = (Instantiate(Resources.Load("Game Assets")) as GameObject).GetComponent<GameAssets>();
            return _asset;
        }
    }

    [SerializeField] private List<GameObject> _prefabAssets;

    public Transform DamagePopUpAsset { get { return _prefabAssets[0].transform; } }
}
