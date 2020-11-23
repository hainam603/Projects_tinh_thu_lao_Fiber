using ClassModel.Connection.Reponsitory;
using ClassModel.Model.Fiber;

namespace FiberSevices.Server.Fiber
{
    public interface IPTMFiber:IReponsitory<PTMFiber>
    {
        dynamic execurePTMFiber(PTMFiber ptmfiber);
        dynamic execuretotalrecordedPTMFiber(PTMFiber ptmfiber);
    }
}
