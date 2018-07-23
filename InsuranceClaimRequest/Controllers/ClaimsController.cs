using InsuranceClaimRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InsuranceClaimRequest.Controllers
{
    //[Authorize]
    public class ClaimsController : Controller
    {
        InsuranceClaimEntites ie = new InsuranceClaimEntites();
        
        public ActionResult Index()
        {
            
            
            List<InsuranceLineItem> ci = new List<InsuranceLineItem> { new InsuranceLineItem { InsurerId = "", AmountClaimed= 0, ClaimItemDescription = "" } };
            return View(ci);
        }
        public ActionResult BulkData(List<InsuranceLineItem> ci)
        {
            if (ModelState.IsValid)
            {
                using (InsuranceClaimEntites dc = new InsuranceClaimEntites())
                {
                    foreach (var i in ci)
                    {
                        dc.InsuranceLineItems.Add(i);
                    }
                    dc.SaveChanges();
                    ViewBag.Message = "Data successfully saved!";
                    ModelState.Clear();
                    ci = new List<InsuranceLineItem> { new InsuranceLineItem { InsurerId = "", AmountClaimed = 0, ClaimItemDescription = "" } };
                }
            }
            return View(ci);
        }

        public JsonResult GetCustomers(string sord, int page, int rows, string searchString)
        {
            // Create Instance of DatabaseContext class for Accessing Database.
            InsuranceClaimEntites db = new InsuranceClaimEntites();

            //Setting Paging
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = db.InsuranceLineItems.Select(
                a => new
                {
                    a.InsurerLineItemId,
                    a.BillDate,
                    a.ClaimItemDescription,
                    a.AmountClaimed,
                    a.BenefitEmergency,
                    a.BenefitId,
                    a.BenefitAmount,
                    a.ApprovedAmount,
                });

            //Get Total Row Count
            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            //Setting Sorting
            if (sord.ToUpper() == "DESC")
            {
                Results = Results.OrderByDescending(s => s.InsurerLineItemId);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Results = Results.OrderBy(s => s.BillDate);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            //Setting Search
            if (!string.IsNullOrEmpty(searchString))
            {
                Results = Results.Where(m => m.ClaimItemDescription == searchString || m.ClaimItemDescription == searchString);
            }
            //Sending Json Object to View.
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

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

	}
}