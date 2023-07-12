using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class ConvertImageToMatrix : MonoBehaviour
{
    [SerializeField] Texture2D[] _texture2Ds;

    public string ConvertToMatrixString(Texture2D t)
    {
        string matrixString = "";
        matrixString += "[";
        for (int i = t.height - 1; i >= 0; i--)
        {
            matrixString += "{";
            for (int j = 0; j < t.width; j++)
            {
                if (j == t.width - 1)
                    matrixString += (t.GetPixel(j, i) == Color.white) ? "1" : "0";
                else
                    matrixString += (t.GetPixel(j, i) == Color.white) ? "1," : "0,";
            }
            if (i == 0)
                matrixString += "}";
            else
                matrixString += "},";
        }
        matrixString += "]";
        return matrixString;
    }

    public void Convert()
    {
        for (int i = 0; i < _texture2Ds.Length; i++)
        {
            using (var writer = File.CreateText($"Assets/Resources/Levels/Level_{_texture2Ds[i].name}.txt"))
            {
                writer.Write(ConvertToMatrixString(_texture2Ds[i]));
            };
        }
    }
}
