using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Waka.Brokers;
using Waka.Models;

namespace Waka.Services
{
    public class CategoryFetcher
    {

        public IEnumerable<string> FetchCategory(List<string> cats)
        {
            
            List<string> categ = new List<string>();
            //string j = string.Empty;
            foreach (var cat in cats)
            {
                if (categ.Contains(cat))
                {

                }
                else
                {

                    categ.Add(cat);
                };
            }
            return categ;

        }
        
    }
}
