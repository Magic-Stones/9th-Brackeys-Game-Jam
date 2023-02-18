using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private float instantiateTimer, 
                  textFloatTime = 0.2f, 
                  fontSize, setFontSize;

    private static int sortingOrder;

    private TextMeshPro textMeshPro;
    private Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        PopUpAnimation();
    }

    public static DamagePopUp Instantiate(Vector3 position, int damageAmount)
    {
        Transform damagePopUpObject = Instantiate(GameAssets.asset.damagePopUpPrefab, position, Quaternion.identity).GetComponent<Transform>();

        DamagePopUp damagePopUp = damagePopUpObject.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount);

        return damagePopUp;
    }

    private void Setup(int damageAmount)
    {
        textMeshPro.SetText(damageAmount.ToString());
        textColor = textMeshPro.color;
        instantiateTimer = 0.5f;

        sortingOrder++;
        textMeshPro.sortingOrder = sortingOrder;

        fontSize = 0f;
        setFontSize = 5.5f;
    }

    private void PopUpAnimation()
    {
        instantiateTimer -= Time.deltaTime;
        if (instantiateTimer > 0f)
        {
            textFloatTime -= Time.deltaTime;
            if (textFloatTime > 0f)
            {
                float yFloatSpeed = 5f;
                transform.position += new Vector3(0f, yFloatSpeed, 0) * Time.deltaTime;

                if (fontSize <= setFontSize)
                {
                    textMeshPro.fontSize = fontSize;
                    fontSize++;
                }
            }
        }
        else
        {
            float reduceAlphaSpeed = 3f;
            textColor.a -= reduceAlphaSpeed * Time.deltaTime;
            textMeshPro.color = textColor;

            if (textColor.a <= 0f)
                Destroy(gameObject);
        }
    }
}
