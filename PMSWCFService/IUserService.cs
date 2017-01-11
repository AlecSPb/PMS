using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PMSWCFService.Models;

namespace PMSWCFService
{
    [ServiceContract(Namespace = "http://www.newlifechou.com")]
    public interface IUserService:IServiceBase<UserDc>
    {
        [OperationContract]
        void DoSomething();
    }
}
