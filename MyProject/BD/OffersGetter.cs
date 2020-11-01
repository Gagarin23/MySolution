using MyProject.Model;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MyProject.BD
{
    /// <summary>
    /// Получить offers из xml документа.
    /// </summary>
    class OffersGetter : XmlReader<SurrogateOffers>, IDisposable //Простоечный класс с наследованием, по моему мнению, нужен для того,
                                                                 //чтобы в дальнейшем избежать проблем, если у класса предка изменится сигнатура.
    {
        private SurrogateOffers _offers;
        public SurrogateOffers Offers
        {
            get => _offers;
            set
            {
                if (value == null && Exception == null)
                    Console.WriteLine("Тип offers не найден в xml документе.");

                else if (value == null && Exception != null)
                    Console.Write("Ошибка чтения xml документа! " + Exception.Message);

                else
                    _offers = value;
            }
        }
        public OffersGetter(string url, string searchElement)
        {
            Offers = GetModelObject(url, searchElement, LoadOffers);
        }

        private SurrogateOffers LoadOffers(string xml)
        {
            var serializer = new XmlSerializer(typeof(SurrogateOffers));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (SurrogateOffers)serializer.Deserialize(ms); // Запаковка в object при возврате крайне не радует :(
            }
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
