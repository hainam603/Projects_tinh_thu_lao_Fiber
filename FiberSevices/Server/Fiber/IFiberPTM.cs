using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassModel.Model.Fiber;
using ClassModel.Connection.Reponsitory;

using Microsoft.EntityFrameworkCore.Migrations;

namespace FiberSevices.Server.Fiber
{
    public interface IFiberPTM:IReponsitory<FiberPTM>
    {
        public dynamic LayDS_PTM_Fiber_Theothang(FiberPTM fiber);
        
    }
}
