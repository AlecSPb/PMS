using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;


namespace PMSWCFService
{
    [ServiceContract]
    public interface IDrawingService
    {
        [OperationContract]
        List<DcDrawing> GetDrawing(int s, int t, string drawingName, string customer, string mainDimension);
        [OperationContract]
        int GetDrawingCount(string drawingName, string customer, string mainDimension);

        [OperationContract]
        void AddDrawing(DcDrawing model);
        [OperationContract]
        void UpdateDrawing(DcDrawing model);
    }
}
