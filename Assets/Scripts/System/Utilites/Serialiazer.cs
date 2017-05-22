using System.IO;
using System;
using UnityEngine;
using System.Xml.Serialization;

namespace Utilites.Serialiazer
{
    public static class Serialiazer
    {
        public static void SerialiazeToXml<T>(ref T inObject, string inFileName)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(T));
                StreamWriter file = new StreamWriter(inFileName);
                writer.Serialize(file, inObject);
                file.Close();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public static void DeserialiazitionFromXml<T>(ref T inObject, string inFileName)
        {
            if (File.Exists(inFileName))
            {
                XmlSerializer reader = new XmlSerializer(typeof(T));
                StreamReader file = new StreamReader(inFileName);
                inObject = (T)reader.Deserialize(file);
                file.Close();
            }
            else
            {
                return;
            }
        }
    }
}

