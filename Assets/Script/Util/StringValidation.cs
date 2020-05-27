using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringValidation
{
    public static bool IsValidFileName(string str)
    {
        
        return !(str.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)&&str.Length != 0;
    }
}
