using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModels
{
    public class ConnectionPackage
    {
        public string Name { get; set; }
        public string PublicKey { get; set; }

        public ConnectionPackage()
        {

        }
        public ConnectionPackage(string name, string publicKey)
        {
            Name = name;
            PublicKey = publicKey;
        }
    }
}
