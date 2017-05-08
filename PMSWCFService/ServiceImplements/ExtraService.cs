using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public class ExtraService : ICheckListService, IItemDebitService
    {
        public int AddCheckList(DcCheckList model, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddItemDebit(DcItemDebit model, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteCheckList(Guid id, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteItemDebit(Guid id, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcCheckList> GetCheckList(int s, int t, string title)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetCheckListCount(string title)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName, string creaditor)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetItemDebitCount(string itemType, string itemName, string creaditor)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateCheckList(DcCheckList model, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateItemDebit(DcItemDebit model, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}