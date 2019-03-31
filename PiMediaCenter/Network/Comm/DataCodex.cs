using System;
using System.Collections.Generic;
using System.Text;

namespace PiMediaCenter.Network.Comm
{
    public class DataCodex
    {
        List<string> dataKey = new List<string>();
        List<string> dataValue = new List<string>();
        string codex;
        string Identifier;
        public int Socketindex;
        public DataCodex(String identifier)
        {
            Identifier = identifier;
        }
        public DataCodex()
        {

        }
        public void setIdentifier(string identity)
        {
            Identifier = identity;
        }
        public void addKeys(params String[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                dataKey.Add(param[i]);
            }
        }
        public List<string> getKeys()
        {
            return dataKey;
        }
        public void addValues(params String[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                dataValue.Add(param[i]);
            }
        }
        public String getIdentifier()
        {
            return (Identifier);
        }
        public void put(String key, String value)
        {
            dataKey.Add(key);
            dataValue.Add(value);
        }
        public String get(String key)
        {
            return (dataValue[dataKey.IndexOf(key)]);
        }
        public String getCodex()
        {
            Encode();
            return codex;
        }
        public void putCodex(string code)
        {
            dataKey.Clear();
            dataValue.Clear();
            this.codex = code;
            Decode();
        }
        public List<string> Keys()
        {
            return dataKey;
        }
        private void Encode()
        {
            StringBuilder code = new StringBuilder("");
            code.Append("vipin's codex#" + Identifier + ":{");
            for (int i = 0; i < dataKey.Count; i++)
            {
                code.Append("&" + dataKey[i].Replace("\n","").Replace("\r","") + "=" + dataValue[i].Replace("\n", "").Replace("\r", "") + ";");
            }
            code.Append("}");
            //Can Write code for Encipher
            codex = code.ToString();
        }
        private void Decode()
        {
            string code = codex;
            int fromIndex = 0;
            Identifier = code.Substring(fromIndex = code.IndexOf("#", fromIndex) + 1, (fromIndex = code.IndexOf(":", fromIndex)- fromIndex));
            int lastIndex = code.LastIndexOf(";");
            while (fromIndex < lastIndex)
            {
                
                int tfromIndex = (fromIndex= code.IndexOf("&", fromIndex) + 1);
                int toIndex = ((fromIndex = code.IndexOf("=", fromIndex)));
                string Key = code.Substring(tfromIndex, toIndex - tfromIndex);
                dataKey.Add(Key);

                tfromIndex = (fromIndex + 1);
                toIndex = (fromIndex = code.IndexOf(";", fromIndex));
                string value = code.Substring(tfromIndex, toIndex - tfromIndex);
                dataValue.Add(value);
            }
        }

    }
}
