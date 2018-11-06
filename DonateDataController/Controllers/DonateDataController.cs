using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Management;
using MySql.Data.MySqlClient;
using UtilityControllers.Models;

namespace DonateDataController.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class DonateDataController : ApiController
    {
        [Route("DonateData/Add")]
        [HttpPost]
        public IHttpActionResult DonateDataAdd([FromBody] DonateDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    string SQLDetailString =
                        @"INSERT INTO donatedetaildata (DocumentRunno, DetailRunno, Description, Amount, Remark)
                      VALUES (@DocumentRunno, @DetailRunno, @Description, @Amount, @Remark)";
                    MySqlCommand qDetailExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLDetailString
                    };
                    var detail = item.DonateDetail;

                    string SQLString =
                        @"INSERT INTO donatedata (WriteAt, DocumentDate, MemberRunno, MemberId,
                      DonateType, DonateObjective, DonatorRunno, DonatorId, DonateAmount)
                      VALUES (@WriteAt, @DocumentDate, @MemberRunno, @MemberId,
                      @DonateType, @DonateObjective, @DonatorRunno, @DonatorId, @DonateAmount )";
                    MySqlCommand qExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLString
                    };
                    qExe.Parameters.AddWithValue("@WriteAt", item.WriteAt);
                    qExe.Parameters.AddWithValue("@DocumentDate", item.DocumentDate);
                    qExe.Parameters.AddWithValue("@MemberRunno", item.MemberRunno);
                    qExe.Parameters.AddWithValue("@MemberId", item.MemberId);
                    qExe.Parameters.AddWithValue("@DonateType", item.DonateType);
                    qExe.Parameters.AddWithValue("@DonateObjective", item.DonateObjective);
                    qExe.Parameters.AddWithValue("@DonatorRunno", item.DonatorRunno);
                    qExe.Parameters.AddWithValue("@DonatorId", item.DonatorId);
                    qExe.Parameters.AddWithValue("@DonateAmount", item.DonateAmount);
                    qExe.ExecuteNonQuery();
                    long returnid = qExe.LastInsertedId;

                    for (int i = 0; i <= detail.Count - 1; i++)
                    {
                        qDetailExe.Parameters.Clear();
                        qDetailExe.Parameters.AddWithValue("@DocumentRunno", returnid);
                        qDetailExe.Parameters.AddWithValue("@DetailRunno", i + 1);
                        qDetailExe.Parameters.AddWithValue("@Description", detail[i].Description);
                        qDetailExe.Parameters.AddWithValue("@Amount", detail[i].Amount);
                        qDetailExe.Parameters.AddWithValue("@Remark", detail[i].Remark);
                        qDetailExe.ExecuteNonQuery();
                    }
                    conn.CloseConnection();
                    return Ok(returnid.ToString());
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/Edit")]
        [HttpPost]
        public IHttpActionResult DonateDataEdit([FromBody] DonateDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    /*
                    string SQLDetailDeleteString = "DELETE FROM donatedetaildata WHERE DocumentRunno = @DocumentRunno";
                    string SQLDetailString = @"INSERT INTO donatedetaildata (DocumentRunno, DetailRunno, Description, Amount, Remark)
                                               VALUES (@DocumentRunno, @DetailRunno, @Description, @Amount, @Remark)";

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
                    */
                    string SQLString =
                        @"UPDATE donatedata SET WriteAt = @WriteAt, DocumentDate = @DocumentDate, MemberRunno = @MemberRunno,
                                     MemberId = @MemberId, DonateType = @DonateType, DonateObjective = @DonateObjective, DonatorRunno = @DonatorRunno,
                                     DonatorId = @DonatorId, DonateAmount = @DonateAmount WHERE DocumentRunno = @DocumentRunno";
                    MySqlCommand qExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLString
                    };
                    qExe.Parameters.AddWithValue("@documentrunno", item.DocumentRunno);
                    qExe.Parameters.AddWithValue("@WriteAt", item.WriteAt);
                    qExe.Parameters.AddWithValue("@DocumentDate", item.DocumentDate);
                    qExe.Parameters.AddWithValue("@MemberRunno", item.MemberRunno);
                    qExe.Parameters.AddWithValue("@MemberId", item.MemberId);
                    qExe.Parameters.AddWithValue("@DonateType", item.DonateType);
                    qExe.Parameters.AddWithValue("@DonateObjective", item.DonateObjective);
                    qExe.Parameters.AddWithValue("@DonatorRunno", item.DonatorRunno);
                    qExe.Parameters.AddWithValue("@DonatorId", item.DonatorId);
                    qExe.Parameters.AddWithValue("@DonateAmount", item.DonateAmount);
                    qExe.ExecuteNonQuery();
                    /*
                    for (int i = 0; i <= detail.Count - 1; i++)
                    {
                        qDetailExe.Parameters.Clear();
                        qDetailExe.Parameters.AddWithValue("@DocumentRunno", item.DocumentRunno);
                        qDetailExe.Parameters.AddWithValue("@DetailRunno", i + 1);
                        qDetailExe.Parameters.AddWithValue("@Description", detail[i].Description);
                        qDetailExe.Parameters.AddWithValue("@Amount", detail[i].Amount);
                        qDetailExe.Parameters.AddWithValue("@Remark", detail[i].Remark);
                        qDetailExe.ExecuteNonQuery();
                    }
                    */
                    conn.CloseConnection();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/Delete/{id}")]
        [HttpPost]
        public IHttpActionResult DonateDataDelete(string id)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    string sqlString = @"delete from donatedata where DocumentRunno = @DocumentRunno";
                    MySqlCommand qExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = sqlString
                    };
                    if (string.IsNullOrEmpty(id))
                    {
                        return BadRequest("Document Key is null!");
                    }
                    qExe.Parameters.AddWithValue("@DocumentRunno", id);
                    qExe.ExecuteNonQuery();
                    sqlString = "delete from donatedetaildata where DocumentRunno = @DocumentRunno";
                    qExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = sqlString
                    };
                    qExe.Parameters.AddWithValue("@DocumentRunno", id);
                    qExe.ExecuteNonQuery();
                    conn.CloseConnection();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/GetByID/{id}")]
        [HttpGet]
        public IHttpActionResult DonateDataListbyRunno(string id)
        {
            string AddressGenerate;
            DonateDataModel result = new DonateDataModel();
            result.DonateDetail = new List<DonateDetailDataModel>();
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    string sqlString;
                    if (!string.IsNullOrEmpty(id))
                        sqlString = @"select doc.*, docs.DetailRunno, docs.Description, docs.Amount, docs.Remark, 
                                  mem.MemberRunno, mem.MemberId, mem.MemberPreName, mem.MemberName, mem.MemberSurname,
                                  mem.PositionNo, mem.BirthDate, mem.HouseNumber mHouseNumber, mem.Soi mSoi, mem.Road mRoad, 
                                  mem.Moo mMoo, mem.Building mBuilding, mem.Tambon mTambon, mem.Amphur mAmphur, 
                                  mem.Province mProvince, mem.Zipcode mZipcode, mem.Telephone mTelephone,
                                  don.DonatorRunno, don.DonatorId, don.DonatorPreName, don.DonatorName, don.DonatorSurName,
                                  don.DonatorCitizenId, don.DonatorRegisterNo, don.DonatorTaxId, don.HouseNumber dHouseNumber,
                                  don.Soi dSoi, don.Road dRoad, don.Moo dMoo, don.Building dBuilding, don.Tambon dTambon,
                                  don.Amphur dAmphur, don.Province dProvince, don.Zipcode dZipcode, don.Telephone dTelephone, par.positionName
                                  from donatedetaildata docs, donatedata doc left join memberdata mem on mem.MemberRunno = doc.MemberRunno
                                  left join donatordata don on don.DonatorRunno = doc.DonatorRunno
                                  left join partyposition par on par.PositionNo = mem.PositionNO
                                  where doc.DocumentRunno = @DocumentRunno
                                  and doc.DocumentRunno = docs.DocumentRunno order by DocumentRunno, DetailRunno";
                    else
                        return Json("Document Number is blank!");
                    string sqlDetail =
                        @"select * from donatedetaildata where DocumentRunno = @DocumentRunno order by DetailRunno";
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
                    qExe.Parameters.AddWithValue("@DocumentRunno", id);
                    qDetail.Parameters.AddWithValue("@DocumentRunno", id);
                    MySqlDataReader detailReader = qDetail.ExecuteReader();
                    while (detailReader.Read())
                    {
                        DonateDetailDataModel detailRow = new DonateDetailDataModel();
                        detailRow.DocumentRunno = int.Parse(detailReader["DocumentRunno"].ToString());
                        detailRow.DetailRunno = int.Parse(detailReader["detailrunno"].ToString());
                        detailRow.Description = detailReader["description"].ToString();
                        detailRow.Amount = double.Parse(detailReader["amount"].ToString());
                        detailRow.Remark = detailReader["remark"].ToString();
                        result.DonateDetail.Add(detailRow);
                    }
                    detailReader.Close();
                    MySqlDataReader dataReader = qExe.ExecuteReader();
                    while (dataReader.Read())
                    {
                        result.DocumentRunno = int.Parse(dataReader["DocumentRunno"].ToString());
                        result.WriteAt = dataReader["writeat"].ToString();
                        result.DocumentDate = Convert.ToDateTime(dataReader["DocumentDate"].ToString(),
                            new CultureInfo("en-US"));
                        result.MemberRunno = int.Parse(dataReader["MemberRunno"].ToString());
                        result.DonateType = dataReader["DonateType"].ToString();
                        result.DonateObjective = dataReader["DonateObjective"].ToString();
                        result.MemberId = dataReader["MemberId"].ToString();
                        result.MemberName = dataReader["memberPrename"].ToString() + dataReader["membername"].ToString() +
                                            "   " + dataReader["MemberSurName"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["birthdate"].ToString()))
                            result.MemberBirthdate = Convert.ToDateTime(dataReader["birthdate"].ToString(),
                                new CultureInfo("en-US"));
                        else
                            result.MemberBirthdate = null;
                        AddressGenerate = "";
                        if (!string.IsNullOrEmpty(dataReader["mhousenumber"].ToString()))
                            AddressGenerate = " บ้านเลขที่ " + dataReader["mhousenumber"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mmoo"].ToString()))
                            AddressGenerate = " หมู่ที่ " + dataReader["mmoo"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mbuilding"].ToString()))
                            AddressGenerate = " อาคาร " + dataReader["mbuilding"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["msoi"].ToString()))
                            AddressGenerate = " ซอย " + dataReader["msoi"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mroad"].ToString()))
                            AddressGenerate = " ถนน " + dataReader["mroad"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mtambon"].ToString()))
                            AddressGenerate = " ตำบล/แขวง " + dataReader["mtambon"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mamphur"].ToString()))
                            AddressGenerate = " อำเภอ/เขต " + dataReader["mamphur"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mprovince"].ToString()))
                            AddressGenerate = " จังหวัด " + dataReader["mprovince"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["mzipcode"].ToString()))
                            AddressGenerate = " รหัสไปรษณีย์ " + dataReader["mzipcode"].ToString();
                        result.MemberAddress = AddressGenerate.Trim();
                        result.MemberTelephone = dataReader["mtelephone"].ToString();
                        result.PositionNo = int.Parse(dataReader["positionNo"].ToString());
                        result.MemberPosition = dataReader["PositionName"].ToString();
                        result.DonatorRunno = int.Parse(dataReader["DonatorRunno"].ToString());
                        result.DonatorId = dataReader["DonatorId"].ToString();
                        result.DonatorName = dataReader["donatorPrename"].ToString() + dataReader["donatorname"].ToString() +
                                             "  " + dataReader["donatorSurname"].ToString();
                        result.DonatorRegisterNO = dataReader["donatorregisterno"].ToString();
                        result.DonatorTaxID = dataReader["donatortaxid"].ToString();
                        AddressGenerate = "";
                        if (!string.IsNullOrEmpty(dataReader["dhousenumber"].ToString()))
                            AddressGenerate = " บ้านเลขที่ " + dataReader["dhousenumber"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["dmoo"].ToString()))
                            AddressGenerate = " หมู่ที่ " + dataReader["dmoo"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["dbuilding"].ToString()))
                            AddressGenerate = " อาคาร " + dataReader["dbuilding"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["dsoi"].ToString()))
                            AddressGenerate = " ซอย " + dataReader["dsoi"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["droad"].ToString()))
                            AddressGenerate = " ถนน " + dataReader["droad"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["dtambon"].ToString()))
                            AddressGenerate = " ตำบล/แขวง " + dataReader["dtambon"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["damphur"].ToString()))
                            AddressGenerate = " อำเภอ/เขต " + dataReader["damphur"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["dprovince"].ToString()))
                            AddressGenerate = " จังหวัด " + dataReader["dprovince"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["dzipcode"].ToString()))
                            AddressGenerate = " รหัสไปรษณีย์ " + dataReader["dzipcode"].ToString();
                        result.DonatorAddress = AddressGenerate.Trim();
                        result.DonatorTelephone = dataReader["dtelephone"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["DonateAmount"].ToString()))
                            result.DonateAmount = double.Parse(dataReader["DonateAmount"].ToString());
                        else
                            result.DonateAmount = 0;
                    }
                    dataReader.Close();
                    conn.CloseConnection();
                    return Json(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/ListAll")]
        [HttpGet]
        public IHttpActionResult DonateDataList()
        {
            List<DonateDataModel> result = new List<DonateDataModel>();
            DonateDataModel row = null;
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    string SQLString = @"select doc.*, docs.DetailRunno, docs.Description, docs.Amount, docs.Remark, 
                                  mem.MemberRunno, mem.MemberId, mem.MemberPreName, mem.MemberName, mem.MemberSurname,
                                  mem.PositionNo, mem.BirthDate, mem.HouseNumber mHouseNumber, mem.Soi mSoi, mem.Road mRoad, 
                                  mem.Moo mMoo, mem.Building mBuilding, mem.Tambon mTambon, mem.Amphur mAmphur, 
                                  mem.Province mProvince, mem.Zipcode mZipcode, mem.Telephone mTelephone,
                                  don.DonatorRunno, don.DonatorId, don.DonatorPreName, don.DonatorName, don.DonatorSurName,
                                  don.DonatorCitizenId, don.DonatorRegisterNo, don.DonatorTaxId, don.HouseNumber dHouseNumber,
                                  don.Soi dSoi, don.Road dRoad, don.Moo dMoo, don.Building dBuilding, don.Tambon dTambon,
                                  don.Amphur dAmphur, don.Province dProvince, don.Zipcode dZipcode, don.Telephone dTelephone, par.positionName
                                  from donatedetaildata docs, donatedata doc left join memberdata mem on mem.MemberRunno = doc.MemberRunno
                                  left join donatordata don on don.DonatorRunno = doc.DonatorRunno
                                  left join partyposition par on par.PositionNo = mem.PositionNO
                                  where doc.DocumentRunno = docs.DocumentRunno order by DocumentRunno, DetailRunno";
                    MySqlCommand qExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLString
                    };

                    MySqlDataReader dataReader = qExe.ExecuteReader();
                    int RunnoBreak = 0;
                    int DetailRunnoBreak = 0;
                    string AddressGenerate;
                    while (dataReader.Read())
                    {
                        if (RunnoBreak != int.Parse(dataReader["DocumentRunno"].ToString()))
                        {
                            if (row != null)
                                result.Add(row);
                            row = new DonateDataModel();
                            row.DonateDetail = new List<DonateDetailDataModel>();
                            RunnoBreak = int.Parse(dataReader["DocumentRunno"].ToString());
                            DetailRunnoBreak = 0;

                            row.DocumentRunno = int.Parse(dataReader["DocumentRunno"].ToString());
                            row.WriteAt = dataReader["writeat"].ToString();
                            row.DocumentDate = Convert.ToDateTime(dataReader["DocumentDate"].ToString(),
                                new CultureInfo("en-US"));
                            row.MemberRunno = int.Parse(dataReader["MemberRunno"].ToString());
                            row.DonateType = dataReader["DonateType"].ToString();
                            row.DonateObjective = dataReader["DonateObjective"].ToString();
                            row.MemberId = dataReader["MemberId"].ToString();
                            row.MemberName = dataReader["memberPrename"].ToString() + dataReader["membername"].ToString() +
                                             "   " + dataReader["MemberSurName"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["birthdate"].ToString()))
                                row.MemberBirthdate = Convert.ToDateTime(dataReader["birthdate"].ToString(),
                                    new CultureInfo("en-US"));
                            else
                                row.MemberBirthdate = null;
                            AddressGenerate = "";
                            if (!string.IsNullOrEmpty(dataReader["mhousenumber"].ToString()))
                                AddressGenerate = " บ้านเลขที่ " + dataReader["mhousenumber"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mmoo"].ToString()))
                                AddressGenerate = " หมู่ที่ " + dataReader["mmoo"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mbuilding"].ToString()))
                                AddressGenerate = " อาคาร " + dataReader["mbuilding"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["msoi"].ToString()))
                                AddressGenerate = " ซอย " + dataReader["msoi"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mroad"].ToString()))
                                AddressGenerate = " ถนน " + dataReader["mroad"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mtambon"].ToString()))
                                AddressGenerate = " ตำบล/แขวง " + dataReader["mtambon"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mamphur"].ToString()))
                                AddressGenerate = " อำเภอ/เขต " + dataReader["mamphur"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mprovince"].ToString()))
                                AddressGenerate = " จังหวัด " + dataReader["mprovince"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["mzipcode"].ToString()))
                                AddressGenerate = " รหัสไปรษณีย์ " + dataReader["mzipcode"].ToString();
                            row.MemberAddress = AddressGenerate.Trim();
                            row.MemberTelephone = dataReader["mtelephone"].ToString();
                            row.PositionNo = int.Parse(dataReader["positionNo"].ToString());
                            row.MemberPosition = dataReader["PositionName"].ToString();
                            row.DonatorRunno = int.Parse(dataReader["DonatorRunno"].ToString());
                            row.DonatorId = dataReader["DonatorId"].ToString();
                            row.DonatorName = dataReader["donatorPrename"].ToString() + dataReader["donatorname"].ToString() +
                                              "  " + dataReader["donatorSurname"].ToString();
                            row.DonatorRegisterNO = dataReader["donatorregisterno"].ToString();
                            row.DonatorTaxID = dataReader["donatortaxid"].ToString();
                            AddressGenerate = "";
                            if (!string.IsNullOrEmpty(dataReader["dhousenumber"].ToString()))
                                AddressGenerate = " บ้านเลขที่ " + dataReader["dhousenumber"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["dmoo"].ToString()))
                                AddressGenerate = " หมู่ที่ " + dataReader["dmoo"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["dbuilding"].ToString()))
                                AddressGenerate = " อาคาร " + dataReader["dbuilding"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["dsoi"].ToString()))
                                AddressGenerate = " ซอย " + dataReader["dsoi"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["droad"].ToString()))
                                AddressGenerate = " ถนน " + dataReader["droad"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["dtambon"].ToString()))
                                AddressGenerate = " ตำบล/แขวง " + dataReader["dtambon"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["damphur"].ToString()))
                                AddressGenerate = " อำเภอ/เขต " + dataReader["damphur"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["dprovince"].ToString()))
                                AddressGenerate = " จังหวัด " + dataReader["dprovince"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["dzipcode"].ToString()))
                                AddressGenerate = " รหัสไปรษณีย์ " + dataReader["dzipcode"].ToString();
                            row.DonatorAddress = AddressGenerate.Trim();
                            row.DonatorTelephone = dataReader["dtelephone"].ToString();
                            if (!string.IsNullOrEmpty(dataReader["DonateAmount"].ToString()))
                                row.DonateAmount = double.Parse(dataReader["DonateAmount"].ToString());
                            else
                                row.DonateAmount = 0;
                        }
                        if (DetailRunnoBreak != int.Parse(dataReader["DetailRunno"].ToString()))
                        {
                            DonateDetailDataModel detailRow = new DonateDetailDataModel();
                            detailRow.DocumentRunno = int.Parse(dataReader["DocumentRunno"].ToString());
                            detailRow.DetailRunno = int.Parse(dataReader["detailrunno"].ToString());
                            detailRow.Description = dataReader["description"].ToString();
                            detailRow.Amount = double.Parse(dataReader["amount"].ToString());
                            detailRow.Remark = dataReader["remark"].ToString();
                            row.DonateDetail.Add(detailRow);
                            DetailRunnoBreak = int.Parse(dataReader["DetailRunno"].ToString());
                        }
                    }
                    if (row != null)
                        result.Add(row);
                    dataReader.Close();

                    conn.CloseConnection();
                    return Json(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/AddDetail")]
        [HttpPost]
        public IHttpActionResult AddDonateDetailData([FromBody] DonateDetailDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    int maxRunno = 0;
                    string findMax = "select max(DetailRunno) maxRunno from donatedetaildata where DocumentRunno = " + item.DocumentRunno.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(findMax, conn.connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "maxRunno");
                    DataTable dt = ds.Tables["maxRunno"];
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["maxRunno"].ToString()))
                            maxRunno = int.Parse(row["maxRunno"].ToString()) + 1;
                        else
                            maxRunno = 1;
                    }
                    string SQLDetailString =
                    @"INSERT INTO donatedetaildata (DocumentRunno, DetailRunno, Description, Amount, Remark)
                      VALUES (@DocumentRunno, @DetailRunno, @Description, @Amount, @Remark)";
                    MySqlCommand qDetailExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLDetailString
                    };
                    qDetailExe.Parameters.Clear();
                    qDetailExe.Parameters.AddWithValue("@DocumentRunno", item.DocumentRunno);
                    qDetailExe.Parameters.AddWithValue("@DetailRunno", maxRunno);
                    qDetailExe.Parameters.AddWithValue("@Description", item.Description);
                    qDetailExe.Parameters.AddWithValue("@Amount", item.Amount);
                    qDetailExe.Parameters.AddWithValue("@Remark", item.Remark);
                    qDetailExe.ExecuteNonQuery();
                    conn.CloseConnection();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/EditDetail")]
        [HttpPost]
        public IHttpActionResult EditDonateDetailData([FromBody] DonateDetailDataModel item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    string SQLDetailString =
                        @"UPDATE donatedetaildata SET DocumentRunno = @DocumentRunno, DetailRunno = @DetailRunno,
                          Description = @Description, Amount = @Amount, Remark = @Remark 
                          WHERE DocumentRunno = @DocumentRunno AND DetailRunno = @DetailRunno";
                    MySqlCommand qDetailExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLDetailString
                    };
                    qDetailExe.Parameters.Clear();
                    qDetailExe.Parameters.AddWithValue("@DocumentRunno", item.DocumentRunno);
                    qDetailExe.Parameters.AddWithValue("@DetailRunno", item.DetailRunno);
                    qDetailExe.Parameters.AddWithValue("@Description", item.Description);
                    qDetailExe.Parameters.AddWithValue("@Amount", item.Amount);
                    qDetailExe.Parameters.AddWithValue("@Remark", item.Remark);
                    qDetailExe.ExecuteNonQuery();

                    conn.CloseConnection();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/Delete/{ID}/{detailID}")]
        [HttpPost]
        public IHttpActionResult DeleteDonateDetailData(string ID, string detailID)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    string SQLDetailDeleteString = "DELETE FROM donatedetaildata WHERE DocumentRunno = @DocumentRunno and DetailRunno = @DetailRunno";
                    MySqlCommand qDetailExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLDetailDeleteString
                    };
                    qDetailExe.Parameters.AddWithValue("@DocumentRunno", ID);
                    qDetailExe.Parameters.AddWithValue("@DetailRunno", detailID);
                    qDetailExe.ExecuteNonQuery();
                    conn.CloseConnection();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("DonateData/GetDetailData/{DocumentRunno}")]
        [HttpGet]
        public IHttpActionResult GetDetailData(string DocumentRunno)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            if (conn.OpenConnection())
            {
                try
                {
                    List<DonateDetailDataModel> result = new List<DonateDetailDataModel>();
                    string SQLDetailDeleteString = "select * from donatedetaildata WHERE DocumentRunno = @DocumentRunno";
                    MySqlCommand qDetailExe = new MySqlCommand
                    {
                        Connection = conn.connection,
                        CommandText = SQLDetailDeleteString
                    };
                    qDetailExe.Parameters.AddWithValue("@DocumentRunno", DocumentRunno);
                    MySqlDataReader dataReader = qDetailExe.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DonateDetailDataModel row = new DonateDetailDataModel();
                        row.DocumentRunno = int.Parse(dataReader["DocumentRunno"].ToString());
                        row.DetailRunno = int.Parse(dataReader["detailrunno"].ToString());
                        row.Description = dataReader["description"].ToString();
                        row.Amount = double.Parse(dataReader["amount"].ToString());
                        row.Remark = dataReader["remark"].ToString();
                        result.Add(row);
                    }
                    conn.CloseConnection();
                    return Json(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("Donator/Add")]
        [HttpPost]
        public IHttpActionResult AddDonatorData([FromBody] DonatorData item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            string SQLString;
            if (conn.OpenConnection())
            {
                SQLString = @"INSERT INTO donatordata (DonatorId, DonatorPreName, DonatorName, DonatorSurName,
                              DonatorCitizenId, DonatorRegisterNo, DonatorTaxId, HouseNumber, Soi, Road, Moo, Building, Tambon,
                              Amphur, Province, ZipCode, Telephone) VALUES (@DonatorId, @DonatorPreName,
                              @DonatorName, @DonatorSurName, @DonatorCitizenId, @DonatorRegisterNo, @DonatorTaxId, @HouseNumber,
                              @Soi, @Road, @Moo, @Building, @Tambon, @Amphur, @Province, @ZipCode, @Telephone)";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                qExe.Parameters.AddWithValue("@DonatorId", item.DonatorId);
                qExe.Parameters.AddWithValue("@DonatorPreName", item.DonatorPreName);
                qExe.Parameters.AddWithValue("@DonatorName", item.DonatorName);
                qExe.Parameters.AddWithValue("@DonatorSurName", item.DonatorSurName);
                qExe.Parameters.AddWithValue("@DonatorCitizenId", item.DonatorCitizenId);
                qExe.Parameters.AddWithValue("@DonatorRegisterNo", item.DonatorRegisterNo);
                qExe.Parameters.AddWithValue("@DonatorTaxId", item.DonatorTaxId);
                qExe.Parameters.AddWithValue("@HouseNumber", item.HouseNumber);
                qExe.Parameters.AddWithValue("@Soi", item.Soi);
                qExe.Parameters.AddWithValue("@Road", item.Road);
                qExe.Parameters.AddWithValue("@Moo", item.Moo);
                qExe.Parameters.AddWithValue("@Building", item.Building);
                qExe.Parameters.AddWithValue("@Tambon", item.Tambon);
                qExe.Parameters.AddWithValue("@Amphur", item.Amphur);
                qExe.Parameters.AddWithValue("@Province", item.Province);
                qExe.Parameters.AddWithValue("@ZipCode", item.ZipCode);
                qExe.Parameters.AddWithValue("@Telephone", item.Telephone);
                qExe.ExecuteNonQuery();
                long returnid = qExe.LastInsertedId;
                conn.CloseConnection();
                return Ok(returnid.ToString());
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("Donator/Edit")]
        [HttpPost]
        public IHttpActionResult EditDonatorData([FromBody] DonatorData item)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            string SQLString;
            if (conn.OpenConnection())
            {
                SQLString = @"UPDATE donatordata SET DonatorRunno = @DonatorRunno, DonatorId = @DonatorId, DonatorPreName = @DonatorPreName,
                              DonatorName = @DonatorName, DonatorSurName = @DonatorSurName, DonatorCitizenId = @DonatorCitizenId,
                              DonatorRegisterNo = @DonatorRegisterNo, DonatorTaxId = @DonatorTaxId, HouseNumber = @HouseNumber,
                              Soi = @Soi, Road = @Road, Moo = @Moo, Building = @Building, Tambon = @Tambon, Amphur = @Amphur,
                              Province = @Province, Zipcode = @Zipcode, Telephone = @Telephone WHERE DonatorRunno = @DonatorRunno ";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                qExe.Parameters.AddWithValue("@DonatorRunno", item.DonatorRunno);
                qExe.Parameters.AddWithValue("@DonatorId", item.DonatorId);
                qExe.Parameters.AddWithValue("@DonatorPreName", item.DonatorPreName);
                qExe.Parameters.AddWithValue("@DonatorName", item.DonatorName);
                qExe.Parameters.AddWithValue("@DonatorSurName", item.DonatorSurName);
                qExe.Parameters.AddWithValue("@DonatorCitizenId", item.DonatorCitizenId);
                qExe.Parameters.AddWithValue("@DonatorRegisterNo", item.DonatorRegisterNo);
                qExe.Parameters.AddWithValue("@DonatorTaxId", item.DonatorTaxId);
                qExe.Parameters.AddWithValue("@HouseNumber", item.HouseNumber);
                qExe.Parameters.AddWithValue("@Soi", item.Soi);
                qExe.Parameters.AddWithValue("@Road", item.Road);
                qExe.Parameters.AddWithValue("@Moo", item.Moo);
                qExe.Parameters.AddWithValue("@Building", item.Building);
                qExe.Parameters.AddWithValue("@Tambon", item.Tambon);
                qExe.Parameters.AddWithValue("@Amphur", item.Amphur);
                qExe.Parameters.AddWithValue("@Province", item.Province);
                qExe.Parameters.AddWithValue("@ZipCode", item.ZipCode);
                qExe.Parameters.AddWithValue("@Telephone", item.Telephone);
                qExe.ExecuteNonQuery();
                conn.CloseConnection();
                return Ok();
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("Donator/Delete/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteDonatorData(string id)
        {
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            string SQLString;
            if (conn.OpenConnection())
            {
                SQLString = @"DELETE FROM donatordata WHERE DonatorRunno = @DonatorRunno";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                qExe.Parameters.AddWithValue("@DonatorRunno", id);
                qExe.ExecuteNonQuery();
                conn.CloseConnection();
                return Ok();
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }

        [Route("Donator/ListAll")]
        [HttpGet]
        public IHttpActionResult ListAllDonator()
        {
            List<DonatorData> result = new List<DonatorData>();
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            string SQLString;
            if (conn.OpenConnection())
            {
                SQLString = @"SELECT * FROM donatordata order by DonatorId";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                MySqlDataReader dataReader = qExe.ExecuteReader();
                while (dataReader.Read())
                {
                    DonatorData detail = new DonatorData();
                    detail.DonatorRunno = int.Parse(dataReader["DonatorRunno"].ToString());
                    detail.DonatorId = dataReader["DonatorId"].ToString();
                    detail.DonatorPreName = dataReader["DonatorPreName"].ToString();
                    detail.DonatorName = dataReader["DonatorName"].ToString();
                    detail.DonatorSurName = dataReader["DonatorSurName"].ToString();
                    detail.DonatorCitizenId = dataReader["DonatorCitizenId"].ToString();
                    detail.DonatorRegisterNo = dataReader["DonatorRegisterNo"].ToString();
                    detail.DonatorTaxId = dataReader["DonatorTaxId"].ToString();
                    detail.HouseNumber = dataReader["HouseNumber"].ToString();
                    detail.Soi = dataReader["Soi"].ToString();
                    detail.Road = dataReader["Road"].ToString();
                    detail.Moo = dataReader["Moo"].ToString();
                    detail.Building = dataReader["Building"].ToString();
                    detail.Tambon = dataReader["Tambon"].ToString();
                    detail.Amphur = dataReader["Amphur"].ToString();
                    detail.Province = dataReader["Province"].ToString();
                    detail.ZipCode = dataReader["ZipCode"].ToString();
                    detail.Telephone = dataReader["Telephone"].ToString();
                    result.Add(detail);
                }
                dataReader.Close();
                dataReader.Dispose();
                return Json(result);
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
        [Route("Donator/GetByID/{id}")]
        [HttpGet]
        public IHttpActionResult GetDonatorData(string id)
        {
            DonatorData result = new DonatorData();
            DBConnector.DBConnector conn = new DBConnector.DBConnector();
            string SQLString;
            if (conn.OpenConnection())
            {
                SQLString = @"SELECT * FROM donatordata where DonatorRunno = '" + id + @"'
                              order by DonatorId";
                MySqlCommand qExe = new MySqlCommand
                {
                    Connection = conn.connection,
                    CommandText = SQLString
                };
                MySqlDataReader dataReader = qExe.ExecuteReader();
                while (dataReader.Read())
                {
                    result.DonatorRunno = int.Parse(dataReader["DonatorRunno"].ToString());
                    result.DonatorId = dataReader["DonatorId"].ToString();
                    result.DonatorPreName = dataReader["DonatorPreName"].ToString();
                    result.DonatorName = dataReader["DonatorName"].ToString();
                    result.DonatorSurName = dataReader["DonatorSurName"].ToString();
                    result.DonatorCitizenId = dataReader["DonatorCitizenId"].ToString();
                    result.DonatorRegisterNo = dataReader["DonatorRegisterNo"].ToString();
                    result.DonatorTaxId = dataReader["DonatorTaxId"].ToString();
                    result.HouseNumber = dataReader["HouseNumber"].ToString();
                    result.Soi = dataReader["Soi"].ToString();
                    result.Road = dataReader["Road"].ToString();
                    result.Moo = dataReader["Moo"].ToString();
                    result.Building = dataReader["Building"].ToString();
                    result.Tambon = dataReader["Tambon"].ToString();
                    result.Amphur = dataReader["Amphur"].ToString();
                    result.Province = dataReader["Province"].ToString();
                    result.ZipCode = dataReader["ZipCode"].ToString();
                    result.Telephone = dataReader["Telephone"].ToString();
                }
                dataReader.Close();
                dataReader.Dispose();
                return Json(result);
            }
            else
            {
                return BadRequest("Database connect fail!");
            }
        }
    }
}