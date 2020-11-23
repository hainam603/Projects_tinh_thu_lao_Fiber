using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassModel.Connection.Reponsitory;
using ClassModel.Connection.Reponsitory.Impl;
using ClassModel.Model.Fiber;

namespace FiberSevices.Server.Fiber
{
    public interface IThuLaoFiber : IReponsitory<ThuLaoFiber>
    {
        dynamic execureThuLaoFiber(ThuLaoFiber thulaofiber);
        dynamic execuretotalrecordedThuLaoFiber(ThuLaoFiber thulaofiber);
    }
}
