using System.Collections.Generic;
using UnityEngine;

public static class TextProcessorUtils
{
    public const string PARENTHESIS_OPEN = "(";
    public const string PARENTHESIS_CLOSE = ")";
    public const string COMMA = ",";
    public const string QUOTE = "\"";
    public const string NEW_LINE = "\n";
    public const float FLOAT_ERROR = float.NegativeInfinity;
    public static readonly Vector3 VECTOR3_ERROR = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

    public static string GetFunctionName(string input)
    {
        if (IsFunction(input))
        {
            input = input.RemoveAllWhiteSpace();
            return input.Substring(0, input.IndexOf(PARENTHESIS_OPEN));
        }

        return input;
    }

    public static bool IsFunction(string input)
    {
        return input.Contains(PARENTHESIS_OPEN) && input.Contains(PARENTHESIS_CLOSE);
    }

    public static string RemoveParenthesis(string input)
    {
        if (IsFunction(input))
        {
            input = input.Remove(input.IndexOf(PARENTHESIS_OPEN), PARENTHESIS_OPEN.Length);
            input = input.Remove(input.IndexOf(PARENTHESIS_CLOSE), PARENTHESIS_OPEN.Length);
        }

        return input;
    }

    public static List<object> GetParameters(string input, List<object> paramList = null)
    {
        string[] chunks = null;
        object param = null;
        if (paramList == null) paramList = new List<object>();
        input = RemoveParenthesis(input);

        if (!input.Contains(COMMA))
        {
            paramList.Add(param);
        }
        else
        {
            chunks = input.Split(COMMA.FirstLetterToChar());

            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i] = chunks[i].Trim();
                paramList.Add(param);
            }
        }

        return paramList;
    }

    public static List<float> GetFloatParameters(string input, List<float> floatNums = null)
    {
        string[] chunks = null;
        float num = 0;
        if (floatNums == null) floatNums = new List<float>();
        input = RemoveParenthesis(input);

        if (!input.Contains(COMMA))
        {
            if (float.TryParse(input, out num))
                floatNums.Add(num);
        }
        else
        {
            chunks = input.Split(COMMA.FirstLetterToChar());

            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i] = chunks[i].Trim();

                if (float.TryParse(chunks[i], out num))
                    floatNums.Add(num);
            }
        }

        return floatNums;
    }

    public static List<string> GetStringParameters(string input, List<string> stringList = null)
    {
        string[] chunks = null;
        if (stringList == null) stringList = new List<string>();
        input = RemoveParenthesis(input);

        if (!input.Contains(COMMA))
        {
            stringList.Add(input);
        }
        else
        {
            chunks = input.Split(COMMA.FirstLetterToChar());

            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i] = chunks[i].Trim();
                stringList.Add(chunks[i]);
            }
        }

        return stringList;
    }

    public static Vector3 GetVectorParameter(string input)
    {
        List<float> nums = GetFloatParameters(input);
        return nums.Count >= 3 ? new Vector3(nums[0], nums[1], nums[2]) : VECTOR3_ERROR;
    }

    public static bool IsVector3Error(Vector3 vector3)
    {
        return vector3.x == VECTOR3_ERROR.x
            && vector3.y == VECTOR3_ERROR.y
            && vector3.z == VECTOR3_ERROR.z;
    }

    public static string SuccessDisplayString(string message, string input)
    {
        return "Execution succeeded " + NEW_LINE + message + "[" + input + "]: ";
    }

    public static string ErrorDisplayString(string message, string input)
    {
        return "Execution failed " + NEW_LINE + message + "[" + input + "]: ";
    }
}