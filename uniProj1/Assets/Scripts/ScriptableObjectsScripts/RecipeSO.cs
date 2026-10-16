using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSoList;
    public string recipeName;
}
