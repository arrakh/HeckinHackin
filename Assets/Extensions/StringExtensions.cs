using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions
{
    /// <summary>
    /// Remove extension from a file name.
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <param name="string"></param>
    public static string RemoveExtension(this string fileName)
    {
        int fileExtPos = fileName.LastIndexOf(".");
        if (fileExtPos >= 0)
            return fileName.Substring(0, fileExtPos);
        else
            return fileName;
    }
}
