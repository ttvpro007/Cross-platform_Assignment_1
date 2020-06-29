using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum FunctionClass
{
    Default,
    Utility,
    SceneObject_1_StringParam,
    CustomMathf_2_FloatParams,
    Movement_1_FloatParam,
    Movement_1_Vector3Param,
}

public enum FuntionReturnType
{
    Void,
    Int,
    Float,
    String,
    List_Int,
    List_Float,
    List_String,
}

public enum FunctionParamSignature
{
    No_Param,
    Float_One_Param,
    Float_Two_Param,
    String_One_Param,
    String_Two_Param,
    Vector3_One_Param,
}

public static class CustomCodeExecutor
{
    public static readonly string[] CALCULATION_FUNCTIONS_2_FLOAT_PARAMS = { "Add", "Sub", "Mul", "Div" };
    public static readonly string[] SCENEOBJECT_FUNCTIONS_1_STRING_PARAM = { "Select" };
    public static readonly string[] MOVEMENT_FUNCTIONS_1_VECTOR3_PARAM = { "MoveTo", "Move" };
    public static readonly string[] MOVEMENT_FUNCTIONS_1_FLOAT_PARAM = { "MoveUp", "MoveDown", "MoveLeft", "MoveRight", "MoveForward", "MoveBackward" };
    public static readonly string[] UTILITY_FUNCTIONS_NO_PARAM = { "Clear", "Quit" };

    private static TextProcessor textProcessor = null;
    private static SceneGameObjects sceneGameObjects = null;

    public static string ExecuteCommand(string input)
    {
        if (!textProcessor)
            textProcessor = Object.FindObjectOfType<TextProcessor>();

        if (!sceneGameObjects)
            sceneGameObjects = Object.FindObjectOfType<SceneGameObjects>();

        string result = string.Empty;
        Movement m = null;

        switch (GetFunctionClass(input))
        {
            case FunctionClass.Default:
                result = input;
                break;
            case FunctionClass.Utility:
                result = GetUtility_NoParam_Result(input, textProcessor);
                break;
            case FunctionClass.SceneObject_1_StringParam:
                result = GetSceneObjectResult_1_StringParam(input, sceneGameObjects);
                break;
            case FunctionClass.CustomMathf_2_FloatParams:
                result = GetCustomMathfResult_2_FloatParams(input);
                break;
            case FunctionClass.Movement_1_FloatParam:
                m = GetMovingObject();
                result = GetMovementResult_1_FloatParam(input, m);
                break;
            case FunctionClass.Movement_1_Vector3Param:
                m = GetMovingObject();
                result = GetMovementResult_1_Vector3Param(input, m);
                break;
        }

        return result;
    }

    private static Movement GetMovingObject()
    {
        Movement m = sceneGameObjects.SelectedGameObject.GetComponent<Movement>();
        if (!m) m = sceneGameObjects.SelectedGameObject.AddComponent<Movement>();
        return m;
    }

    public static FunctionClass GetFunctionClass(string input)
    {
        string functionName = TextProcessorUtils.GetFunctionName(input);

        if (UTILITY_FUNCTIONS_NO_PARAM.Contains(functionName))
        {
            return FunctionClass.Utility;
        }
        else if (SCENEOBJECT_FUNCTIONS_1_STRING_PARAM.Contains(functionName))
        {
            return FunctionClass.SceneObject_1_StringParam;
        }
        else if (CALCULATION_FUNCTIONS_2_FLOAT_PARAMS.Contains(functionName))
        {
            return FunctionClass.CustomMathf_2_FloatParams;
        }
        else if (MOVEMENT_FUNCTIONS_1_FLOAT_PARAM.Contains(functionName))
        {
            return FunctionClass.Movement_1_FloatParam;
        }
        else if (MOVEMENT_FUNCTIONS_1_VECTOR3_PARAM.Contains(functionName))
        {
            return FunctionClass.Movement_1_Vector3Param;
        }

        return FunctionClass.Default;
    }

    public static bool ExecuteUtilityFunction_NoParam(string input, TextProcessor instance) // TODO change text processor to helper class
    {
        if (!string.IsNullOrEmpty(input) && TextProcessorUtils.IsFunction(input))
        {
            string functionName = TextProcessorUtils.GetFunctionName(input);
            MethodInfo method = instance.GetType().GetMethod(functionName, BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(instance, null);
            return true;
        }
        return false;
    }
    public static string GetUtility_NoParam_Result(string input, TextProcessor instance) // TODO change text processor to helper class
    {
        bool success = ExecuteUtilityFunction_NoParam(input, instance);
        return success ? string.Empty : TextProcessorUtils.ErrorDisplayString("Error ", input);
    }

    public static bool ExecuteSceneObjectFunction_1_StringParam(string input, out string objectName, SceneGameObjects instance)
    {
        if (!string.IsNullOrEmpty(input))
        {
            string functionName = TextProcessorUtils.GetFunctionName(input);
            input = input.Remove(input.IndexOf(functionName[0]), functionName.Length);
            List<string> strings = TextProcessorUtils.GetStringParameters(input);

            if (strings.Count >= 1)
            {
                objectName = strings[0];
                MethodInfo method = instance.GetType().GetMethod(functionName, BindingFlags.Public | BindingFlags.Instance);
                return (bool) method.Invoke(instance, new object[] { strings[0] });
            }
        }

        objectName = string.Empty;
        return false;
    }
    public static string GetSceneObjectResult_1_StringParam(string input, SceneGameObjects instance)
    {
        string objectName = string.Empty;
        bool success = ExecuteSceneObjectFunction_1_StringParam(input, out objectName, instance);
        return success ? TextProcessorUtils.SuccessDisplayString("Selected a ", objectName) : TextProcessorUtils.ErrorDisplayString("Error ", input);
    }

    public static float ExecuteCustomMathfFunction_2_FloatParams(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            string functionName = TextProcessorUtils.GetFunctionName(input);
            input = input.Remove(input.IndexOf(functionName[0]), functionName.Length);
            List<float> nums = TextProcessorUtils.GetFloatParameters(input);

            if (nums.Count >= 2)
            {
                MethodInfo method = typeof(CustomMathf).GetMethod(functionName, BindingFlags.Public | BindingFlags.Static);
                return (float)method.Invoke(null, new object[] { nums[0], nums[1] });
            }
        }

        return float.NegativeInfinity;
    }
    public static string GetCustomMathfResult_2_FloatParams(string input)
    {
        float calculationResult = ExecuteCustomMathfFunction_2_FloatParams(input);
        string resultString = TextProcessorUtils.SuccessDisplayString("Result of ", input);
        return calculationResult != TextProcessorUtils.FLOAT_ERROR ? resultString + calculationResult : TextProcessorUtils.ErrorDisplayString("Error ", input);
    }

    public static Vector3 ExecuteMovementFunction_1_FloatParam(string input, MonoBehaviour instance)
    {
        if (!string.IsNullOrEmpty(input))
        {
            string functionName = TextProcessorUtils.GetFunctionName(input);
            input = input.Remove(input.IndexOf(functionName[0]), functionName.Length);
            List<float> nums = TextProcessorUtils.GetFloatParameters(input);
            Movement m = (Movement)instance;

            if (nums.Count >= 1)
            {
                MethodInfo method = typeof(Movement).GetMethod(functionName, BindingFlags.Public | BindingFlags.Instance);
                instance.StartCoroutine((IEnumerator)method.Invoke(instance, new object[] { nums[0] }));
                return m.Destination;
            }
        }

        return TextProcessorUtils.VECTOR3_ERROR;
    }
    public static string GetMovementResult_1_FloatParam(string input, MonoBehaviour instance)
    {
        Vector3 position = ExecuteMovementFunction_1_FloatParam(input, instance);
        string resultString = TextProcessorUtils.SuccessDisplayString("", input);
        return !TextProcessorUtils.IsVector3Error(position) ? resultString + position : TextProcessorUtils.ErrorDisplayString("Error ", input);
    }

    public static Vector3 ExecuteMovementFunction_1_Vector3Param(string input, MonoBehaviour instance)
    {
        if (!string.IsNullOrEmpty(input))
        {
            string functionName = TextProcessorUtils.GetFunctionName(input);
            input = input.Remove(input.IndexOf(functionName[0]), functionName.Length);
            Vector3 position = TextProcessorUtils.GetVectorParameter(input);
            Movement m = (Movement)instance;

            if (!TextProcessorUtils.IsVector3Error(position))
            {
                MethodInfo method = typeof(Movement).GetMethod(functionName, BindingFlags.Public | BindingFlags.Instance);
                instance.StartCoroutine((IEnumerator)method.Invoke(instance, new object[] { position }));
                return position;
            }
        }

        return TextProcessorUtils.VECTOR3_ERROR;
    }
    public static string GetMovementResult_1_Vector3Param(string input, MonoBehaviour instance)
    {
        Vector3 position = ExecuteMovementFunction_1_Vector3Param(input, instance);
        string resultString = TextProcessorUtils.SuccessDisplayString("", input);
        return !TextProcessorUtils.IsVector3Error(position) ? resultString + position : TextProcessorUtils.ErrorDisplayString("Error ", input);
    }
}