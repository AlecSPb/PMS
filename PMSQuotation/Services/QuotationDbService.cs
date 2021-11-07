using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSQuotation.Models;
using Dapper;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace PMSQuotation.Services
{
    /// <summary>
    /// 报价服务类
    /// </summary>
    public class QuotationDbService
    {
        private string conn_str;
        public QuotationDbService()
        {
            //string dbPath = Path.Combine(XSHelper.XS.File.GetCurrentFolderPath("DB"), "pmsquotation.db");
            //string dbPath = @"D:\Fine\source\repos\PMS\PMSQuotation\DB\pmsquotation.db";
            string dbPath = @"D:\source\repos\PMS\PMSQuotation\DB\pmsquotation.db";
            //发布后修改
            conn_str = $"Data Source={dbPath};Version=3";
        }

        /// <summary>
        /// 获取报价单项目
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="keyword"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<Quotation> GetQuotations(string customer, string keyword, bool showDeleted)
        {
            List<Quotation> models = new List<Quotation>();
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "";
                if (showDeleted)
                {
                    sql = "select * from quotations where contactInfo_customer like @cc and keyword like @kw order by createtime desc";
                }
                else
                {
                    sql = "select * from quotations where contactInfo_customer like @cc and keyword like @kw and" +
                        " state !='Deleted' order by createtime desc";
                }

                var parameters = new
                {
                    cc = $"%{customer}%",
                    kw = $"%{keyword}%"
                };
                var result = conn.Query<Quotation>(sql, parameters);
                models.Clear();
                models.AddRange(result);
            }


            return models;
        }

        public Quotation GetQuotationByID(int id)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "select * from quotations where id=@id order by createtime desc";


                var parameters = new
                {
                    id = id
                };
                var result = conn.Query<Quotation>(sql, parameters).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// 返回时间最近的Quotation
        /// </summary>
        /// <returns></returns>
        public Quotation GetQuotationLastestCreateTime()
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "select * from quotations order by createtime desc";

                var result = conn.Query<Quotation>(sql).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// 添加报价单项目
        /// </summary>
        /// <param name="model"></param>
        public void Add(Quotation model)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "insert into quotations(CurrencyType,TotalCost,CreateTime,LastUpdateTime,ExpirationTime,Creator,Lot," +
                    "Remark,KeyWord,ContactInfo_Customer,ContactInfo_Self,PackageFee,PackageRemark,ShippingFee,ShippingRemark," +
                    "CustomFee,CustomRemark,IsAutoTax,TaxFee,TaxRemark,State) values (@CurrencyType,@TotalCost,@CreateTime,@LastUpdateTime,@ExpirationTime,@Creator,@Lot," +
                    "@Remark,@KeyWord,@ContactInfo_Customer,@ContactInfo_Self,@PackageFee,@PackageRemark,@ShippingFee,@ShippingRemark," +
                    "@CustomFee,@CustomRemark,@IsAutoTax,@TaxFee,@TaxRemark,@State)";
                var parameters = new Quotation
                {
                    CurrencyType = model.CurrencyType,
                    TotalCost = model.TotalCost,
                    CreateTime = model.CreateTime,
                    LastUpdateTime = model.LastUpdateTime,
                    ExpirationTime = model.ExpirationTime,
                    Creator = model.Creator,
                    Lot = model.Lot,
                    Remark = model.Remark,
                    KeyWord = model.KeyWord,
                    ContactInfo_Customer = model.ContactInfo_Customer,
                    ContactInfo_Self = model.ContactInfo_Self,
                    PackageFee = model.PackageFee,
                    PackageRemark = model.PackageRemark,
                    ShippingFee = model.ShippingFee,
                    ShippingRemark = model.ShippingRemark,
                    CustomFee = model.CustomFee,
                    CustomRemark = model.CustomRemark,
                    IsAutoTax = model.IsAutoTax,
                    TaxFee = model.TaxFee,
                    TaxRemark = model.TaxRemark,
                    State = model.State
                };

                conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// 更新Quotation
        /// </summary>
        /// <param name="model"></param>
        public void Update(Quotation model)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "update quotations set CurrencyType=@CurrencyType,TotalCost=@TotalCost,CreateTime=@CreateTime," +
                    "LastUpdateTime=@LastUpdateTime,ExpirationTime=@ExpirationTime,Creator=@Creator,Lot=@Lot," +
                    "Remark=@Remark,KeyWord=@KeyWord,ContactInfo_Customer=@ContactInfo_Customer,ContactInfo_Self=@ContactInfo_Self," +
                    "PackageFee=@PackageFee,PackageRemark=@PackageRemark,ShippingFee=@ShippingFee,ShippingRemark=@ShippingRemark," +
                    "CustomFee=@CustomFee,CustomRemark=@CustomRemark,IsAutoTax=@IsAutoTax,TaxFee=@TaxFee,TaxRemark=@TaxRemark,State=@State" +
                    " where id=@id";
                var parameters = new Quotation
                {
                    CurrencyType = model.CurrencyType,
                    TotalCost = model.TotalCost,
                    CreateTime = model.CreateTime,
                    LastUpdateTime = model.LastUpdateTime,
                    ExpirationTime = model.ExpirationTime,
                    Creator = model.Creator,
                    Lot = model.Lot,
                    Remark = model.Remark,
                    KeyWord = model.KeyWord,
                    ContactInfo_Customer = model.ContactInfo_Customer,
                    ContactInfo_Self = model.ContactInfo_Self,
                    PackageFee = model.PackageFee,
                    PackageRemark = model.PackageRemark,
                    ShippingFee = model.ShippingFee,
                    ShippingRemark = model.ShippingRemark,
                    CustomFee = model.CustomFee,
                    CustomRemark = model.CustomRemark,
                    IsAutoTax = model.IsAutoTax,
                    TaxFee = model.TaxFee,
                    TaxRemark = model.TaxRemark,
                    State = model.State,
                    ID = model.ID
                };

                conn.Execute(sql, parameters);
            }
        }





        public List<QuotationItem> GetQuotationItems(int quotationId, bool showDeleted)
        {

            List<QuotationItem> models = new List<QuotationItem>();
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "";
                if (showDeleted)
                {
                    sql = "select * from quotationitems where quotationid=@quotationid order by createtime asc";
                }
                else
                {
                    sql = "select * from quotationitems where quotationid=@quotationid and state!='Deleted' order by createtime asc";
                }

                var parameters = new
                {
                    quotationid = quotationId
                };
                var result = conn.Query<QuotationItem>(sql, parameters);
                models.Clear();
                models.AddRange(result);
            }


            return models;

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void AddItem(QuotationItem model)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "insert into quotationitems (QuotationID,Composition,Specification,UnitPrice,Quantity,TotalPrice,DeliveryTime,Note," +
                    "UnitPriceDetail,CreateTime,Creator,State) " +
                    "values (@QuotationID,@Composition,@Specification,@UnitPrice,@Quantity,@TotalPrice,@DeliveryTime,@Note," +
                    "@UnitPriceDetail,@CreateTime,@Creator,@State)";
                var parameters = new QuotationItem
                {
                    QuotationID = model.QuotationID,
                    Composition = model.Composition,
                    Specification = model.Specification,
                    UnitPrice = model.UnitPrice,
                    Quantity = model.Quantity,
                    TotalPrice = model.TotalPrice,
                    DeliveryTime = model.DeliveryTime,
                    Note = model.Note,
                    UnitPriceDetail = model.UnitPriceDetail,
                    CreateTime = model.CreateTime,
                    Creator = model.Creator,
                    State = model.State
                };
                conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void UpdateItem(QuotationItem model)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "update quotationitems set QuotationID=@QuotationID,Composition=@Composition,Specification=@Specification," +
                    "UnitPrice=@UnitPrice,Quantity=@Quantity,TotalPrice=@TotalPrice,DeliveryTime=@DeliveryTime," +
                    "Note=@Note,UnitPriceDetail=@UnitPriceDetail,CreateTime=@CreateTime,Creator=@Creator,State=@State " +
                    " where id=@id";
                var parameters = new QuotationItem
                {
                    QuotationID = model.QuotationID,
                    Composition = model.Composition,
                    Specification = model.Specification,
                    UnitPrice = model.UnitPrice,
                    Quantity = model.Quantity,
                    TotalPrice = model.TotalPrice,
                    DeliveryTime = model.DeliveryTime,
                    Note = model.Note,
                    UnitPriceDetail = model.UnitPriceDetail,
                    CreateTime = model.CreateTime,
                    Creator = model.Creator,
                    State = model.State,
                    ID = model.ID
                };
                conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// 获取数据字典项目
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataDict GetDataDictByKey(string key)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "select * from datadicts where datakey=@datakey";
                var parameters = new
                {
                    datakey = key
                };
                var result = conn.Query<DataDict>(sql, parameters);
                return result.FirstOrDefault();
            }
        }


        public List<DataDict> GetDataDicts()
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "select * from datadicts order by numberorder asc";
                var result = conn.Query<DataDict>(sql);
                return result.ToList();
            }
        }


        public void AddDataDict(DataDict model)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "insert into datadicts (DataKey,DataValue,Description,LastUpdateTime,State) values " +
                    "(@DataKey,@DataValue,@Description,@LastUpdateTime,@State)";
                var parameters = new DataDict()
                {
                    DataKey = model.DataKey,
                    DataValue = model.DataValue,
                    Description = model.Description,
                    LastUpdateTime = model.LastUpdateTime,
                    State = model.State
                };
                conn.Execute(sql, parameters);
            }
        }

        public void UpdateDataDict(DataDict model)
        {
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "update datadicts set DataKey=@DataKey,DataValue=@DataValue,Description=@Description," +
                    "LastUpdateTime=@LastUpdateTime,State=@State where id=@id";
                var parameters = new DataDict()
                {
                    DataKey = model.DataKey,
                    DataValue = model.DataValue,
                    Description = model.Description,
                    LastUpdateTime = model.LastUpdateTime,
                    State = model.State,
                    ID = model.ID
                };
                conn.Execute(sql, parameters);
            }
        }


    }
}
