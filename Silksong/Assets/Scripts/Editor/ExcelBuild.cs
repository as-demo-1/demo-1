using System.Collections;
using System.Collections.Generic;
using Excel;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// �����������Ʒϵͳ�ҳ�������Ǳ����Ӧ�������洢
/// </summary>
public class ExcelBuild : Editor
{
        [MenuItem("Assets/Create/ScriptableObjects/WeaponUpdate")]
    public static void CreateWeaponUpdate()
        {
            WeaponUpdate manager = ScriptableObject.CreateInstance<WeaponUpdate>();
            manager.weaponUpgradeInfoList = ExcelReader.CreateWeaponUpgradeInfoWithExcel(WeaponExcelConfig.excelsFolderPath + "weapon.xlsx");
            //manager.playerInventory=
            //ȷ���ļ��д���
            if (!Directory.Exists(WeaponExcelConfig.assetPath))
            {
                Directory.CreateDirectory(WeaponExcelConfig.assetPath);
            }

            string assetPath = string.Format("{0}{1}.asset", WeaponExcelConfig.assetPath, "WeaponUpgrade");
            AssetDatabase.CreateAsset(manager, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
       }

}
