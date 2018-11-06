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

        [TestMethod]
        public void ทดสอบบันทึกเฉพาะDetailของเอกสาร()
        {
            DonateDetailDataModel detailitem = new DonateDetailDataModel
            {
                DocumentRunno = 5,
                Description = "เพิ่มรายการโดยที่",
                Amount = 5000,
                Remark = "เลขระหว่างกลางหาย"
            };              
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            var result = service.AddDonateDetailData(detailitem);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ทดสอบแก้ไขDetailเอกสาร()
        {
            DonateDetailDataModel detailitem = new DonateDetailDataModel
            {
                DocumentRunno = 5,
                DetailRunno = 2,
                Description = "เครื่องชงกาแฟสำเร็จ",
                Amount = 20000,
                Remark = "สำหรับใช้งานใน Office"
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            var result = service.EditDonateDetailData(detailitem);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ทดสอบลบข้อมูลDetail()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            var result = service.DeleteDonateDetailData("5", "3");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ดึงข้อมูลDetail()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            var result = service.GetDetailData("5");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ทดสอบเพิ่มข้อมูลผู้บริจาค()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonatorData item = new DonatorData();
            item.DonatorId = "0003";
            item.DonatorPreName = "นาย";
            item.DonatorName = "ทดสอบลบ";
            item.DonatorSurName = "ลบได้เลย";
            item.DonatorCitizenId = "4564567890123";
            item.DonatorRegisterNo = "";
            item.DonatorTaxId = "";
            item.HouseNumber = "98";
            item.Soi = "ซอย";
            item.Road = "";
            item.Moo = "5";
            item.Building = "";
            item.Tambon = "";
            item.Amphur = "";
            item.Province = "";
            item.ZipCode = "10400";
            item.Telephone = "";
            var result = service.AddDonatorData(item);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ทดสอบแก้ไขข้อมูล()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonatorData item = new DonatorData();
            item.DonatorRunno = 2;
            item.DonatorId = "0002";
            item.DonatorPreName = "นาย";
            item.DonatorName = "แก้ไขข้อมูล";
            item.DonatorSurName = "ทดสอบมา";
            item.DonatorCitizenId = "4";
            item.DonatorRegisterNo = "";
            item.DonatorTaxId = "";
            item.HouseNumber = "8";
            item.Soi = "3321";
            item.Road = "";
            item.Moo = "9";
            item.Building = "";
            item.Tambon = "";
            item.Amphur = "";
            item.Province = "";
            item.ZipCode = "99999";
            item.Telephone = "";
            var result = service.EditDonatorData(item);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ทดสอบลบข้อมูลผู้บริจาค()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonatorData item = new DonatorData();
            item.DonatorRunno = 2;
            var result = service.DeleteDonatorData(item.DonatorRunno.ToString());
            Assert.IsNotNull(result);
        }
    }
}
