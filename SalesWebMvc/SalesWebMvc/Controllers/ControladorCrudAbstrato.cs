using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SalesWebMvc.Models.Enums;
using System;

namespace SalesWebMvc.Controllers
{
    public abstract class ControladorCrudAbstrato : Controller
    {
        protected static string _actionControler;
        public static ViewDataDictionary ObtenhaViewDatas(ViewDataDictionary ViewData,                                                    
                                                        EnumStatusAcoes EnumAcao, int? id, 
                                                        string nameAction, Boolean modelRegistrado, Boolean possuiID)
        {
            ViewData["ehModelInvalid"] = false;

            if (!possuiID && EnumAcao.Equals(EnumStatusAcoes.CREATE))
            {
                ViewData["Title"] = "CREATE NEW";
                ViewData["acao"] = EnumAcao;
                ViewData["acaoForm"] = nameAction;
            }
            else if (possuiID)
            {
                if (modelRegistrado)
                {
                    if (EnumAcao.Equals(EnumStatusAcoes.EDIT))
                    {
                        ViewData["Title"] = "EDIT";
                        ViewData["acao"] = EnumAcao;
                        ViewData["acaoForm"] = nameAction;
                    }
                    else if (EnumAcao.Equals(EnumStatusAcoes.DELETE))
                    {
                        ViewData["Title"] = "DELETE";
                        ViewData["acao"] = EnumAcao;
                        ViewData["acaoForm"] = nameAction;
                    }
                    else if (EnumAcao.Equals(EnumStatusAcoes.DETAILS))
                    {
                        ViewData["Title"] = "DETAILS";
                        ViewData["acao"] = EnumAcao;
                        ViewData["acaoForm"] = "#";
                    }
                    else { ViewData.Clear(); }
                }
            }
            else { ViewData.Clear(); }

            return ViewData;
        }
    }
}
