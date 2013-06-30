using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZWaveDeviceBridge.RemoteControl
{
    class ButtonMappingStore
    {
        private string m_name;
        private Dictionary<string, ButtonMapping> m_mappings = new Dictionary<string, ButtonMapping>();
        private IFormatter m_formater = new BinaryFormatter(); 

        public ButtonMappingStore(string name)
        {
            m_name = name;
            Mappings = new ObservableCollection<ButtonMapping>();

            Load();
        }

        public ObservableCollection<ButtonMapping> Mappings { get; private set; }

        public ButtonMapping GetMapping(IEnumerable<int> data)
        {
            string s = GetString(data);

            ButtonMapping mapping;

            m_mappings.TryGetValue(s, out mapping);

            return mapping;
        }

        public void AddMapping(IEnumerable<int> data, ButtonMapping mapping)
        {
            string s = GetString(data);

            if (m_mappings.ContainsKey(s))
            {
                throw new Exception("Mapping already exists for that button");
            }

            Mappings.Add(mapping);
            m_mappings[s] = mapping;
            Save();
        }

        public void DeleteMapping(IEnumerable<int> data)
        {
            Mappings.Remove(m_mappings[GetString(data)]);
            m_mappings.Remove(GetString(data));
            Save();
        }

        private void Load()
        {
            if (File.Exists(FileName))
            {
                using (FileStream fs = File.OpenRead(FileName))
                {
                    m_mappings = (Dictionary<string, ButtonMapping>)m_formater.Deserialize(fs);
                    m_mappings.Values.ToList().ForEach(Mappings.Add);
                }
            }
        }

        private void Save()
        {
            using (FileStream fs = File.OpenWrite(FileName))
            {
                m_formater.Serialize(fs, m_mappings);
            }
        }

        private string FileName
        {
            get
            {
                return Path.Combine("C:\\Users\\bwillard\\Documents\\HomeAutomation", m_name) + ".bmd";
            }
        }


        private string GetString(IEnumerable<int> data)
        {
            return string.Join(",", data.Select(s => s.ToString("00").PadLeft(3)).ToArray());
        }

    }
}
