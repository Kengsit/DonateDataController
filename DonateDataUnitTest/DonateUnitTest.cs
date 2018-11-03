using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityControllers.Models;

namespace DonateDataUnitTest
{
    [TestClass]
    public class DonateUnitTest
    {
        [TestMethod]
        public void ทดสอบบันทึกข้อมูลใหม่()
        {
            List<DonateDetailDataModel> detailList = new List<DonateDetailDataModel>
            {
                new DonateDetailDataModel
                {
                    Description = "บริจาคด้วยเงินสด",
                    Amount = 5000,
                    Remark = "หมายเหตุไม่มี"
                }
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel itemData = new DonateDataModel
            {
                WriteAt = "บ้าน",
                DocumentDate = DateTime.Now.Date,
                DonateType = "เงินสด",
                DonateObjective = "เพื่อบำรุงพรรค",
                MemberRunno = 1,
                MemberId = "",
                DonatorRunno = 1,
                DonatorId = "",
                DonateAmount = 5000,
                DonateDetail = detailList
            };
            var result = service.DonateDataAdd(itemData);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void ทดสอบแก้ไขเอกสาร()
        {
            List<DonateDetailDataModel> detailList = new List<DonateDetailDataModel>
            {
                new DonateDetailDataModel
                {
                    Description = "บริจาคด้วยเงินสด",
                    Amount = 500,
                    Remark = "โอนเงินเข้าธนาคาร"
                },
                new DonateDetailDataModel
                {
                    Description = "เครื่องทำกาแฟ",
                    Amount = 1500,
                    Remark = ""
                }
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel itemData = new DonateDataModel
            {
                DocumentRunno = 5,
                WriteAt = "บ้าน",
                DocumentDate = DateTime.Now.Date,
                DonateType = "เงินสด",
                DonateObjective = "เพื่อบำรุงพรรค",
                MemberRunno = 1,
                MemberId = "",
                DonatorRunno = 1,
                DonatorId = "",
                DonateAmount = 5000,
                DonateDetail = detailList
            };
            var result = service.DonateDataEdit(itemData);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ทดสอบลบเอกสาร()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            // DonateDataModel item = new DonateDataModel();
            // item.DocumentRunno = "2";
            var result = service.DonateDataDelete("6");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ทดสอบการดึงข้อมูลตามrunno()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel item = new DonateDataModel();
            string DocumentRunno = "5";
            var result = service.DonateDataListbyRunno(DocumentRunno);
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void ทดสอบดึงเอกสารทั้งหมด()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            var result = service.DonateDataList();
            Assert.IsNotNull(result);
        }
        
    }
}
