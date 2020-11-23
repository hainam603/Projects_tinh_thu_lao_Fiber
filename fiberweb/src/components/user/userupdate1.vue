<template>
  <div class="user">
    <el-container>
      <el-main>
        <el-form
           v-loading="loading"
          :model="ruleForm"
          :rules="rules"
          ref="ruleForm"
          label-width="200px"
          class="demo-ruleForm"
        >
          <div class="title">Cập nhật người dùng</div>
          <el-form-item label="Tên đăng nhập" prop="username">
            <el-input v-model="ruleForm.username" :disabled="true"></el-input>
            <div class="showInfoUser"></div>
          </el-form-item>

          <el-form-item label="Mật khẩu" prop="password">
            <el-input type="password" v-model="ruleForm.password"></el-input>
            <div class="showInfoUser"></div>
          </el-form-item>

           <el-form-item label="Nhập lại mật khẩu" prop="password1">
            <el-input type="password" v-model="ruleForm.password1"></el-input>
            <div class="showInfoUser"></div>
          </el-form-item>
          
          <el-form-item label="Phân quyền" prop="role">
            <el-select v-model="ruleForm.role" placeholder="Chọn quyền">
              <el-option v-for="item in roles" :key="item.value" :label="item.label" :value="item.value"></el-option>
            </el-select>  
            <div class="showInfoUser">{{role}}</div>
          </el-form-item>
          
          
          <el-form-item label="Hoạt động" prop="active">
                <el-radio-group v-model="ruleForm.active">
                <el-radio label="1">Có</el-radio>
                <el-radio label="0">Không</el-radio>
                </el-radio-group>
            </el-form-item>

          <el-form-item label="Trạng thái" prop="status">
            <el-select v-model="ruleForm.status" placeholder="Chọn trạng thái">
              <el-option v-for="item in statuses" :key="item.value" :label="item.label" :value="item.value"></el-option>
            </el-select>
            <div class="showInfoUser">{{status}}</div>
          </el-form-item>
          
          
          <el-form-item>
            <el-button type="primary" @click="submitForm('ruleForm')">Cập nhật người dùng</el-button>
            <!-- <el-button @click="resetForm('ruleForm')">Làm lại</el-button> -->
            <el-button @click="$emit('goback');">Đóng</el-button>
          </el-form-item>
        </el-form>
      </el-main>
    </el-container>
  </div>
</template>

<script>
import factoryRepository from '../../service/factory/repositoryfactory';
import repository from '../../service/repository/repository';
const userRepository = factoryRepository.get('user');
export default {
  props:['userupdate'],
  data() {
    var validatePass = (rule, value, callback) => {
       
        if (value != '' && value.length < 6)
            callback(new Error('Mật khẩu tối thiểu 6 ký tự'));
        if (this.ruleForm.checkPass !== '') {
        this.$refs.ruleForm.validateField('checkPass');
        }
        callback();
        
      };
      var validatePass2 = (rule, value, callback) => {
        if (value !== this.ruleForm.password) {
          callback(new Error('Mật khẩu không giống nhau'));
        } else {
          callback();
        }
      };
    return {
        loading:false,
        role:"",
        status:"",
      ruleForm: {
        username: "",
        password: "",
        password1: "",
        role: "",
        active: "",
        status: "",
      },
      roles: [{
          value: '1',
          label: 'Quản trị'
        }, {
          value: '2',
          label: 'Người dùng'
        }],
         statuses: [{
          value: '1',
          label: 'Hoạt động'
        }, {
          value: '2',
          label: 'Khóa'
        }],
      rules: {
    username: [
      {
        required: false,
        message: "Vui lòng nhập tên đăng nhập",
        trigger: "blur",
      },
      { min: 6, message: "Tối thiểu 6 ký tự", trigger: "blur" },
    ],
    
    password: [
        { validator: validatePass, trigger: 'blur' }
    ],

    password1: [
        { validator: validatePass2, trigger: 'blur' }
    ],
    role: [
      {
        required: true,
        message: "Vui lòng chọn quyền",
        trigger: "change",
      },
    ],
    active: [
      {
        required: true,
        message: "Vui lòng trọn 1 trạng thái hoạt động",
        trigger: "change",
      },
    ],
    status: [
      {
        required: true,
        message: "Vui lòng chọn trạng thái",
        trigger: "change",
      },
    ],
  },
    };
  },
  created() {
      this.getUserByID();
  },
  methods: {
   
    getUserByID() {
        var self=this;
        userRepository.getById(self.userupdate.idUser).then(function (response) {
        if (response.data.success) {
            var status = self.statuses.filter(function(elem){
                if(elem.value == response.data.data.status) return elem.value;
            });
            var role = self.roles.filter(function(elem){
                if(elem.value == response.data.data.status) return elem.value;
            });
            var active="1";
            if(!response.data.data.active)
                active="0";
            self.$set(self.ruleForm, "idUser", response.data.data.idUser);
            self.$set(self.ruleForm, "username", response.data.data.userName);
            self.$set(self.ruleForm, "active", active);
            self.role=role[0].label;
            self.status=status[0].label;
            console.log(self.ruleForm);
          }
        });
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
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
            var self=this;
            self.loading=true;
            var active=true;
                if(self.ruleForm.active==="0")
                    active=false;
            var user={
                'idUser':self.ruleForm.idUser,
                'userName':self.ruleForm.username,
                'passWord':self.ruleForm.password,
                'role':self.ruleForm.role,
                'active':active,
                'status':self.ruleForm.status
            }
            console.log(user);
            userRepository.update(user).then(function (response) {
                if (response.data.success) {
                    self.$message({
                        showClose: true,
                        message: 'Cập nhật thành công',
                        type: 'success'
                    });
                } else {
                    var msg="Đã xảy ra lỗi, thử lại sau"
                    if(response.data.message=='Username already exists')
                        msg="Đã tồn tại tài khoản này"
                    self.$message({
                    showClose: true,
                    message: msg,
                    type: 'error'
                    });
                }
            });
            
            self.loading=false;
        } else {
          return false;
        }
      });
    },
    resetForm(formName) {
    //   this.$refs[formName].resetFields();
      this.loading=true;
      this.getUserByToken();
      this.loading=false;
    },
  },
  
};
</script>

<style>
@import url(./user.css);
</style>