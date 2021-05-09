using System.Collections.Generic;

namespace Cash.Logic.XmlRepositories
{
    public class XmlProductRepository
    {
        public List<XmlProduct> Procucts { get; set; }

        public XmlProductRepository()
        {
            Procucts = new List<XmlProduct>();
        }
    }
}
