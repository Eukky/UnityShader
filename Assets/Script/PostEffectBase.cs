using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent( (typeof(Camera)))]
public class PostEffectBase : MonoBehaviour
{
    protected void CheckResources()
    {
        bool isSupported = CheckSupport();
        if (isSupported = false)
        {
            NotSupported();
        }
    }

    protected bool CheckSupport()
    {
        if (SystemInfo.supportsImageEffects == false || SystemInfo.supportsRenderTextures == false)
        {
            Debug.LogWarning("This platform does not supported image effects or render texture");
            return false;
        }
        return true;
    }

    protected void NotSupported()
    {
        enabled = false;
    }

    protected void Start()
    {
        CheckResources();
    }

    protected Material checkShaderAndCreateMaterial(Shader shader, Material material)
    {
        if (shader == null)
        {
            return null;
        }

        if (shader.isSupported && material && material.shader == shader)
        {
            return material;
        }

        if (!shader.isSupported)
        {
            return null;
        }
        else
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            if (material)
            {
                return material;
            }
            else
            {
                return null;
            }
        }
    }
}
