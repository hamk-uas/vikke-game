using System.Runtime.CompilerServices;
using UnityEngine;

public class Logger : MonoBehaviour
{

    /// <summary>
    /// <para>Prints out a caller, line number and message on Unity console </para>
    /// <para>Example: Logger.Log(this, {string message}); NOTE! line number acquired automatically</para>
    /// </summary>
    /// <param name="sender">Name of the class. Use <see cref="this"/></param>
    /// <param name="message">Log message</param>
    /// <param name="lineNumber">Value aqquired automatically from <see cref="System.Runtime.CompilerServices"/></param>
    public static void Log(object sender, string message, [CallerLineNumber]int lineNumber = 0)
    {
        print(string.Format("Class: {0}, Line: {1}, Message: '{2}'", sender, lineNumber, message));
    }
}
