<template>
  <div>
    <div class="profile">
      <h1>{{title}}</h1>
      <div class="textfield">
        <span>Người dùng</span>
        <input
          type="text"
          v-model="userupdate.userName"
          placeholder="Username"
          value=""
          :readonly="readonly"
        />
        <div class="validate" v-if="validateUsername">*</div>
      </div>
      <div class="textfield">
        <span>Mật khẩu</span>
        <input
          type="password"
          placeholder="Password"
          v-model="password"
          value=""
        />
        <div class="validate" v-if="validatePassword">*</div>
      </div>
      <div class="textfield">
        <span>Quyền hạn</span>
        <ejs-dropdownlist
          id="dropdownlistRole"
          :dataSource="roles"
          :index="userupdate.role - 1"
          @select="onSelect($event, 1)"
          placeholder="Chọn quyền"
          popupWidth="185px"
          popupHeight="200px"
          width="185px"
        ></ejs-dropdownlist>
        <div class="validate" v-if="validateRole">*</div>
      </div>
      <div class="textfield">
        <span>Hoạt động</span>
        <div class="radio">
          <label for="active0">
            <input
              type="radio"
              :checked="userupdate.active ? 'checked' : ''"
              name="active"
              @click="clickRadioButton(1)"
              id="active0"
            />
            Có
          </label>
          <label for="active1">
            <input
              type="radio"
              :checked="!userupdate.active ? 'checked' : ''"
              name="active"
              @click="clickRadioButton(0)"
              id="active1"
            />
            Không
          </label>
          <div class="validate" v-if="validateActive">*</div>
        </div>
      </div>
      <div class="textfield">
        <span>Trạng thái</span>
        <ejs-dropdownlist
          id="dropdownlistStatus"
          :dataSource="Statuses"
          :index="userupdate.status - 1"
          @select="onSelect($event, 2)"
          placeholder="Chọn trạng thái"
          popupWidth="185px"
          popupHeight="200px"
          width="185px"
        ></ejs-dropdownlist>
        <div class="validate" v-if="validateStatus">*</div>
      </div>
      <div class="textfield">
        <button @click="handleSubmit">{{titleButton}}</button>
        <button @click="handleCancle">Hủy</button>
      </div>
      <div class="textfield">
        {{msg}}
      </div>
    </div>
  </div>
</template>

<script>
import {
  DropDownListComponent,
  DropDownListPlugin,
} from "@syncfusion/ej2-vue-dropdowns";
import factory from "../../service/factory/repositoryfactory";
const user = factory.get("user");
export default {
    props:{
        profile:Number
    },
  data() {
    return {
      validateUsername:false,
      validatePassword:false,
      validateActive:false,
      validateRole:false,
      validateStatus:false,
      readonly:false,
      title:"Tạo người dùng mới",
      titleButton:"Tạo mới",
      msg:"",
      password: "",
      userupdate: {},
      roles: [
        { text: "Quản trị", value: 1 },
        { text: "Người dùng 1", value: 2 },
        { text: "Người dùng 2", value: 3 },
      ],
      Statuses: [
        { text: "Kích hoạt", value: 1 },
        { text: "Khóa", value: 2 },
        { text: "Tạm khóa", value: 3 },
      ],
    };
  },
  created() {
      if(this.profile==2)
      {
        this.title="Cập nhật thông tin người dùng";
        this.titleButton="Cập nhật";
        this.readonly=true;
        this.getUserByToken();
      }
  },
  methods: {
    validate(){

      if(self.userupdate.userName=='')
        self.validateUsername=true;
      if(self.userupdate.password=='')
        self.validateUsername=true;
      if(self.userupdate.roles == '')
        self.validateRole=true;
      if(self.userupdate.active == '')
        self.validateActive=true;
      if(self.userupdate.status == '')
        self.validateStatus=true;
      
      if(self.profile==1)
        return(self.validateUsername,self.validatePassword,self.userupdate.roles,self.userupdate.active,self.validateStatus);
      else
        return(self.validateUsername,self.userupdate.roles,self.userupdate.active,self.validateStatus);
    },
    handleSubmit() {
      if (this.password != "")
        this.$set(this.userupdate, "passWord", this.password);
        //if(this.validate())
        {
          if(this.profile==2)
            this.updateUser();
          else{
            this.createUser();
          }
        }
      
    },
    createUser(){
      var self=this;
      self.msg="Đang tạo người dùng, xin chờ...";
      user.create(self.userupdate).then(function (response) {
          if (response.data.success) {
              debugger;
            self.msg="Tạo người dùng thành công";
          } else {
            self.msg="Đã xảy ra lỗi, thử lại sau";
          }
      });
    },
    updateUser(){
      var self=this;
      self.msg="Đang cập nhật thông tin, xin chờ...";
      user.update(self.userupdate).then(function (response) {
          if (response.data.success) {
              debugger;
            self.msg="Cập nhật thông tin thành công";
          } else {
            self.msg="Đã xảy ra lỗi, thử lại sau";
          }
      });

    },
    handleCancle() {
      this.$emit("changestatusloginEvent");
    },
    onSelect(e, t) {
      if (t == 1) this.$set(this.userupdate, "role", e.itemData.value);
      else this.$set(this.userupdate, "status", e.itemData.value);
    },
    clickRadioButton(e) {
      this.$set(this.userupdate, "active", e);
    },
    getUserByToken() {
        var self=this;
      var section_local = self.getCookie("token_session");
      if (section_local) {
        var tok = self.strToObj(section_local);
        user.getById(tok.usid).then(function (response) {
        if (response.data.success) {
          self.$set(self.userupdate, "idUser", response.data.data.idUser);
          self.$set(self.userupdate, "userName", response.data.data.userName);
          self.$set(self.userupdate, "role", response.data.data.role);
          self.$set(self.userupdate, "active", response.data.data.active);
          self.$set(self.userupdate, "status", response.data.data.status);
        }
      });
      } else {
          this.$emit("changestatusloginEvent");
      }
    },
    getCookie(cname) {
      var name = cname + "=";
      var decodedCookie = decodeURIComponent(document.cookie);
      for (var i = 0; i < decodedCookie.length; i++) {
        var c = decodedCookie;
        while (c.charAt(0) === " ") {
          c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
          return c.substring(name.length, c.length);
        }
      }
      return "";
    },
    strToObj(str) {
      var obj = {};
      if (str && typeof str === "string") {
        var objStr = str.match(/\{(.)+\}/g);
        eval("obj =" + objStr);
      }
      return obj;
    },
    
  },
  components: { DropDownListComponent, DropDownListPlugin },
};
</script>

<style>
@import "../../../node_modules/@syncfusion/ej2-base/styles/material.css";
@import "../../../node_modules/@syncfusion/ej2-inputs/styles/material.css";
@import "../../../node_modules/@syncfusion/ej2-vue-dropdowns/styles/material.css";
@import "/profile.css";
</style>