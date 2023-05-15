using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WPF.Models;

namespace Test.WPF.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static ObservableCollection<Notes> ToObservableCollection(this IEnumerable<Notes> collection)
        {
            var oCollection = new ObservableCollection<Notes>();

            foreach (var item in collection)
            {
                oCollection.Add(item);
            }

            return oCollection;
        }
    }
}
