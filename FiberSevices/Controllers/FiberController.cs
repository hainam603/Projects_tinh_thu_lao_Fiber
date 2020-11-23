using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassModel.Connection.Sql;
using ClassModel.Model.Fiber;
using ClassModel.Model.Respond;
using FiberSevices.Server.Fiber;
using FiberSevices.Server.Fiber.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiberSevices.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class FiberController : Controller
    {
        private IPTMFiber m_PTMFiber;
        private IThuLaoFiber m_ThuLaoFiber;
        private IFiberPTM _FiberPTM;
        private IFiberThulao _FiberThulao;
        public FiberController(IPTMFiber ptmfiber, IThuLaoFiber thulaofiber, IFiberPTM fiberptm, IFiberThulao fiberthulao )
        {
            m_PTMFiber = ptmfiber;
            m_ThuLaoFiber = thulaofiber;
            _FiberPTM = fiberptm;
            _FiberThulao = fiberthulao;
        }
        [HttpPost("LayDS_PTM_Fiber_Theothang")]
        public dynamic LayDS_PTM_Fiber_Theothang([FromBody] FiberPTM fiber)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = _FiberPTM.LayDS_PTM_Fiber_Theothang(fiber);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [HttpPost("LayDS_Thulao_Fiber_Theo_Thang_D1")]
        public dynamic LayDS_Thulao_Fiber_Theo_Thang([FromBody] monthFromTo month)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = _FiberThulao.LayDS_Thulao_Fiber_Theo_Thang_D1(month);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [HttpPost("LayDS_Thulao_Fiber_Theo_Thang_D2")]
        public dynamic LayDS_Thulao_Fiber_Theo_Thang_D2([FromBody] monthFromTo month)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = _FiberThulao.LayDS_Thulao_Fiber_Theo_Thang_D2(month);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [HttpPost("getPTMFiber")]
        public dynamic getPTMFiber([FromBody] PTMFiber ptmfiber)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_PTMFiber.execurePTMFiber(ptmfiber);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [HttpPost("gettotalrecordedPTMFiber")]
        //[EnableCors("AllowOrigin")]
        public dynamic gettotalrecordedPTMFiber([FromBody] PTMFiber ptmfiber)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_PTMFiber.execuretotalrecordedPTMFiber(ptmfiber);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [HttpPost("getThuLaoFiber")]
        public dynamic getThuLaoFiber([FromBody] ThuLaoFiber thulaofiber)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_ThuLaoFiber.execureThuLaoFiber(thulaofiber);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [HttpPost("gettotalrecordedThuLaoFiber")]
        //[EnableCors("AllowOrigin")]
        public dynamic gettotalrecordedThuLaoFiber([FromBody] ThuLaoFiber thulaofiber)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_ThuLaoFiber.execuretotalrecordedThuLaoFiber(thulaofiber);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        // GET: api/<FiberController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FiberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FiberController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FiberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FiberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
