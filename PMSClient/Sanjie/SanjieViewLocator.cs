using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
namespace PMSClient.Sanjie
{
    public class SanjieViewLocator
    {
        public SanjieViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
        }





    }
}
