using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using biblioteca2.Models;
using Microsoft.Web.Helpers;


namespace biblioteca2.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }


        public ActionResult insertar() 
        {
            
            return View();
        }
        //
        // POST: /Account/LogOn
        //int cont = 0;
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            //validacion de usuario con permisos

            if (ModelState.IsValid)
            {
                //REVISION DE BENEADO
                DataClasses1DataContext db = new DataClasses1DataContext();
                var beneado = (from p in db.perfil join us in db.aspnet_Users on p.UserId equals us.UserId where p.UserId == us.UserId && us.UserName == model.UserName select p.beneado).ToArray()[0];
                if (beneado == "false")
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {

                            return Redirect(returnUrl);
                        }
                        else
                        {

                            return RedirectToAction("login", "Home");
                        }
                    }
                    else
                    {

                        ModelState.AddModelError("", "The user name or password provided is incorrect.(Error de Nombreo Contraseña)");
                        if (Session["captcha"] != null && (int)Session["captcha"] > 2 && !ReCaptcha.Validate(privateKey: "6LcD79ESAAAAAHuI-gZuvfEPESrpzMaO-8fT8Bsy"))
                        {
                            ModelState.AddModelError("", "Verifique la Imagen Captvha.  Por favor, inténtelo de nuevo ");
                        }
                        if (Session["captcha"] == null)
                        {
                            Session["captcha"] = 0;
                        }
                        int c = (int)Session["captcha"];
                        c++;
                        Session["captcha"] = c;
                    }


                    // If we got this far, something failed, redisplay form
                    return View(model);
                }
            }
            else {
                ViewBag.beneado = "Lo Sentimos usted esta Beneado por lo tanto no tiene permiso de ver la Pagina";
                return RedirectToAction("login", "Home"); 
            }
                return View(model);
             
        }

        
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
                       
            if (ModelState.IsValid)
            {
                
                
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //registro usuarioroles
                                     
                        DataClasses1DataContext db = new DataClasses1DataContext();
                        //var registro = from UsersId in db.aspnet_Users select UsersId;

                        System.Guid idUs = db.aspnet_Users.Where(a => a.UserName == model.UserName).Select(a => a.UserId).ToArray()[0];
                        System.Guid idRol = db.aspnet_Roles.Where(a => a.RoleName == "Cliente").Select(a => a.RoleId).ToArray()[0];
                        aspnet_UsersInRoles rel = new aspnet_UsersInRoles() { RoleId = idRol, UserId = idUs };
                        db.aspnet_UsersInRoles.InsertOnSubmit(rel);
                        db.SubmitChanges();
                    
                    
                    //registro usuarioroles

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
