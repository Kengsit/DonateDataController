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
        [Route("Delete/{id}")]
        [HttpPost]
        public IHttpActionResult DonateDataDelete(string id)
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
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Document Key is null!");
                }
                qExe.Parameters.AddWithValue("@documentrunno", id);
                qExe.ExecuteNonQuery();
                sqlString = "delete from donatedetaildata where documentrunno = @documentrunno";
                qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = sqlString
                };
                qExe.Parameters.AddWithValue("@documentrunno", id);
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
        public IHttpActionResult DonateDataList()
        {
            List<DonateDataModel> result = new List<DonateDataModel>();
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                string SQLString = @"select * from donatedata order by documentrunno";
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
                    DonateDataModel detail = new DonateDataModel();
                    detail.DocumentRunno = dataReader["documentrunno"].ToString();
                    detail.WriteAt = dataReader["writeat"].ToString();
                    detail.DocumentDate = Convert.ToDateTime(dataReader["documentdate"].ToString(), new CultureInfo("en-US"));
                    detail.PartymemID = dataReader["partymemid"].ToString();
                    detail.MemberName = dataReader["membername"].ToString();
                    detail.MemberID = dataReader["memberid"].ToString();
                    if (!string.IsNullOrEmpty(dataReader["memberbirthdate"].ToString()))
                        detail.MemberBirthdate = Convert.ToDateTime(dataReader["memberbirthdate"].ToString(), new CultureInfo("en-US"));
                    else
                        detail.MemberBirthdate = null;
                    detail.MemberHouseNumber = dataReader["memberhousenumber"].ToString();
                    detail.MemberSoi = dataReader["membersoi"].ToString();
                    detail.MemberRoad = dataReader["memberroad"].ToString();
                    detail.MemberMoo = dataReader["membermoo"].ToString();
                    detail.MemberBuilding = dataReader["memberbuilding"].ToString();
                    detail.MemberTambon = dataReader["membertambon"].ToString();
                    detail.MemberAmphur = dataReader["memberamphur"].ToString();
                    detail.MemberProvince = dataReader["memberprovince"].ToString();
                    detail.MemberZipcode = dataReader["memberzipcode"].ToString();
                    detail.MemberTelephone = dataReader["membertelephone"].ToString();
                    detail.MemberPosition = dataReader["memberposition"].ToString();
                    detail.DonateType = dataReader["donatetype"].ToString();
                    detail.DonateObjective = dataReader["donateobjective"].ToString();
                    detail.DonatorName = dataReader["donatorname"].ToString();
                    detail.DonatorID = dataReader["donatorid"].ToString();
                    detail.DonatorRegisterNO = dataReader["donatorregisterno"].ToString();
                    detail.DonatorTaxID = dataReader["donatortaxid"].ToString();
                    detail.DonatorHouseNumber = dataReader["donatorhousenumber"].ToString();
                    detail.DonatorMoo = dataReader["donatormoo"].ToString();
                    detail.DonatorBuilding = dataReader["donatorbuilding"].ToString();
                    detail.DonatorSoi = dataReader["donatorsoi"].ToString();
                    detail.DonatorRoad = dataReader["donatorroad"].ToString();
                    detail.DonatorTambon = dataReader["donatortambon"].ToString();
                    detail.DonatorAmphur = dataReader["donatoramphur"].ToString();
                    detail.DonatorProvince = dataReader["donatorprovince"].ToString();
                    detail.DonatorZipcode = dataReader["donatorzipcode"].ToString();
                    detail.DonatorTelephone = dataReader["donatortelephone"].ToString();
                    if (!string.IsNullOrEmpty(dataReader["donateamount"].ToString()))
                        detail.DonateAmount = double.Parse(dataReader["donateamount"].ToString());
                    else
                        detail.DonateAmount = 0;
                    result.Add(detail);
                }
                dataReader.Close();

                foreach (var donateDataModel in result)
                {
                    List<DonateDetailDataModel> detailList = new List<DonateDetailDataModel>();
                    qDetail.Parameters.Clear();
                    qDetail.Parameters.AddWithValue("@documentrunno", donateDataModel.DocumentRunno);
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
                    detailReader.Close();
                    donateDataModel.DonateDetail = detailList;
                }

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
