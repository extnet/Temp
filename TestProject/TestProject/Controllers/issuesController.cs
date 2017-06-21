using Ext.Net;
using Ext.Net.MVC;
using TestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class issuesController : Controller
    {
        // GET: issues
        public ActionResult Index()
        {
            return View();
        }

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
    }
}