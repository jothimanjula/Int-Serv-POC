using InsuranceClaimRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InsuranceClaimRequest.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        InsuranceClaimEntites ie = new InsuranceClaimEntites();
        
        public ActionResult Index()
        {
            
            
           List<InsuranceLineItem> ci = new List<InsuranceLineItem> 
                                           { new InsuranceLineItem
                                             { InsurerId = "", 
                                                 ClaimItemDescription = "",
                                                 BillDate=null} };

           
            return View(ci);
        }

        public ActionResult claimsHome()
        {
            return RedirectToAction("SearchClaim");
        }

        public ActionResult Addclaims()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(List<InsuranceLineItem> ci,  FormCollection fc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (InsuranceClaimEntites dc = new InsuranceClaimEntites())
                    {

                        Insurance insModel = new Insurance();
                        insModel.InsurerId = fc["InsurerId"].ToString();
                        insModel.InsurerName = fc["InsurerName"].ToString();
                        insModel.DateOfBirth = Convert.ToDateTime(fc["DateOfBirth"].ToString());
                        insModel.ClaimReceivedDate = fc["ClaimReceivedDate"].ToString();
                        insModel.ApprovedTotalAmount = fc["ApprovedTotalAmount"] == "" ? 0 : Convert.ToDecimal(fc["ApprovedTotalAmount"]);
                        insModel.ApprovedOverrideAmount = fc["ApprovedOverrideAmount"] == "" ? 0 : Convert.ToDecimal(fc["ApprovedOverrideAmount"]);

                        dc.Insurances.Add(insModel);

                        foreach (var i in ci)
                        {
                            i.InsurerId = fc["InsurerId"].ToString();
                            dc.InsuranceLineItems.Add(i);
                        }
                        dc.SaveChanges();
                        ViewBag.Message = "Data successfully saved!";
                        ModelState.Clear();
                        ci = new List<InsuranceLineItem> { new InsuranceLineItem { InsurerId = "", AmountClaimed = 0, ClaimItemDescription = "" } };
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View("Index", ci);
        }

        //public JsonResult GetClaims(string searchString="CD001")
        //{
        //    // Create Instance of DatabaseContext class for Accessing Database.
        //    InsuranceClaimEntites db = new InsuranceClaimEntites();

        //    var InsData = from ins in db.Insurances
        //                  where (ins.InsurerName == searchString)
        //                  select new
        //                  {
        //                      InsurerId = ins.InsurerId,
        //                      InsurerName = ins.InsurerName,
        //                      DOB = ins.DateOfBirth,
        //                      ClaimRcvdDate = ins.ClaimReceivedDate,
        //                      AprvdTotalAmt = ins.ApprovedTotalAmount,
        //                      AprvdOvrRdAmt = ins.ApprovedOverrideAmount,
        //                      InsuranceDetails = from insLineItm in ins.InsuranceLineItems
        //                                         select new
        //                                         {
        //                                             insLineItm.BillDate,
        //                                             insLineItm.ClaimItemDescription,
        //                                             insLineItm.AmountClaimed,
        //                                             insLineItm.BenefitEmergency,
        //                                             insLineItm.BenefitAmount,
        //                                             insLineItm.BenefitId,
        //                                             insLineItm.ApprovedAmount
        //                                         }



        //                  };
                          


        //    //Setting Paging
         
          

        //    //Get Total Row Count
           

        //    //Setting Sorting
           
        //    //Sending Json Object to View.
        //    var jsonData = new
        //    {
               
               
        //        rows = InsData
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public JsonResult CreateClaim([Bind(Exclude = "InsurerLineItemId")] List<InsuranceLineItem> data)
        {
            StringBuilder msg = new StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    using (InsuranceClaimEntites ie = new InsuranceClaimEntites())
                    {
                         return Json("Saved Successfully", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var errorList = (from item in ModelState
                                     where item.Value.Errors.Any()
                                     select item.Value.Errors[0].ErrorMessage).ToList();

                    return Json(errorList, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var errormessage = "Error occured: " + ex.Message;
                return Json(errormessage, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetBenefitType()
        {
            InsuranceClaimEntites ctx = new InsuranceClaimEntites();
            List<string> listSelectListItems = new List<string>();
            var BenefitType = from benefitVal in ctx.Benefits 
                              select new 
                              { benefitVal.BenefitName, benefitVal.BenefitID };
            var val = BenefitType.ToList();
            return Json(val, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBenefitPerct(int BenefitTyp, bool EmergencyBenefit)
        {

            var BenefitPercentage = (from BP in ie.Benefits
                                         where BP.BenefitID == BenefitTyp
                                         select new
                                         {
                                             BPT = EmergencyBenefit == true ? BP.EmergencyBenefitPercentage : BP.NormalBenefitPercentage
                                         }).FirstOrDefault();

            return Json(BenefitPercentage, JsonRequestBehavior.AllowGet);
        

        }

        public ActionResult SearchClaim()
        {
            return View(ie.Insurances.ToList());
        }
        [HttpGet]
        public ActionResult ClaimDetails(string Id)
        {
            
            try
            {
                var data = ie.InsuranceLineItems.Where(m => m.InsurerId == Id).ToList();
                 //var data =
                 //    from lineItem in ie.InsuranceLineItems
                 //    join bft in ie.Benefits on lineItem.BenefitId equals bft.BenefitID
                 //    where lineItem.InsurerId == Id
                 //    select new InsuranceLineItem
                 //    {
                 //        BillDate = lineItem.BillDate,
                 //        ClaimItemDescription = lineItem.ClaimItemDescription,
                 //        AmountClaimed = lineItem.AmountClaimed,
                 //        BenefitEmergency = lineItem.BenefitEmergency,
                 //        BenefitId = lineItem.BenefitId,
                 //        BenefitAmount = lineItem.BenefitAmount,
                 //        ApprovedAmount = lineItem.ApprovedAmount

                 //    };
               return PartialView("ClaimDetailsPartial", data);
            }
            catch (Exception e)
            { throw e; }
            
           
        }
	}
}