﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TPToolsLibrary
{
    public static class Logger
    {
        private static readonly string _fileLocation = @"C:\%USERPROFILE%\Desktop\TPTools.log";


        public static void LogError(string error)
        {
            try
            {
                File.AppendAllText(_fileLocation, "Error: " + DateTime.Now.ToString() + " : " + error + "/n");
            }
            catch
            {

            }
            
        }


        public static void LogInfo(string info)
        {
            try
            {
                File.AppendAllText(_fileLocation, "Info: " + DateTime.Now.ToString() + " : " + info + "/n");
            }
            catch
            {

            }
            
        }
    }
}
