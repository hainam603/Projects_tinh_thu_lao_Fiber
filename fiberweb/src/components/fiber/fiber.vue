<template>
  <div class="main">
    <!-- PTM Fiber -->
    <div class="fiber">
      <div class="title flex">
        <span>Phát triển mới fiber
          {{ monthPTM.substring(4, 6) + "/" + monthPTM.substring(0, 4) }}</span>

        <div class="block">
          <el-pagination
            @current-change="handleCurrentChange($event, 1)"
            @size-change="handleSizeChange($event,1)"
            :page-sizes=[30,50,100]
            :page-size="30"
            background
            layout="sizes, prev, pager, next"
            :total="totalpagePTM"
          >
          </el-pagination>
        </div>

        <div class="block">
          Chọn tháng để xem:
          <el-date-picker
            @change="clickmonth($event,1)"
            v-model="monthPTM"
            type="month"
            placeholder="Chọn tháng"
            format="MM-yyyy"
            value-format="yyyyMM"
          >
          </el-date-picker>
        </div>
      </div>
      <div class="content">
        <!-- <table class="c-table">
        <thead>
          <tr>
            <th>STT</th>
            <th>Mã thuê bao</th>
            <th>TB CD DONG QUANG</th>
            <th>Hủy trước 30 ngày</th>
            <th>Không thuộc KVPTTB</th>
            <th>Tháng hòa mạng</th>
            <th>Ngày hòa mạng</th>
            <th>Ngày thêm vào</th>
            <th>Trạng thái</th>
            <th>Gói trả trước</th>
            <th>Tốc độ ID</th>
            <th>Mã NVKT TT</th>
            <th>HĐTB ID</th>
            <th>HĐKH ID</th>
            <th>Thuê bao ID</th>
            <th>Kiểu dữ liệu ID</th>
            <th>Mã GD</th>
            <th>Phòng bán hàng</th>
            <th>Phòng bán hàng ID</th>
            <th>Tên TB</th>
            <th>Địa chỉ LĐ</th>
            <th>Ngày TT</th>
            <th>TB DT</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, key) in itemsPTM" v-bind:key="key">
            <td>{{ (key += 1) }}</td>
            <td>{{ item.mA_TB }}</td>
            <td>{{ item.tB_CD_DONGQUANG }}</td>
            <td>{{ item.huY_TRUOC_30NGAY }}</td>
            <td>{{ item.khongthuoC_KHUVUC_PTTB }}</td>
            <td>{{ item.thanG_HOAMANG }}</td>
            <td>{{ item.ngaY_HOAMANG }}</td>
            <td>{{ item.ngaY_INS }}</td>
            <td>{{ item.trangthai }}</td>
            <td>{{ item.goI_TRATRUOC }}</td>
            <td>{{ item.tocdO_ID }}</td>
            <td>{{ item.mA_NVKT_TT }}</td>
            <td>{{ item.hdtB_ID }}</td>
            <td>{{ item.hdkH_ID }}</td>
            <td>{{ item.thuebaO_ID }}</td>
            <td>{{ item.kieulD_ID }}</td>
            <td>{{ item.mA_GD }}</td>
            <td>{{ item.phongbh }}</td>
            <td>{{ item.phongbH_ID }}</td>
            <td>{{ item.teN_TB }}</td>
            <td>{{ item.diachI_LD }}</td>
            <td>{{ item.ngaY_TT }}</td>
            <td>{{ item.tB_DUNGTHU }}</td>
          </tr>
          <tr></tr>
        </tbody>
      </table> -->

        <el-table
          v-loading="loadingptm"
          :data="itemsPTM"
          style="width: 100%"
          height="500"
          border
        >
          <el-table-column fixed type="index">
          </el-table-column>
          <el-table-column
            fixed
            sortable
            prop="mA_TB"
            label="Mã TB"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="tB_CD_DONGQUANG"
            label="DONG QUANG"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="huY_TRUOC_30NGAY"
            label="Hủy trước 30N"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="khongthuoC_KHUVUC_PTTB"
            label="Không thuộc PTTB"
            width="180"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="thanG_HOAMANG"
            label="Tháng hòa mạng"
            width="160"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ngaY_HOAMANG"
            label="Ngày hòa mạng"
            width="200"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ngaY_INS"
            label="Ngày thêm"
            width="200"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="trangthai"
            label="Trạng thái"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="goI_TRATRUOC"
            label="Gói trả trước"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="tocdO_ID"
            label="Tốc độ ID"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="mA_NVKT_TT"
            label="Mã NVKT TT"
            width="150"
          >
          </el-table-column>
          <el-table-column sortable prop="hdtB_ID" label="HĐTB" width="120">
          </el-table-column>
          <el-table-column sortable prop="hdkH_ID" label="HĐKH" width="120">
          </el-table-column>
          <el-table-column
            sortable
            prop="thuebaO_ID"
            label="Thuê bao"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="kieulD_ID"
            label="Kiểu LĐ"
            width="120"
          >
          </el-table-column>
          <el-table-column sortable prop="mA_GD" label="Mã GD" width="200">
          </el-table-column>
          <el-table-column sortable prop="phongbh" label="Phòng BH" width="350">
          </el-table-column>
          <el-table-column
            sortable
            prop="phongbH_ID"
            label="Phong BH ID"
            width="150"
          >
          </el-table-column>
          <el-table-column sortable prop="teN_TB" label="Tên TB" width="350">
          </el-table-column>
          <el-table-column
            sortable
            prop="diachI_LD"
            label="Địa chỉ LĐ"
            width="500"
          >
          </el-table-column>
          <el-table-column sortable prop="ngaY_TT" label="Ngày TT" width="200">
          </el-table-column>
          <el-table-column
            sortable
            prop="tB_DUNGTHU"
            label="TB Dùng thử"
            width="150"
          >
          </el-table-column>
        </el-table>
      </div>
    </div>
    <!-- Thù Lao Fiber -->
    <div class="fiber">
      <div class="title flex">
        <span
          >Thù Lao fiber
          {{ monthThuLao.substring(4, 6) + "/" + monthThuLao.substring(0, 4) }}</span
        >

        <div class="block">
          <el-pagination
            @current-change="handleCurrentChange($event,2)"
            @size-change="handleSizeChange($event,2)"
            :page-sizes=[30,50,100]
            :page-size="30"
            background
            layout="sizes, prev, pager, next"
            :total="totalpageThuLao"
          >
          </el-pagination>
        </div>
        <div class="block">
          <!-- <div class="demonstration">{{monthPTM}}</div> -->
          Chọn tháng để xem:
          <el-date-picker
            @change="clickmonth($event, 2)"
            v-model="monthThuLao"
            type="month"
            placeholder="Chọn tháng"
            format="MM-yyyy"
            value-format="yyyyMM"
          >
          </el-date-picker>
        </div>
      </div>
      <div class="content">
        <el-table
          v-loading="loadingthulao"
          :data="itemsThulao"
          style="width: 100%"
          height="500"
          border
        >
          <el-table-column fixed type="index">
          </el-table-column>
          <el-table-column
            fixed
            sortable
            prop="mA_TB"
            label="Mã TB"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="tB_CD_DONGQUANG"
            label="DONG QUANG"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="huY_TRUOC_30NGAY"
            label="Hủy trước 30N"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="khongthuoC_KHUVUC_PTTB"
            label="Không thuộc PTTB"
            width="180"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="thanG_HOAMANG"
            label="Tháng hòa mạng"
            width="160"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ngaY_HOAMANG"
            label="Ngày hòa mạng"
            width="200"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="trangthai"
            label="Trạng thái"
            width="180"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="goI_TRATRUOC"
            label="Gói trả trước"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="tocdO_ID"
            label="Tốc độ ID"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="mA_NVKT_TT"
            label="Mã NVKT TT"
            width="150"
          >
          </el-table-column>
          <el-table-column sortable prop="hdtB_ID" label="HĐTB" width="120">
          </el-table-column>
          <el-table-column
            sortable
            prop="thuebaO_ID"
            label="Thuê bao"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="kieulD_ID"
            label="Kiểu LĐ"
            width="120"
          >
          </el-table-column>
          <el-table-column sortable prop="mA_GD" label="Mã GD" width="200">
          </el-table-column>
          <el-table-column sortable prop="teN_TB" label="Tên TB" width="200">
          </el-table-column>
          
          <el-table-column
            sortable
            prop="diachI_LD"
            label="Địa chỉ LĐ"
            width="500"
          >
          </el-table-column>
          <el-table-column sortable prop="ngaY_TT" label="Ngày TT" width="200">
          </el-table-column>
          <el-table-column
            sortable
            prop="tB_DUNGTHU"
            label="TB Dùng thử"
            width="140"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="phongbh"
            label="Phòng BH"
            width="180"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="phongbH_ID"
            label="Phòng BH ID"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ghichu"
            label="Ghi chú"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="doanhthugoi"
            label="Doanh thu gói"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="thulaO_DOT1"
            label="Thù lao đợt 1"
            width="150"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="thuhoi"
            label="Thu hồi"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ngaY_TINH_THUHOI"
            label="Ngày tính thu hồi"
            width="160"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="trangthaitB_ID"
            label="Trạng thái TB ID"
            width="160"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="lydohuy"
            label="Lý do hủy"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ngaY_HUY"
            label="Ngày hủy"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="thanG_HUY"
            label="Tháng hủy"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="sothang"
            label="Số tháng"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="ketthuc"
            label="Kết thúc"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="sothanG_HT"
            label="Số tháng HT"
            width="180"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="hO_NVKT_TT"
            label="Họ NVKTT"
            width="140"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="teN_NVKT_TT"
            label="Tên NVKTT"
            width="140"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="dV_NVKT_TT"
            label="Đơn vị NVKTT"
            width="140"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="pB_NVKT_TT"
            label="PB NVKTT"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="tO_NVKT_TT"
            label="To NVKTT"
            width="120"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="dongiA_THEO_TOCDO_TT"
            label="Đơn giá theo tốc độ TT"
            width="200"
          >
          </el-table-column>
          <el-table-column
            sortable
            prop="thulaO_D1_DONGIA_THEO_TOCDO_TT"
            label="Thù lao D1 đơn giá theo tốc độ TT"
            width="280"
          >
          </el-table-column>
           <el-table-column
            sortable
            prop="toantrinh"
            label="Toan Trinh"
            width="120"
          >
          </el-table-column>
           <el-table-column
            sortable
            prop="ngaY_INS"
            label="Ngày thêm"
            width="120"
          >
          </el-table-column>
           <el-table-column
            sortable
            prop="thanG_CHI_TRA"
            label="Tháng chi trả"
            width="150"
          >
          </el-table-column>
           <el-table-column
            sortable
            prop="ngaY_TINH_THULAO_DOT1"
            label="Ngày tính thù lao đợt 1"
            width="200"
          >
         
           </el-table-column> 
        </el-table>
        
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios';

import factory from "../../service/factory/repositoryfactory";
const fiber = factory.get("fiber");

export default {
  name: "fiber",
  data() {
    return {
      loadingptm:true,
      monthPTM: "202007",
      totalpagePTM:30,
      dataPTMFiber: {
        thang_hoamang: "202007",
        start: "0",
        limit: 30,
      },
      itemsPTM: [],
      loadingthulao:true,
      monthThuLao: "201901",
      totalpageThuLao:30,
      dataThuLaoFiber: {
        thang_hoamang: "201901",
        start: "0",
        limit: 30,
      },
      itemsThulao: [],
      errors: [],
    };
  },
  created() {
    this.getPTMFiber();
    this.gettotalrecordedPTMFiber();
    this.getThuLaoFiber();
    this.gettotalrecordedThuLaoFiber();
    
  },
  methods: {
    handleSizeChange(e,t){
      var self = this;
      if(t==1)
      {
        self.$set(self.dataPTMFiber, "limit", e);
        self.getPTMFiber();
      }else{
        self.$set(self.dataThuLaoFiber, "limit", e);
        self.getThuLaoFiber();

      }


    },
    handleCurrentChange(e, t) {
      var self = this;
      if(t==1){
        var s = (e - 1) * self.dataPTMFiber.limit;
        self.$set(self.dataPTMFiber, "start", s);
        self.getPTMFiber();
      }
      else{
        var s = (e - 1) * self.dataThuLaoFiber.limit;
        self.$set(self.dataThuLaoFiber, "start", s);
        self.getThuLaoFiber();
      }
    },
    clickmonth(e,t) {
      var self = this;
      if(t==1){
        self.$set(self.dataPTMFiber, "thang_hoamang", self.monthPTM);
        self.getPTMFiber();
        self.gettotalrecordedPTMFiber();
      }
      else{
        self.$set(self.dataThuLaoFiber, "thang_hoamang", self.monthThuLao);
        self.getThuLaoFiber();
        self.gettotalrecordedThuLaoFiber();
      }
    },
    getPTMFiber() {
      var self = this;
      self.loadingptm=true;
      fiber.getPTMFiber(self.dataPTMFiber).then(function (response) {
        if (response.data.success) {
          self.itemsPTM=[];
          self.itemsPTM = response.data.data;
          
          // response.data.data.forEach((e, i) => {
          //   self.itemsPTM.push({
          //     mA_TB:e.mA_TB,
          //     tB_CD_DONGQUANG:e.tB_CD_DONGQUANG,
          //     huY_TRUOC_30NGAY:e.huY_TRUOC_30NGAY,
          //     khongthuoC_KHUVUC_PTTB:e.khongthuoC_KHUVUC_PTTB,
          //     thanG_HOAMANG:e.thanG_HOAMANG,
          //     ngaY_HOAMANG:e.ngaY_HOAMANG,
          //     ngaY_INS:e.ngaY_INS,
          //     trangthai:e.trangthai,
          //     goI_TRATRUOC:e.goI_TRATRUOC,
          //     tocdO_ID:e.tocdO_ID,
          //     mA_NVKT_TT:e.mA_NVKT_TT,
          //     hdtB_ID:e.hdtB_ID,
          //     hdkH_ID:e.hdkH_ID,
          //     thuebaO_ID:e.thuebaO_ID,
          //     kieulD_ID:e.kieulD_ID,
          //     mA_GD:e.mA_GD,
          //     phongbh:e.phongbh,
          //     phongbH_ID:e.phongbH_ID,
          //     teN_TB:e.teN_TB,
          //     diachI_LD:e.diachI_LD,
          //     ngaY_TT:e.ngaY_TT,
          //     tB_DUNGTHU:e.tB_DUNGTHU
          //   });

          // });
          // console.log(self.itemsPTM);
          self.loadingptm=false;
        } else {
          console.log(response.data.errors);
        }
      });
    },
    gettotalrecordedPTMFiber() {
      var self = this;
      fiber
        .gettotalrecordedPTMFiber(self.dataPTMFiber)
        .then(function (response) {
          if (response.data.success) {
            response.data.data.forEach((element, index) => {
              self.totalpagePTM = parseInt(element.total);
              
            });
          } else {
            console.log(response.data.errors);
          }
        });
    },
    getThuLaoFiber() {
      var self = this;
      self.loadingthulao=true;
      fiber.getThuLaoFiber(self.dataThuLaoFiber).then(function (response) {
        if (response.data.success) {
          self.itemsThulao = [];
          self.itemsThulao = response.data.data;
          self.loadingthulao=false;
        } else {
          console.log(response.data.errors);
        }
      });
    },
    gettotalrecordedThuLaoFiber() {
      var self = this;
      fiber
        .gettotalrecordedThuLaoFiber(self.dataThuLaoFiber)
        .then(function (response) {
          if (response.data.success) {
            response.data.data.forEach((element, index) => {
              self.totalpageThuLao = parseInt(element.total);
            });
          } else {
            console.log(response.data.errors);
          }
        });
    }
  },
  computed: {},
};
</script>

<style scoped>
@import url(./fiber.css);
</style>