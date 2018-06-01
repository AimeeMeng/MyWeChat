using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeChat.Menus
{
    public class MenuFactory
    {
        public static IMenu ClickMenuResponse(Models.Msg.EventMessage em)
        {
            //switch (em.EventKey)
            //{
            //    case "TotalAnalysis":
            //        {
            //            return new EnergyMenu.TotalAnalysis(em);
            //        }
            //    case "CTable":
            //        {
            //            return new EnergyMenu.CTable(em);
            //        }
            //    case "OverView":
            //        {
            //            return new EnergyMenu.OverView(em);
            //        }
            //    case "UntreatedOrder":
            //        {
            //            return new EnergyMenu.UntreatedOrder(em);
            //        }
            //    case "TreatingOrder":
            //        {
            //            return new EnergyMenu.TreatingOrder(em);
            //        }
            //    case "CompletedOrder":
            //        {
            //            return new EnergyMenu.CompletedOrder(em);
            //        }
            //    case "UnScoreOrder":
            //        {
            //            return new EnergyMenu.UnScoreOrder(em);
            //        }
            //    case "ScoredOrder":
            //        {
            //            return new EnergyMenu.ScoredOrder(em);
            //        }
            //    case "DistributionOrder":
            //        {
            //            return new EnergyMenu.DistributionOrder(em);
            //        }

            //}
            return null;
        }

    }
}