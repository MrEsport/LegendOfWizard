using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputActionAssetExtension
{
    public static InputActionReference[] GetAllInputActionReferences(this InputActionAsset m_inputAsset)
    {
        var inputActionReferences = new InputActionReference[m_inputAsset.Count()];
        if (inputActionReferences.Length == 0) return inputActionReferences;

        int index = 0;
        Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(m_inputAsset));
        foreach (Object obj in subAssets)
        {
            // there are 2 InputActionReference returned for each InputAction in the asset, need to filter to not add the hidden one generated for backward compatibility
            if (obj is InputActionReference inputActionReference && (inputActionReference.hideFlags & HideFlags.HideInHierarchy) == 0)
            {
                inputActionReferences[index++] = inputActionReference;
            }
        }
        return inputActionReferences;
    }
}
