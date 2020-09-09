using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRenderer : MonoBehaviour
{
    private List<CraftCell> cells;

    private void Start()
    {
        gameObject.SetActive(false); // временный фикс
    }
}
