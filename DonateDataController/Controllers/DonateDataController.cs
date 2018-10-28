using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;
using MySql.Data.MySqlClient;
using UtilityControllers.Models;

namespace DonateDataController.Controllers
{
    [RoutePrefix("api/DanateData")]
    public class DonateDataController : ApiController
    {
        [Route("Add")]
        [HttpPost]
        public IHttpActionResult DonateDataAdd([FromBody] DonateDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                string SQLDetailString =
                    @"INSERT INTO donatedetaildata (documentrunno, detailrunno, description, amount, remark)
                                        VALUES (@documentrunno, @detailrunno, @description, @amount, @remark)";
                MySqlCommand qDetailExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLDetailString
                };
                var detail = item.DonateDetail;

                string SQLString =
                    @"INSERT INTO donatedata (writeat, documentdate, partymemid, membername, memberid, memberbirthdate,
                                    memberhousenumber, membersoi, memberroad, membermoo, memberbuilding, membertambon, memberamphur,
                                    memberprovince, memberzipcode, membertelephone, memberposition, donatetype, donateobjective,
                                    donatorname, donatorid, donatorregisterno, donatortaxid, donatorhousenumber, donatormoo, donatorbuilding,
                                    donatorsoi, donatorroad, donatortambon, donatoramphur, donatorprovince, donatorzipcode, donatortelephone, donateamount)
                                    VALUES (@writeat, @documentdate, @partymemid, @membername, @memberid, @memberbirthdate, @memberhousenumber,
                                    @membersoi, @memberroad, @membermoo, @memberbuilding, @membertambon, @memberamphur, @memberprovince, @memberzipcode,
                                    @membertelephone, @memberposition, @donatetype, @donateobjective, @donatorname, @donatorid, @donatorregisterno,
                                    @donatortaxid, @donatorhousenumber, @donatormoo, @donatorbuilding, @donatorsoi, @donatorroad, @donatortambon,
                                    @donatoramphur, @donatorprovince, @donatorzipcode, @donatortelephone, @donateamount)";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                qExe.Parameters.AddWithValue("@writeat", item.WriteAt);
                qExe.Parameters.AddWithValue("@documentdate", item.DocumentDate);
                qExe.Parameters.AddWithValue("@partymemid", item.PartymemID);
                qExe.Parameters.AddWithValue("@membername", item.MemberName);
                qExe.Parameters.AddWithValue("@memberid", item.MemberID);
                qExe.Parameters.AddWithValue("@memberbirthdate", item.MemberBirthdate);
                qExe.Parameters.AddWithValue("@memberhousenumber", item.MemberHouseNumber);
                qExe.Parameters.AddWithValue("@membersoi", item.MemberSoi);
                qExe.Parameters.AddWithValue("@memberroad", item.MemberSoi);
                qExe.Parameters.AddWithValue("@membermoo", item.MemberMoo);
                qExe.Parameters.AddWithValue("@memberbuilding", item.MemberBuilding);
                qExe.Parameters.AddWithValue("@membertambon", item.MemberTambon);
                qExe.Parameters.AddWithValue("@memberamphur", item.MemberAmphur);
                qExe.Parameters.AddWithValue("@memberprovince", item.MemberProvince);
                qExe.Parameters.AddWithValue("@memberzipcode", item.MemberZipcode);
                qExe.Parameters.AddWithValue("@membertelephone", item.MemberTelephone);
                qExe.Parameters.AddWithValue("@memberposition", item.MemberPosition);
                qExe.Parameters.AddWithValue("@donatetype", item.DonateType);
                qExe.Parameters.AddWithValue("@donateobjective", item.DonateObjective);
                qExe.Parameters.AddWithValue("@donatorname", item.DonatorName);
                qExe.Parameters.AddWithValue("@donatorid", item.DonatorID);
                qExe.Parameters.AddWithValue("@donatorregisterno", item.DonatorRegisterNO);
                qExe.Parameters.AddWithValue("@donatortaxid", item.DonatorTaxID);
                qExe.Parameters.AddWithValue("@donatorhousenumber", item.DonatorHouseNumber);
                qExe.Parameters.AddWithValue("@donatormoo", item.DonatorMoo);
                qExe.Parameters.AddWithValue("@donatorbuilding", item.DonatorBuilding);
                qExe.Parameters.AddWithValue("@donatorsoi", item.DonatorSoi);
                qExe.Parameters.AddWithValue("@donatorroad", item.DonatorRoad);
                qExe.Parameters.AddWithValue("@donatortambon", item.DonatorTambon);
                qExe.Parameters.AddWithValue("@donatoramphur", item.DonatorAmphur);
                qExe.Parameters.AddWithValue("@donatorprovince", item.DonatorProvince);
                qExe.Parameters.AddWithValue("@donatorzipcode", item.DonatorZipcode);
                qExe.Parameters.AddWithValue("@donatortelephone", item.DonatorTelephone);
                qExe.Parameters.AddWithValue("@donateamount", item.DonateAmount);
                qExe.ExecuteNonQuery();
                long returnid = qExe.LastInsertedId;

                for (int i = 0; i <= detail.Count - 1; i++)
                {
                    qDetailExe.Parameters.Clear();
                    qDetailExe.Parameters.AddWithValue("@documentrunno", returnid);
                    qDetailExe.Parameters.AddWithValue("@detailrunno", i + 1);
                    qDetailExe.Parameters.AddWithValue("@description", detail[i].description);
                    qDetailExe.Parameters.AddWithValue("@amount", detail[i].Amount);
                    qDetailExe.Parameters.AddWithValue("@remark", detail[i].Remark);
                    qDetailExe.ExecuteNonQuery();
                }

                conn.CloseConnection();
                return Ok(returnid.ToString());
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("Edit")]
        [HttpPost]
        public IHttpActionResult DonateDataEdit([FromBody] DonateDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                string SQLDetailDeleteString = "DELETE FROM donatedetaildata WHERE documentrunno = @documentrunno";
                string SQLDetailString = @"INSERT INTO donatedetaildata (documentrunno, detailrunno, description, amount, remark)
                                        VALUES (@documentrunno, @detailrunno, @description, @amount, @remark)";
                MySqlCommand qDetailExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLDetailDeleteString
                };
                qDetailExe.Parameters.AddWithValue("@documentrunno", item.DocumentRunno);
                qDetailExe.ExecuteNonQuery();
                qDetailExe.Parameters.Clear();
                qDetailExe.CommandText = SQLDetailString;
                var detail = item.DonateDetail;

                string SQLString = @"UPDATE donatedata SET documentrunno = @documentrunno, writeat = @writeat,
                                     documentdate = @documentdate, partymemid = @partymemid, membername = @membername,
                                     memberid = @memberid, memberbirthdate = @memberbirthdate, memberhousenumber = @memberhousenumber,
                                     membersoi = @membersoi, memberroad = @memberroad, membermoo = @membermoo, memberbuilding = @memberbuilding,
                                     membertambon = @membertambon, memberamphur = @memberamphur, memberprovince = @memberprovince,
                                     memberzipcode = @memberzipcode, membertelephone = @membertelephone, memberposition = @memberposition,
                                     donatetype = @donatetype, donateobjective = @donateobjective, donatorname = @donatorname,
                                     donatorid = @donatorid, donatorregisterno = @donatorregisterno, donatortaxid = @donatortaxid,
                                     donatorhousenumber = @donatorhousenumber, donatormoo = @donatormoo, donatorbuilding = @donatorbuilding,
                                     donatorsoi = @donatorsoi, donatorroad = @donatorroad, donatortambon = @donatortambon,
                                     donatoramphur = @donatoramphur, donatorprovince = @donatorprovince, donatorzipcode = @donatorzipcode,
                                     donatortelephone = @donatortelephone, donateamount = @donateamount WHERE documentrunno = @documentrunno ";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                qExe.Parameters.AddWithValue("@documentrunno", item.DocumentRunno);
                qExe.Parameters.AddWithValue("@writeat", item.WriteAt);
                qExe.Parameters.AddWithValue("@documentdate", item.DocumentDate);
                qExe.Parameters.AddWithValue("@partymemid", item.PartymemID);
                qExe.Parameters.AddWithValue("@membername", item.MemberName);
                qExe.Parameters.AddWithValue("@memberid", item.MemberID);
                qExe.Parameters.AddWithValue("@memberbirthdate", item.MemberBirthdate);
                qExe.Parameters.AddWithValue("@memberhousenumber", item.MemberHouseNumber);
                qExe.Parameters.AddWithValue("@membersoi", item.MemberSoi);
                qExe.Parameters.AddWithValue("@memberroad", item.MemberSoi);
                qExe.Parameters.AddWithValue("@membermoo", item.MemberMoo);
                qExe.Parameters.AddWithValue("@memberbuilding", item.MemberBuilding);
                qExe.Parameters.AddWithValue("@membertambon", item.MemberTambon);
                qExe.Parameters.AddWithValue("@memberamphur", item.MemberAmphur);
                qExe.Parameters.AddWithValue("@memberprovince", item.MemberProvince);
                qExe.Parameters.AddWithValue("@memberzipcode", item.MemberZipcode);
                qExe.Parameters.AddWithValue("@membertelephone", item.MemberTelephone);
                qExe.Parameters.AddWithValue("@memberposition", item.MemberPosition);
                qExe.Parameters.AddWithValue("@donatetype", item.DonateType);
                qExe.Parameters.AddWithValue("@donateobjective", item.DonateObjective);
                qExe.Parameters.AddWithValue("@donatorname", item.DonatorName);
                qExe.Parameters.AddWithValue("@donatorid", item.DonatorID);
                qExe.Parameters.AddWithValue("@donatorregisterno", item.DonatorRegisterNO);
                qExe.Parameters.AddWithValue("@donatortaxid", item.DonatorTaxID);
                qExe.Parameters.AddWithValue("@donatorhousenumber", item.DonatorHouseNumber);
                qExe.Parameters.AddWithValue("@donatormoo", item.DonatorMoo);
                qExe.Parameters.AddWithValue("@donatorbuilding", item.DonatorBuilding);
                qExe.Parameters.AddWithValue("@donatorsoi", item.DonatorSoi);
                qExe.Parameters.AddWithValue("@donatorroad", item.DonatorRoad);
                qExe.Parameters.AddWithValue("@donatortambon", item.DonatorTambon);
                qExe.Parameters.AddWithValue("@donatoramphur", item.DonatorAmphur);
                qExe.Parameters.AddWithValue("@donatorprovince", item.DonatorProvince);
                qExe.Parameters.AddWithValue("@donatorzipcode", item.DonatorZipcode);
                qExe.Parameters.AddWithValue("@donatortelephone", item.DonatorTelephone);
                qExe.Parameters.AddWithValue("@donateamount", item.DonateAmount);
                qExe.ExecuteNonQuery();

                for (int i = 0; i <= detail.Count - 1; i++)
                {
                    qDetailExe.Parameters.Clear();
                    qDetailExe.Parameters.AddWithValue("@documentrunno", item.DocumentRunno);
                    qDetailExe.Parameters.AddWithValue("@detailrunno", i + 1);
                    qDetailExe.Parameters.AddWithValue("@description", detail[i].description);
                    qDetailExe.Parameters.AddWithValue("@amount", detail[i].Amount);
                    qDetailExe.Parameters.AddWithValue("@remark", detail[i].Remark);
                    qDetailExe.ExecuteNonQuery();
                }

                conn.CloseConnection();
                return Ok();
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult DonateDataDelete([FromBody] DonateDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                string sqlString = @"delete from donatedata where documentrunno = @documentrunno";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = sqlString
                };
                if (string.IsNullOrEmpty(item.DocumentRunno))
                {
                    return BadRequest("Document Key is null!");
                }
                qExe.Parameters.AddWithValue("@documentrunno", item.DocumentRunno);
                qExe.ExecuteNonQuery();
                sqlString = "delete from donatedetaildata where documentrunno = @documentrunno";
                qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = sqlString
                };
                qExe.Parameters.AddWithValue("@documentrunno", item.DocumentRunno);
                qExe.ExecuteNonQuery();
                conn.CloseConnection();
                return Ok();
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("ListbyRunno/{runno}")]
        [HttpGet]
        public IHttpActionResult DonateDataListbyRunno(string runno)
        {
            DonateDataModel result = new DonateDataModel();
            result.DonateDetail = new List<DonateDetailDataModel>();
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                string sqlString;
                if (!string.IsNullOrEmpty(runno))
                    sqlString = @"select * from donatedata where documentrunno = @documentrunno";
                else
                    return Json("Document Number is blank!");
                string sqlDetail = @"select * from donatedetaildata where documentrunno = @documentrunno order by detailrunno";
                MySqlCommand qDetail = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = sqlDetail
                };
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = sqlString
                };
                qExe.Parameters.AddWithValue("@documentrunno", runno);
                qDetail.Parameters.AddWithValue("@documentrunno", runno);
                MySqlDataReader detailReader = qDetail.ExecuteReader();
                while (detailReader.Read())
                {
                    DonateDetailDataModel detailRow = new DonateDetailDataModel();
                    detailRow.DocumentRunno = detailReader["documentrunno"].ToString();
                    detailRow.DetailRunno = int.Parse(detailReader["detailrunno"].ToString());
                    detailRow.description = detailReader["description"].ToString();
                    detailRow.Amount = double.Parse(detailReader["amount"].ToString());
                    detailRow.Remark = detailReader["remark"].ToString();
                    result.DonateDetail.Add(detailRow);
                }
                detailReader.Close();
                MySqlDataReader dataReader = qExe.ExecuteReader();
                while (dataReader.Read())
                {
                    result.DocumentRunno = dataReader["documentrunno"].ToString();
                    result.WriteAt = dataReader["writeat"].ToString();
                    result.DocumentDate = Convert.ToDateTime(dataReader["documentdate"].ToString(), new CultureInfo("en-US"));
                    result.PartymemID = dataReader["partymemid"].ToString();
                    result.MemberName = dataReader["membername"].ToString();
                    result.MemberID = dataReader["memberid"].ToString();
                    if (!string.IsNullOrEmpty(dataReader["memberbirthdate"].ToString()))
                        result.MemberBirthdate = Convert.ToDateTime(dataReader["memberbirthdate"].ToString(), new CultureInfo("en-US"));
                    else
                        result.MemberBirthdate = null;
                    result.MemberHouseNumber = dataReader["memberhousenumber"].ToString();
                    result.MemberSoi = dataReader["membersoi"].ToString();
                    result.MemberRoad = dataReader["memberroad"].ToString();
                    result.MemberMoo = dataReader["membermoo"].ToString();
                    result.MemberBuilding = dataReader["memberbuilding"].ToString();
                    result.MemberTambon = dataReader["membertambon"].ToString();
                    result.MemberAmphur = dataReader["memberamphur"].ToString();
                    result.MemberProvince = dataReader["memberprovince"].ToString();
                    result.MemberZipcode = dataReader["memberzipcode"].ToString();
                    result.MemberTelephone = dataReader["membertelephone"].ToString();
                    result.MemberPosition = dataReader["memberposition"].ToString();
                    result.DonateType = dataReader["donatetype"].ToString();
                    result.DonateObjective = dataReader["donateobjective"].ToString();
                    result.DonatorName = dataReader["donatorname"].ToString();
                    result.DonatorID = dataReader["donatorid"].ToString();
                    result.DonatorRegisterNO = dataReader["donatorregisterno"].ToString();
                    result.DonatorTaxID = dataReader["donatortaxid"].ToString();
                    result.DonatorHouseNumber = dataReader["donatorhousenumber"].ToString();
                    result.DonatorMoo = dataReader["donatormoo"].ToString();
                    result.DonatorBuilding = dataReader["donatorbuilding"].ToString();
                    result.DonatorSoi = dataReader["donatorsoi"].ToString();
                    result.DonatorRoad = dataReader["donatorroad"].ToString();
                    result.DonatorTambon = dataReader["donatortambon"].ToString();
                    result.DonatorAmphur = dataReader["donatoramphur"].ToString();
                    result.DonatorProvince = dataReader["donatorprovince"].ToString();
                    result.DonatorZipcode = dataReader["donatorzipcode"].ToString();
                    result.DonatorTelephone = dataReader["donatortelephone"].ToString();
                    if (!string.IsNullOrEmpty(dataReader["donateamount"].ToString()))
                        result.DonateAmount = double.Parse(dataReader["donateamount"].ToString());
                    else
                        result.DonateAmount = 0;
                }
                dataReader.Close();
                conn.CloseConnection();
                return Json(result);
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("ListAllReceipt")]
        [HttpGet]
        public IHttpActionResult ReceipDataList()
        {
            List<DonateDataModel> result = new List<DonateDataModel>();
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                string SQLString = @"select * from donatedata where documentrunno = @documentrunno";
                string sqlDetail = @"select * from donatedetaildata where documentrunno = @documentrunno order by detailrunno";
                MySqlCommand qDetail = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = sqlDetail
                };
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };

                MySqlDataReader dataReader = qExe.ExecuteReader();
                while (dataReader.Read())
                {
                    List<DonateDetailDataModel> detailList = new List<DonateDetailDataModel>();
                    qDetail.Parameters.AddWithValue("@documentrunno", dataReader["documentrunno"].ToString());
                    MySqlDataReader detailReader = qDetail.ExecuteReader();
                    while (detailReader.Read())
                    {
                        DonateDetailDataModel detailRow = new DonateDetailDataModel();
                        detailRow.DocumentRunno = detailReader["documentrunno"].ToString();
                        detailRow.DetailRunno = int.Parse(detailReader["detailrunno"].ToString());
                        detailRow.description = detailReader["description"].ToString();
                        detailRow.Amount = double.Parse(detailReader["amount"].ToString());
                        detailRow.Remark = detailReader["remark"].ToString();
                        detailList.Add(detailRow);
                    }

                    DonateDataModel detail = new DonateDataModel
                    {
                        DocumentRunno = dataReader["documentrunno"].ToString(),
                        WriteAt = dataReader["writeat"].ToString(),
                        DocumentDate = Convert.ToDateTime(dataReader["documentdate"].ToString(), new CultureInfo("en-US")),
                        PartymemID = dataReader["partymemid"].ToString(),
                        MemberName = dataReader["membername"].ToString(),
                        MemberID = dataReader["memberid"].ToString(),
                        MemberBirthdate = Convert.ToDateTime(dataReader["memberbirthdate"].ToString(), new CultureInfo("en-US")),
                        MemberHouseNumber = dataReader["memberhousenumber"].ToString(),
                        MemberSoi = dataReader["membersoi"].ToString(),
                        MemberRoad = dataReader["memberroad"].ToString(),
                        MemberMoo = dataReader["membermoo"].ToString(),
                        MemberBuilding = dataReader["memberbuilding"].ToString(),
                        MemberTambon = dataReader["membertambon"].ToString(),
                        MemberAmphur = dataReader["memberamphur"].ToString(),
                        MemberProvince = dataReader["memberprovince"].ToString(),
                        MemberZipcode = dataReader["memberzipcode"].ToString(),
                        MemberTelephone = dataReader["membertelephone"].ToString(),
                        MemberPosition = dataReader["memberposition"].ToString(),
                        DonateType = dataReader["donatetype"].ToString(),
                        DonateObjective = dataReader["donateobjective"].ToString(),
                        DonatorName = dataReader["donatorname"].ToString(),
                        DonatorID = dataReader["donatorid"].ToString(),
                        DonatorRegisterNO = dataReader["donatorregisterno"].ToString(),
                        DonatorTaxID = dataReader["donatortaxid"].ToString(),
                        DonatorHouseNumber = dataReader["donatorhousenumber"].ToString(),
                        DonatorMoo = dataReader["donatormoo"].ToString(),
                        DonatorBuilding = dataReader["donatorbuilding"].ToString(),
                        DonatorSoi = dataReader["donatorsoi"].ToString(),
                        DonatorRoad = dataReader["donatorroad"].ToString(),
                        DonatorTambon = dataReader["donatortambon"].ToString(),
                        DonatorAmphur = dataReader["donatoramphur"].ToString(),
                        DonatorProvince = dataReader["donatorprovince"].ToString(),
                        DonatorZipcode = dataReader["donatorzipcode"].ToString(),
                        DonatorTelephone = dataReader["donatortelephone"].ToString(),
                        DonateAmount = double.Parse(dataReader["donateamount"].ToString()),
                        DonateDetail = detailList
                    };

                }
                dataReader.Close();
                conn.CloseConnection();
                return Json(result);
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
    }
}
