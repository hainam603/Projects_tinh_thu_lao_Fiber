<template>
  <div id="app">
    <el-menu
      :default-active="'1'"
      class="el-menu-demo"
      mode="horizontal"
      background-color="#007FEC"
      text-color="#fff"
      active-text-color="#ffd04b">
      
      <el-menu-item index="0" v-if="islogin" @click="handleSelectComponent('HomeComponent')"><img src="./assets/white-vnpt.png" alt="" height="45" width="50"></el-menu-item>
      <el-menu-item index="1" v-if="islogin" @click="handleSelectComponent('FiberPTMTheothangComponent')">Danh sách fiber</el-menu-item>
      <el-menu-item index="2" v-if="islogin" @click="handleSelectComponent('FiberThulaoDot1Component')">Tính thù lao lần 1</el-menu-item>
      <el-menu-item index="3" v-if="islogin" @click="handleSelectComponent('FiberThulaoDot2Component')">Tính thù lao lần 2</el-menu-item>
      <el-submenu index="4" v-if="islogin" style="float:right">
          <template slot="title"> 
              <span style="margin-left:10px">{{username}}</span>
          </template>
          <el-menu-item index="4-1" @click="handleSelectComponent('userComponent')">Tạo người dùng mới</el-menu-item>
          <el-menu-item index="4-2" @click="handleSelectComponent('userupdateComponent')">Cập nhật thông tin</el-menu-item>
          <el-menu-item index="4-3" @click="handleSelectComponent('usermanagementComponent')">Quản lý người dùng</el-menu-item>
          <el-menu-item index="4-4" @click="signout()">Đăng xuất</el-menu-item>
      </el-submenu>

    </el-menu>
    
    <component :profile="profile" :islogin="islogin" v-on:changestatusloginEvent="changestatuslogin" :is="selectComponent" ></component>
    
  </div>
</template>

<script>

import fiberComponent from './components/fiber/fiber';
import loginComponent from './components/login/login1';
import userComponent from './components/user/user';
import userupdateComponent from './components/user/userupdate';
import usermanagementComponent from './components/user/usermanagement';
import profileComponent from './components/profile/profile';
import FiberThulaoDot1Component from './components/fiber/FiberThulaoDot1';
import FiberThulaoDot2Component from './components/fiber/FiberThulaoDot2';
import FiberPTMTheothangComponent from './components/fiber/FiberPTMTheothang';
import HomeComponent from './components/home/home';
import factory from './service/factory/repositoryfactory';
const user = factory.get("user");

export default {
  name: 'App',
  data(){
    return {
      profile:1,
      username:"User",
      islogin:false,
      selectComponent:'',
      active:'1',
    }
  },
  created() {
    this.getTokenByLocal();
  },
  methods:{
    handleSelectComponent(c,t)
    {
      this.selectComponent=c;
      this.active='';
      this.profile=t;
    },
    changestatuslogin(){
      var self=this;
      self.islogin=true;
      self.getTokenByLocal();
      self.selectComponent="FiberPTMTheothangComponent";
    },
    getTokenByLocal() {
      var self=this;
      var section_local = self.getCookie("token_session");
      if (section_local) {
        var tok = self.strToObj(self.getCookie("token_session"));     
        user.getById(parseInt(tok.usid)).then(response => {
          if (response.data.success) {
            self.username = response.data.data.userName;
            self.islogin = true;
            self.selectComponent='FiberPTMTheothangComponent';
          } else {
            self.islogin = false;
            self.selectComponent='loginComponent';
          }
        });
      } else {
          self.islogin = false;
          self.selectComponent='loginComponent';
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
      //convert string to object
      var obj = {};
      if (str && typeof str === "string") {
        var objStr = str.match(/\{(.)+\}/g);
        eval("obj =" + objStr);
      }
      return obj;
    },
    signout() {
      var self=this;
      document.cookie = "token_session=; expires=Thu, 01 Jan 1970 00:00:00 UTC";
      self.islogin = false;
      self.selectComponent='loginComponent';
    },
  },
  components:{
    fiberComponent,
    loginComponent,
    profileComponent,
    userComponent,
    userupdateComponent,
    usermanagementComponent,
    FiberThulaoDot1Component,
    FiberThulaoDot2Component,
    FiberPTMTheothangComponent,
    HomeComponent}
}
</script>

<style>
@import url(app.css);
</style>
