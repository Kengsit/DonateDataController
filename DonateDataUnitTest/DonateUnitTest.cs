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
                    Amount = 500,
                    Remark = "โอนเงินเข้าธนาคาร"
                }
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel itemData = new DonateDataModel
            {
                WriteAt = "เขียนที่",
                DocumentDate = DateTime.Now.Date,
                PartymemID = "0001",
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
                DonatorSoi = "",
                DonatorRoad = "",
                DonatorTambon = "",
                DonatorAmphur = "",
                DonatorProvince = "",
                DonatorZipcode = "",
                DonatorTelephone = "",
                DonateAmount = 500,
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
                }
            };
            DonateDataController.Controllers.DonateDataController service = new DonateDataController.Controllers.DonateDataController();
            DonateDataModel itemData = new DonateDataModel
            {
                WriteAt = "เขียนที่",
                DocumentDate = DateTime.Now.Date,
                PartymemID = "0001",
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
                DonatorSoi = "",
                DonatorRoad = "",
                DonatorTambon = "",
                DonatorAmphur = "",
                DonatorProvince = "",
                DonatorZipcode = "",
                DonatorTelephone = "",
                DonateAmount = 500,
                DonateDetail = detailList
            };
            var result = service.DonateDataAdd(itemData);
            Assert.IsNotNull(result);

        }
    }
}
