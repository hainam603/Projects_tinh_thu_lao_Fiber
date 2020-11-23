<template>
  <div class="main-login">
    <div class="login-box">
        <img src="../../assets/vnpt-large-logo.png" alt="VNPT">
      <h1>Đăng nhập</h1>
      <form action="">
        <div class="textfied">
          <i class="fas fa-user"></i>
          <input
            type="text"
            name="username"
            placeholder="Tên đăng nhập"
            v-model="datalogin.username"
          />
        </div>
        <div class="textfied">
          <i class="fas fa-lock"></i>
          <input
            type="password"
            name="password"
            placeholder="Mật khẩu"
            v-model="datalogin.password"
          />
        </div>

        <input
          type="button"
          value="Đăng nhập"
          class="btn-login"
          v-on:click="login()"
        />
        <div class="text-alert">{{msg}}</div>
      </form>
    </div>
  </div>
</template>

<script>
import factory from "../../service/factory/repositoryfactory";
// header('Access-Control-Allow-Origin', 'http://localhost:8080') ;
const auth = factory.get("auth");
export default {
  data() {
    return {
      datalogin: {
        username: "",
        password: "",
      },
      //show name username on header
      token: "", //save token
      msg: ""
    };
  },
  props: {
  islogin: Boolean,
  },
  methods: {
    login() {
      var self = this;
      self.msg="Đang xử lý, xin chờ ...";
      auth
        .auth(self.datalogin)
        .then(function (response) {
          if (response.data.success) {
            self.token = response.data.data.access_token;
            var value = {
                usid: response.data.data.user.idUser,
                token: response.data.data.token.access_token
              };
            var today = new Date();
            var nexthour = new Date();
            nexthour.setDate(today.getDate() + 1);
            document.cookie = "token_session=" + JSON.stringify(value) + ";expires =" +nexthour +"";
            self.$emit('changestatusloginEvent');
          } else {
            self.msg="Sai thông tin đăng nhập";
          }
        });
    },
    
  },
};
</script>

<style>
@import url(login.css);
</style>