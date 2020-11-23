using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassModel.Model.Fiber
{
    public class FiberPTM
    {
        [Key]
        public int hdtb_id { get; set; }
        public int thuebao_id { get; set; }
        public string ma_tb { get; set; }
        public string ten_tb { get; set; }
        public string diachi_tb { get; set; }
        public int hdkh_id { get; set; }
        public string ma_kh { get; set; }
        public string ten_kh { get; set; }
        public string ma_nv { get; set; }
        public string ten_nv { get; set; }
        public string ten_dv { get; set; }
        public int kieuld_id { get; set; }
        public string thang_tt { get; set; }
        public string ma_kieuld { get; set; }
        public string ten_kieuld { get; set; }
        public int loaitb_id { get; set; }
        public string loaihinh_tb { get; set; }
        public int tthd_id { get; set; }
        public string trangthai_hd { get; set; }
        public int trangthaitb_id { get; set; }
        public string trangthai_tb { get; set; }
        public string ngay_tt { get; set; }





    }
}
