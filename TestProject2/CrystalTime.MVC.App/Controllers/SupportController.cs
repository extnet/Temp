using System;
using System.Globalization;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Profile;
using System.Collections.Generic;
using System.Collections;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

using Ext.Net;
using Ext.Net.MVC;

namespace Crystal.Controllers
{
    [HandleError]
    public class SupportController : Controller
    {
        public ActionResult StaticTest3(string id2)
        {

            return this.View();
        }
        public ActionResult StaticTest4(string id, FormCollection values)
        {
            return this.View();
        }

        [ValidateInput(false)]
        public DirectResult SaveData(string id, FormCollection values, string startDate, string endDate)
        {
            DirectResult response = new DirectResult();

            Ext.Net.Store store = X.GetCmp<Ext.Net.Store>("StoreTest");
            StoreDataHandler dataHandler = new StoreDataHandler(values["data"]);
            ChangeRecords<TestData> data = dataHandler.BatchObjectData<TestData>();

            for (int i = 0; i < data.Updated.Count; i++)
            {
                TestData testData = data.Updated[i];

                //we want to remove the record since it is processed so we use the delete record option
                ModelProxy record = store.GetById(testData.id);
                store.Remove(record);
                store.CommitRemoving(testData.id);
            }

            return response;
        }

        [ValidateInput(false)]
        public ActionResult GetData()
        {
            List<TestData> l = new List<TestData>();
            for (int i = 0; i < 2000; i++)
            {
                TestData t = new TestData();
                t.id = i;
                t.startTestData = "test_" + i;
                t.endTestData = "test_" + i;
                l.Add(t);
            }


            var query = l.Select(dt => new
            {
                id = dt.id,
                startTestData = dt.startTestData,
                endTestData = dt.endTestData
                
            }).OrderBy(d => d.id);

            return this.Store(query);
        
        }

        [ValidateInput(false)]
        public ActionResult GetData2(int? start, int? limit, string sort, string dir)
        {
            List<TestData2> l = new List<TestData2>();
            for (int i = 0; i < 245; i++)
            {
                TestData2 t = new TestData2();
                t.id = i;
                t.data1 = "test_" + i;
                t.data2 = "test_" + i;
                t.data3 = "test_" + i;
                t.data4 = "test_" + i;
                t.data5 = "test_" + i;
                t.data6 = DateTime.Now.AddDays(i);
                t.data7 = "test_" + i;
                t.data8 = "test_" + i;
                t.data9 = i;
                t.data10 = i;
                t.data11 = i;
                t.data12 = i;
                t.data13 = "test_" + i;
                t.data14 = "test_" + i;
                t.data15 = "test_" + i;
                t.data16 = "test_" + i;
                l.Add(t);
            }


            var query = l.Select(dt => new
            {
                id = dt.id,
                data1 = dt.data1,
                data2 = dt.data2,
                data3 = dt.data3,
                data4 = dt.data4,
                data5 = dt.data5,
                data6 = dt.data6,
                data7 = dt.data7,
                data8 = dt.data8,
                data9 = dt.data9,
                data10 = dt.data10,
                data11 = dt.data11,
                data12 = dt.data12,
                data13 = dt.data13,
                data14 = dt.data14,
                data15 = dt.data15,
                data16 = dt.data16

            }).OrderBy(d => d.id);

            return this.Store(query.Skip(start.HasValue ? start.Value : 0).Take(limit.HasValue ? limit.Value : 500), query.Count());
        }

        [ValidateInput(false)]
        public ActionResult GetData3(int? start, int? limit, string sort, string dir)
        {
            List<TestData3> l = new List<TestData3>();
            for (int i = 0; i < 245; i++)
            {
                TestData3 t = new TestData3();
                t.id = i;
                t.description = "test_" + i;
             
                l.Add(t);
            }

            var query = l.Select(dt => new
            {
                id = dt.id,
                description = dt.description
            }).OrderBy(d => d.id);

            return this.Store(query.Skip(start.HasValue ? start.Value : 0).Take(limit.HasValue ? limit.Value : 500), query.Count());
        }
     }

    public class TestData
    {
        public long id;
        public string startTestData;
        public string endTestData;
    }

    public class TestData2
    {
        public long id;
        public string data1;
        public string data2;
        public string data3;
        public string data4;
        public string data5;
        public DateTime data6;
        public string data7;
        public string data8;
        public int data9;
        public int data10;
        public int data11;
        public int data12;
        public string data13;
        public string data14;
        public string data15;
        public string data16;
    }

    public class TestData3
    {
        public long id;
        public string description;
    }

}