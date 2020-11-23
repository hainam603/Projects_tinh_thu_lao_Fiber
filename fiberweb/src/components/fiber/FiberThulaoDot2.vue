<template>
  <el-container>
    <el-header>
      <el-row>
        <el-col :span="12"><div class="title">TÍNH THÙ LAO FIBER ĐỢT 2 </div></el-col>
        <el-col :span="12">
          Chọn tháng để xem: 
          <!-- <el-date-picker
            @change="datepickerClick()"
            v-model="thang_tt"
            type="month"
            placeholder="Chọn ngày"
            format="MM-yyyy"
            value-format="yyyyMM">
          </el-date-picker> -->
          <el-date-picker
            @change="datepickerClick()"
            v-model="thang_tt"
            type="monthrange"
            format="MM-yyyy"
            start-placeholder="Từ tháng"
            end-placeholder="Đến tháng"
            value-format="yyyyMM">
          </el-date-picker>
           <div class="export-excel">
            <download-excel 
            name="THÙ LAO FIBER ĐỢT 2" 
            :fields="columns"
            :data="tableData">
              <el-button icon="el-icon-download">Excel</el-button>
          </download-excel>
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
          <el-table-column prop="ten_kieuld" label="Tên kiểu lắp đặt" width="180"></el-table-column>
          <el-table-column prop="loaitb_id" label="ID Loại thuê bao" width="180"></el-table-column>
          <el-table-column prop="loaihinh_tb" label="Loại thuê bao" width="180"></el-table-column>
          <el-table-column prop="trangthai_hd" label="Trạng thái hợp đồng" width="180"></el-table-column>
          <el-table-column prop="trangthai_tb" label="Trạng thái thuê bao" width="180"></el-table-column>
          <el-table-column prop="thang_tt" label="Tháng thanh toán" width="180"></el-table-column>
          <el-table-column prop="ngay_tt" label="Ngày thanh toán" width="180"></el-table-column>
          <el-table-column prop="thang_km" label="Số tháng trả trước" width="180"></el-table-column>
          <el-table-column prop="tilethulaotrendoanhthugoi" label="Tỉ lệ thù lao/doanh thu gói" width="200"></el-table-column>
          <el-table-column prop="tilethulao" label="Tỉ lệ thù lao đợt 2" width="180"></el-table-column>
          <el-table-column prop="thangchitra" label="Tháng chi trả đợt 2" width="180"></el-table-column>
          <el-table-column prop="ngaytinhchitra" label="Ngày tính chi trả đợt 2" width="180"></el-table-column>
          <el-table-column prop="ghichu" label="Ghi chú" width="180"></el-table-column>
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
import FactoryRepository from '../../service/factory/repositoryfactory'
const FiberRepository = FactoryRepository.get('fiber')
export default {
   data() {
      return {
        loading:false,
        tableData: [],
        columns:{
          "ID Hợp đồng thuê bao":"hdtb_id",
          "ID Thuê bao":"thuebao_id",
          "Mã Thuê bao":"ma_tb",
          "Tên thuê bao":"ten_tb",
          "Địa chỉ thuê bao":"diachi_tb",
          "ID Hợp đồng khách hàng":"hdkh_id",
          "Tên khách hàng":"ten_kh",
          "Mã nhân viên":"ma_nv",
          "Tên nhân viên":"ten_nv",
          "ID kiểu lắp đặt":"kieuld_id",
          "Tên kiểu lắp đặt":"ten_kieuld",
          "ID Loại thuê bao":"loaitb_id",
          "Loại thuê bao":"loaihinh_tb",
          "Trạng thái hợp đồng":"trangthai_hd",
          "Trạng thái thuê bao":"trangthai_tb",
          "Tháng thanh toán":"thang_tt",
          "Ngày thanh toán":"ngay_tt",
          "Số tháng trả trước":"thang_km",
          "Tỉ lệ thù lao/doanh thu gói":"tilethulaotrendoanhthugoi",
          "Tỉ lệ thù lao đợt 2":"tilethulao",
          "Tháng chi trả đợt 2":"thangchitra",
          "Ngày tính chi trả đợt 2":"ngaytinhchitra",
          "Ghi chú":"ghichu"
        },
        thang_tt:'',
        posts:[],
        page: 1,
        perPage: 50,
        pages: [],
        total:0
      }
    },
    methods:{
      datepickerClick(){
        if(this.thang_tt)
          this.LayDS_Thulao_Fiber_Theo_Thang_D2()
      },
      LayDS_Thulao_Fiber_Theo_Thang_D2(){
        var self=this
        self.$message('Đang lấy dữ liệu xin chờ...');
        self.loading=true;
        var data={"monthFrom":self.thang_tt[0],
                  "monthTo":self.thang_tt[1],}
        FiberRepository.LayDS_Thulao_Fiber_Theo_Thang_D2(data).then(function(response){
          if(response.data.success){
            self.tableData=response.data.data
            self.total=self.tableData.length
            self.posts=self.paginate(self.tableData)
            self.loading=false
            self.$message({
              message: 'Lấy dữ liệu hoàn tất',
              type: 'success'
            });
          }else{
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