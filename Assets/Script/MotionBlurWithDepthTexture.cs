using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MotionBlurWithDepthTexture : PostEffectBase
{
    public Shader motionBlurShader;
    private Material motionblurMaterial = null;

    public Material material
    {
        get
        {
            motionblurMaterial = checkShaderAndCreateMaterial(motionBlurShader, motionblurMaterial);
            return motionblurMaterial;
        }
    }

    private Camera myCamera;

    public Camera camera {
        get
        {
            if (myCamera == null)
            {
                myCamera = GetComponent<Camera>();
            }

            return myCamera;
        }
    }

    [Range(0.0f, 1.0f)] public float blurSize = 0.5f;
    private Matrix4x4 previousViewProjectionMatrix;

    private void OnEnable()
    {
        camera.depthTextureMode |= DepthTextureMode.Depth;
        previousViewProjectionMatrix = camera.projectionMatrix * camera.worldToCameraMatrix;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_BlurSize", blurSize);
            material.SetMatrix("_PreviousViewProjectionMatrix", previousViewProjectionMatrix);
            Matrix4x4 currentViewProjectionMatrix = camera.projectionMatrix * camera.worldToCameraMatrix;
            Matrix4x4 currentViewProjectionInverseMatrix = currentViewProjectionMatrix.inverse;
            material.SetMatrix("_CurrentViewProjectionInverseMatrix", currentViewProjectionInverseMatrix);
            previousViewProjectionMatrix = currentViewProjectionMatrix;
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
