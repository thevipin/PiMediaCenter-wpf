using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiMediaCenter.Ini_File;

namespace PiMediaCenter
{
    class SharedPreferance
    {
        static IniFile iniFile=new IniFile("config.ini");
        string section;
        public SharedPreferance(string name="default")
        {
            section = name;
        }
        public void put(string key,string value)
        {
            iniFile.Write(key, value, section);
        }
        public string get(string key,string defaultValue)
        {
            if (iniFile.KeyExists(key,section))
            {
                string value = iniFile.Read(key, section);
                return (value);
            }
            iniFile.Write(key, defaultValue, section);
            return defaultValue;
        }
    }
}
