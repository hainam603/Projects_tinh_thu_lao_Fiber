using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassModel.Model.Fiber;
using ClassModel.Connection.Reponsitory;
using ClassModel.Model.Respond;

namespace FiberSevices.Server.Fiber
{
    public interface IFiberThulao : IReponsitory<FiberThulao>
    {
        public dynamic LayDS_Thulao_Fiber_Theo_Thang_D1(monthFromTo month);
        public dynamic LayDS_Thulao_Fiber_Theo_Thang_D2(monthFromTo month);
    }
}
