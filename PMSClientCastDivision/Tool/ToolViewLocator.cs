using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Tool
{
    public class ToolViewLocator
    {
        public ToolViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MaterialNeedCalculationView>();
            SimpleIoc.Default.Register<LabelOutPutView>();
            SimpleIoc.Default.Register<DensityEstamatorView>();
        }

        public MaterialNeedCalculationView MaterialNeedCalculation
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedCalculationView>(); }
        }

        public LabelOutPutView LabelOutPut
        {
            get { return SimpleIoc.Default.GetInstance<LabelOutPutView>(); }
        }

        public DensityEstamatorView DensityEstamator
        {
            get
            {
                return SimpleIoc.Default.GetInstance<DensityEstamatorView>();
            }
        }
    }
}
