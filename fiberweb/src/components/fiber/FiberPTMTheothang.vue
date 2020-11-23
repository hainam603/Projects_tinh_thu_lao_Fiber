<template>
  <el-container>
    <el-header>
      <el-row>
        <el-col :span="12"><div class="title">DANH SÁCH FIBER PHÁT TRIỂN MỚI </div></el-col>
        <el-col :span="12">
          Chọn tháng để xem: 
          <el-date-picker
            @change="datepickerClick()"
            v-model="thang_tt"
            type="month"
            placeholder="Chọn ngày"
            format="MM-yyyy"
            value-format="yyyyMM">
          </el-date-picker>
          <div class="export-excel">
            <download-excel 
            name="DANH SÁCH FIBER PHÁT TRIỂN MỚI" 
            :fields="columns"
            :data="tableData">
                <el-button icon="el-icon-download">Excel</el-button>
            </download-excel>
            <!-- <vue-excel-xlsx
                :data="tableData"
                :columns="columns"
                :filename="'filename'"
                :sheetname="'sheetname'"
                >
                Download
            </vue-excel-xlsx> -->
           <!-- <el-button icon="el-icon-download" @click="ExportToExcel()">Excel</el-button> -->
          </div>
          
        </el-col>
      </el-row>
    </el-header>
    <el-main>
        <el-table
          v-loading="loading"
          border
          height="450"
          :data="posts"
          style="width: 100%">
          <el-table-column fixed type="index" width="50"></el-table-column>
          <el-table-column prop="hdtb_id" label="ID Hợp đồng thuê bao" width="180"></el-table-column>
          <el-table-column prop="thuebao_id" label="ID Thuê bao " width="180"></el-table-column>
          <el-table-column prop="ma_tb" label="Mã Thuê bao" width="180"></el-table-column>
          <el-table-column prop="ten_tb" label="Tên thuê bao" width="180"></el-table-column>
          <el-table-column prop="diachi_tb" label="Địa chỉ thuê bao" width="380"></el-table-column>
          <el-table-column prop="hdkh_id" label="ID Hợp đồng khách hàng" width="190"></el-table-column>
          <el-table-column prop="ten_kh" label="Tên khách hàng" width="180"></el-table-column>
          <el-table-column prop="ma_nv" label="Mã nhân viên" width="180"></el-table-column>
          <el-table-column prop="ten_nv" label="Tên nhân viên" width="180"></el-table-column>
          <el-table-column prop="kieuld_id" label="ID kiểu lắp đặt" width="180"></el-table-column>
          <el-table-column prop="ma_kieuld" label="Mã kiểu lắp đặt" width="180"></el-table-column>
          <el-table-column prop="ten_kieuld" label="Tên kiểu lắp đặt" width="180"></el-table-column>
          <el-table-column prop="loaitb_id" label="ID Loại thuê bao" width="180"></el-table-column>
          <el-table-column prop="loaihinh_tb" label="Loại thuê bao" width="180"></el-table-column>
          <el-table-column prop="tthd_id" label="ID trạng thái hợp đồng" width="180"></el-table-column>
          <el-table-column prop="trangthai_hd" label="Trạng thái hợp đồng" width="180"></el-table-column>
          <el-table-column prop="trangthaitb_id" label="ID trạng thái thuê bao" width="180"></el-table-column>
          <el-table-column prop="trangthai_tb" label="Trạng thái thuê bao" width="180"></el-table-column>
          <el-table-column prop="thang_tt" label="Tháng thanh toán" width="180"></el-table-column>
          <el-table-column prop="ngay_tt" label="Ngày thanh toán" width="180"></el-table-column>
        </el-table>
    </el-main>
    <el-footer>
      <el-row>
        <el-col :span="24">
            <el-pagination
              background
              @current-change="displayedPosts"
              @size-change="handleSizeChange"
              layout="total, sizes, prev, pager, next"
              :page-sizes="[50, 100, 150]"
              :page-size="perPage"
              :current-page="page"	
              :total="total">
            </el-pagination>
        </el-col>
      </el-row>
    </el-footer>
  </el-container>
</template>

<script>
import FactoryRepository from '../../service/factory/repositoryfactory';
import {json2excel, excel2json} from 'js2excel';
const FiberRepository = FactoryRepository.get('fiber')
export default {
   data() {
      return {
        loading:false,
        tableData: [],
        // columns:[
        //     {label: "ID Hợp đồng thuê bao",
        //     field: "hdtb_id"},
        //     {label: "ID Thuê bao",
        //     field: "thuebao_id"},
        //     {label: "Mã Thuê bao",
        //     field: "ten_tb"},
        //     {label: "Tên thuê bao",
        //     field: "ten_tb"},
        //     {label: "Địa chỉ thuê bao",
        //     field: "diachi_tb"},
        //     {label: "ID Hợp đồng khách hàng",
        //     field: "hdkh_id"},
        //     {label: "Tên khách hàng",
        //     field: "ten_kh"},
        //     {label: "Mã nhân viên",
        //     field: "ma_nv"},
        //     {label: "Tên nhân viên",
        //     field: "ten_nv"},
        //     {label: "ID kiểu lắp đặt",
        //     field: "kieuld_id"},
        //     {label: "Mã kiểu lắp đặt",
        //     field: "ma_kieuld"},
        //     {label: "Tên kiểu lắp đặt",
        //     field: "ten_kieuld"},
        //     {label: "ID Loại thuê bao",
        //     field: "loaitb_id"},
        //     {label: "Loại thuê bao",
        //     field: "loaihinh_tb"},
        //     {label: "ID trạng thái hợp đồng",
        //     field: "tthd_id"},
        //     {label: "Trạng thái hợp đồng",
        //     field: "trangthai_hd"},
        //     {label: "ID trạng thái thuê bao",
        //     field: "trangthaitb_id"},
        //     {label: "Trạng thái thuê bao",
        //     field: "trangthai_tb"},
        //     {label: "Trạng thái thuê bao",
        //     field: "thang_tt"},
        //     {label: "Ngày thanh toán",
        //     field: "ngay_tt"},
        //   ],
        columns:
            {"ID Hợp đồng thuê bao":"hdtb_id",
            "ID Thuê bao":"thuebao_id",
            "Mã Thuê bao":"ten_tb",
            "Tên thuê bao":"ten_tb",
            "Địa chỉ thuê bao":"diachi_tb",
            "ID Hợp đồng khách hàng":"hdkh_id",
            "Tên khách hàng":"ten_kh",
            "Mã nhân viên":"ma_nv",
            "Tên nhân viên":"ten_nv",
            "ID kiểu lắp đặt":"kieuld_id",
            "Mã kiểu lắp đặt":"ma_kieuld",
            "Tên kiểu lắp đặt":"ten_kieuld",
            "ID Loại thuê bao":"loaitb_id",
            "Loại thuê bao":"loaihinh_tb",
            "ID trạng thái hợp đồng":"tthd_id",
            "Trạng thái hợp đồng":"trangthai_hd",
            "ID trạng thái thuê bao":"trangthaitb_id",
            "Trạng thái thuê bao":"trangthai_tb",
            "Trạng thái thuê bao":"thang_tt",
            "Ngày thanh toán":"ngay_tt"},
        thang_tt:'',
        posts:[],
        page: 1,
        perPage: 50,
        pages: [],
        total:0
      }
    },
    methods:{
      ExportToExcel(){
        var data=this.tableData;
            json2excel({
                data,
                name: 'user-info-data',
                formateDate: 'yyyy/mm/dd'
            });
  
      },
      datepickerClick(){
        if(this.thang_tt)
          this.LayDS_PTM_Fiber_Theothang()
      },
      LayDS_PTM_Fiber_Theothang(){
        var self=this
        self.$message('Đang lấy dữ liệu xin chờ...');
        self.loading=true;
        var data={"thang_tt":self.thang_tt}
        FiberRepository.LayDS_PTM_Fiber_Theothang(data).then(response=>{
          if(response.data.success){
            self.tableData=response.data.data
            self.total=self.tableData.length
            self.posts=self.paginate(self.tableData)
            self.loading=false
            self.$message({
              message: 'Lấy dữ liệu hoàn tất',
              type: 'success'
            });
          }
          else{
            self.loading=false
            self.$message({
              message: 'Lỗi lấy dữ liệu thử lại sau',
              type: 'error'
            });
          }
        })
        .catch(err=>{
          self.loading=false
            self.$message({
              message: err,
              type: 'error'
            });
        })
        
      },
      //phân trang
      displayedPosts(e){
            var self=this;
            self.loading=true;
            self.page=e;
            self.posts=self.paginate(self.tableData);
            self.loading=false;
      },
      paginate (posts) {
            var self=this;
            var page = self.page;
            var perPage = self.perPage;
            var from = (page * perPage) - perPage;
            var to = (page * perPage);
            return  posts.slice(from, to);
      },
      handleSizeChange(e){
        var self=this;
        self.perPage=e;
        self.displayedPosts(1);
      }
    }
}
</script>

<style scoped>
@import url('/FiberThulao.css');
</style>>

</style>