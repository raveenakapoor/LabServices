using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabServices.DAL;
using LabServices.Models;
using System.Security.Cryptography;
namespace LabServices.Controllers
{
    public class RegisterController : Controller
    {  // Define default min and max password lengths.
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

        // Define supported password characters divided into groups.
        // You can add (or remove) characters to (from) these groups.
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "23456789";
        private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        public ActionResult HCPRegister()
        {
            return View("HCPRegister");
        }
        public ActionResult RegisterHcp(HCPModel obj)
        {
            try
            {


                HCPDalcs ob = new HCPDalcs();
                obj.Passsword = RandomPassword.Generate(10, 11);
                string HCPid = ob.insert_HCP(obj);
                if (HCPid == "1" || HCPid == "0")
                {
                    string msg = "Try Again";
                    ViewData["Msg"] = msg;
                }
                else
                {
                    string msg = "Registered Successfully!!!";
                    ViewData["Msg"] = msg;
                    ViewData["HCPId"] = HCPid;
                    ViewData["Password"] = obj.Passsword;
                }
            }
            catch (Exception ex)
            {
                //ex
            }
            return View("HCPRegister");
        }

        public static string Generate(int minLength,int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the password strength.
            char[][] charGroups = new char[][]
            {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
            };

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = BitConverter.ToInt32(randomBytes, 0);

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string(password);
        }
    
    public static string Generate(int length)
        {
            return Generate(length, length);
        }
        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                            DEFAULT_MAX_PASSWORD_LENGTH);
        }

        public JsonResult GetCity(int zipcode)
        {
            LSPDal ob = new LSPDal();
            LSPModel obj = new LSPModel();
            obj = ob.GetState(zipcode);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    
        public ActionResult PatientRegister()
        {
            return View();
        }
      
        public ActionResult RegisterPatient(PatientModel obj)
        {
            //if(ModelState.IsValid)
            //{
            try
            {


                PatientDal ob = new PatientDal();
                obj.Passsword = RandomPassword.Generate(10, 11);
                if (Session["UserId"] != null)
                {

                    obj.ParentId = Session["UserId"].ToString();

                }
                else
                    obj.ParentId = "0";
                string PatientId = ob.insert_Patient(obj);
                if (PatientId == "1" || PatientId == "0")
                {
                    string msg = "Try Again";
                    ViewData["Msg"] = msg;
                }
                else
                {
                    string msg = "Registered Successfully!!!";
                    ViewData["Msg"] = msg;
                    ViewData["LabId"] = PatientId;
                    ViewData["Password"] = obj.Passsword;
                }
            }
            catch (Exception ex)
            {

            }
            return View("PatientRegister");
        }


        // GET: Register
        public ActionResult LabService()
        {
           

                return View();
        }
        [HttpPost]
        public ActionResult LabService(LSPModel obj)
        {
            //if(ModelState.IsValid)
            //{
            try
            {


                LSPDal ob = new LSPDal();
                obj.Passsword = RandomPassword.Generate(10, 11);
                if(Session["UserId"] !=null)
                {
                  
                    obj.ParentId = Session["UserId"].ToString();
                    
                }
                else
                    obj.ParentId = "0";
                string Labid = ob.insert_LSP(obj);
                if (Labid == "1" || Labid == "0")
                {
                    string msg = "Try Again";
                    ViewData["Msg"] = msg;
                }
                else
                {
                    string msg = "Registered Successfully!!!";
                    ViewData["Msg"] = msg;
                    ViewData["LabId"] = Labid;
                    ViewData["Password"] = obj.Passsword;
                  //  ViewData["Category"] = null;
                   // if(Session["UserId"]!=null)
                  //  {
                   //     ViewData["Category"] = "yes";
                  //  }
                }
            }
            catch(Exception ex)
            {
                //ex
            }
            return View();
        }
    

    }
}