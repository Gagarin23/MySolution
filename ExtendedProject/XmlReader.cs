using System;
using System.Xml;

namespace ExtendedProject
{
    class XmlReader<T>
        where T : class
    {
        public Exception Exception { get; set; }

        /// <summary>
        /// Получить объект по имени из xml-документа. Если имя не найдено, то вернёт NULL !!
        /// </summary>
        /// <param name="address"></param>
        /// <param name="elementName"></param>
        /// <param name="getModelObjectFunc"></param>
        /// <returns></returns>
        protected T GetModelObject(string address, string elementName, Func<string, T> getModelObjectFunc)
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                Async = true,
                DtdProcessing = DtdProcessing.Parse,
                ValidationType = ValidationType.Schema
            };

            using (XmlReader reader = XmlReader.Create(address, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == elementName)
                    {
                        return getModelObjectFunc(reader.ReadOuterXml());
                    }
                }
                return null;
            }
        }
    }
}
