using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ISampleService
    {
        //[OperationContract]
        //List<DcSample> GetSample(int s, int t, string productid, string composition);

        //[OperationContract]
        //int GetSampleCount(string productid, string composition);

        [OperationContract]
        List<DcSample> GetSampleAll(int s, int t, string pminumber, string sampleid, string productid, string composition, string trackingstage);

        [OperationContract]
        int GetSampleAllCount(string pminumber, string sampleid, string productid, string composition, string trackingstage);

        [OperationContract]
        List<DcSample> GetSampleBySampleID(string sampleid);

        [OperationContract]
        int GetSampleByPMINumberCount(string pminumber);

        [OperationContract]
        void AddSample(DcSample model);
        [OperationContract]
        void UpdateSample(DcSample model);



        [OperationContract]
        List<DcDeliveryItemSampleCheckModel> CheckDeliveryItemSampleStatus();

    }
}