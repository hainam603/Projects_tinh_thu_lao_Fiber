using System.ComponentModel.DataAnnotations;

namespace ClassModel.Model.Fiber
{
    public class PTMFiber
    {
        [Key]
        public string MA_TB { get; set; }
        public int TB_CD_DONGQUANG { get; set; }
        public int HUY_TRUOC_30NGAY { get; set; }
        public int KHONGTHUOC_KHUVUC_PTTB { get; set; }
        public string THANG_HOAMANG { get; set; }
        public string NGAY_HOAMANG { get; set; }
        public string NGAY_INS { get; set; }
        public string TRANGTHAI { get; set; }
        public int GOI_TRATRUOC { get; set; }
        public int TOCDO_ID { get; set; }
        public string MA_NVKT_TT { get; set; }
        public int HDTB_ID { get; set; }
        public int HDKH_ID { get; set; }
        public int THUEBAO_ID { get; set; }
        public int KIEULD_ID { get; set; }
        public string MA_GD { get; set; }
        public string PHONGBH { get; set; }
        public int PHONGBH_ID { get; set; }
        public string TEN_TB { get; set; }
        public string DIACHI_LD { get; set; }
        public string NGAY_TT { get; set; }
        public int TB_DUNGTHU { get; set; }
        public int START { get; set; }
        public int LIMIT { get; set; }

    }
}
