using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritePixelsPerUnitSetTo120 : AssetPostprocessor
{
    /// <summary>
    /// pixel���������ó��˵�120�������Ͽ����������޸ĳ���Ҫ�Ĵ�С�������޸ĳɱ����ƻ�ֲܿ�
    /// </summary>
    void OnPreprocessTexture()
    {
        //Debug.Log("pre");
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.spritePixelsPerUnit = 120;

    }


}
