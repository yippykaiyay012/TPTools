using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TPToolsLibrary
{
    public static class Logger
    {
        private static readonly string _fileLocation = @"C:\TPTools.log";


        public static void LogError(string error)
        {
            File.AppendAllText(_fileLocation, "Error: " + DateTime.Now.ToString() + " : " + error);
        }


        public static void LogInfo(string info)
        {
            File.AppendAllText(_fileLocation, "Info: " + DateTime.Now.ToString() + " : " + info);
        }
    }
}
