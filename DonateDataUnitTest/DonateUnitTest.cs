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
                    description = "บริจาคด้วยเงินสด",
                    Amount = 5000,
                    Remark = "หมายเหตุไม่มี"
                }
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel itemData = new DonateDataModel
            {
                WriteAt = "บ้าน",
                DocumentDate = DateTime.Now.Date,
                PartymemID = "0002",
                MemberName = "ชื่อสมาชิกพรรค",
                MemberID = "123456789213",
                MemberBirthdate = null,
                MemberHouseNumber = "123",
                MemberMoo = "1",
                MemberBuilding = "",
                MemberSoi = "",
                MemberRoad = "",
                MemberTambon = "",
                MemberAmphur = "",
                MemberProvince = "",
                MemberZipcode = "",
                MemberTelephone = "",
                MemberPosition = "123456",
                DonateType = "เงินสด",
                DonateObjective = "เพื่อบำรุงพรรค",
                DonatorName = "",
                DonatorID = "",
                DonatorRegisterNO = "",
                DonatorTaxID = "",
                DonatorHouseNumber = "",
                DonatorMoo = "",
                DonatorBuilding = "",
                DonatorSoi = "",
                DonatorRoad = "",
                DonatorTambon = "",
                DonatorAmphur = "",
                DonatorProvince = "",
                DonatorZipcode = "",
                DonatorTelephone = "",
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
                    description = "บริจาคด้วยเงินสด",
                    Amount = 500,
                    Remark = "โอนเงินเข้าธนาคาร"
                }, 
                new DonateDetailDataModel
                {
                    description = "เครื่องทำกาแฟ",
                    Amount = 1500,
                    Remark = ""
                }
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel itemData = new DonateDataModel
            {
                DocumentRunno = "1",
                WriteAt = "เขียนที่",
                DocumentDate = DateTime.Now.Date,
                PartymemID = "0001",
                MemberName = "ชื่อสมาชิกพรรค",
                MemberID = "123456789213",
                MemberBirthdate = null,
                MemberHouseNumber = "123",
                MemberMoo = "1",
                MemberBuilding = "อาคาร",
                MemberSoi = "ซอย",
                MemberRoad = "",
                MemberTambon = "",
                MemberAmphur = "",
                MemberProvince = "กรุงเทพมหานคร",
                MemberZipcode = "",
                MemberTelephone = "",
                MemberPosition = "",
                DonateType = "",
                DonateObjective = "",
                DonatorName = "",
                DonatorID = "",
                DonatorRegisterNO = "",
                DonatorTaxID = "",
                DonatorHouseNumber = "",
                DonatorMoo = "",
                DonatorBuilding = "",
                DonatorSoi = "เพิ่มซอยจากการแก้ไข",
                DonatorRoad = "",
                DonatorTambon = "",
                DonatorAmphur = "",
                DonatorProvince = "",
                DonatorZipcode = "",
                DonatorTelephone = "",
                DonateAmount = 2000,
                DonateDetail = detailList
            };
            var result = service.DonateDataEdit(itemData);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ทดสอบลบเอกสาร()
        {
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel item = new DonateDataModel();
            item.DocumentRunno = "2";
            var result = service.DonateDataDelete(item);
            Assert.IsNotNull(result);
        }
    }
    }
