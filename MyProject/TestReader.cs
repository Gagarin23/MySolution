using MyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyProject
{
    class TestReader<T>
        where T : class 
    {
        private string _elementName;
        private Func<string, T> _getModelObjectFunc;
        public TestReader(string elementName, Func<string, T> getModelObjectFunc)
        {
            _elementName = elementName;
            _getModelObjectFunc = getModelObjectFunc;
        }

        /// <summary>
        /// Получить объект по имени из xml-документа. Если имя не найдено, то вернёт NULL !!
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public T GetModelObject(string address)
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                Async = true,
                DtdProcessing = DtdProcessing.Parse
            };

            try
            {
                using (XmlReader reader = XmlReader.Create(address, settings))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == _elementName)
                        {
                            return _getModelObjectFunc(reader.ReadOuterXml());
                        }
                    }
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
