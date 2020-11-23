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
          <div class="title">Tạo mới người dùng</div>
          <el-form-item label="Tên đăng nhập" prop="username">
            <el-input v-model="ruleForm.username"></el-input>
          </el-form-item>

          <el-form-item label="Mật khẩu" prop="password">
            <el-input type="password" v-model="ruleForm.password"></el-input>
          </el-form-item>

           <el-form-item label="Nhập lại mật khẩu" prop="password1">
            <el-input type="password" v-model="ruleForm.password1"></el-input>
          </el-form-item>
          
          <el-form-item label="Phân quyền" prop="role">
            <el-select v-model="ruleForm.role" placeholder="Chọn quyền">
              <el-option label="Quản trị" value="1"></el-option>
              <el-option label="Người dùng" value="2"></el-option>
            </el-select>
          </el-form-item>
          
          <el-form-item label="Hoạt động" prop="active">
                <el-radio-group v-model="ruleForm.active">
                <el-radio label="true">Có</el-radio>
                <el-radio label="false">Không</el-radio>
                </el-radio-group>
            </el-form-item>

          <el-form-item label="Trạng thái" prop="status">
            <el-select v-model="ruleForm.status" placeholder="Chọn trạng thái">
              <el-option label="Hoạt động" value="1"></el-option>
              <el-option label="Khóa" value="2"></el-option>
            </el-select>
          </el-form-item>
          
          
          <el-form-item>
            <el-button type="primary" @click="submitForm('ruleForm')">Tạo người dùng</el-button>
            <el-button @click="resetForm('ruleForm')">Làm mới</el-button>
            <el-button @click="gohome()">Đóng</el-button>
          </el-form-item>
        </el-form>
      </el-main>
    </el-container>
  </div>
</template>

<script>
import factoryRepository from '../../service/factory/repositoryfactory';
const userRepository = factoryRepository.get('user');
export default {
  data() {
    var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('Nhập mật khẩu'));
        } else {
          if (value.length < 6)
                callback(new Error('Mật khẩu tối thiểu 6 ký tự'));
          if (this.ruleForm.checkPass !== '') {
            this.$refs.ruleForm.validateField('checkPass');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('Nhập lại mật khẩu'));
        } else if (value !== this.ruleForm.password) {
          callback(new Error('Mật khẩu không giống nhau'));
        } else {
          callback();
        }
      };
    return {
        loading:false,
      ruleForm: {
        username: "",
        password: "",
        password1: "",
        role: "",
        active: "",
        status: "",
      },
      rules: {
    username: [
      {
        required: true,
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
  created() {},
  methods: {
    gohome(){
        this.$emit('changestatusloginEvent');
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
            var self=this;
            self.loading=true;
            var user={
                'userName':self.ruleForm.username,
                'passWord':self.ruleForm.password,
                'role':self.ruleForm.role,
                'active':self.ruleForm.active,
                'status':self.ruleForm.status
            }
            userRepository.create(user).then(function (response) {
                if (response.data.success) {
                    self.$message({
                        showClose: true,
                        message: 'Tạo người dùng thành công',
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
      this.$refs[formName].resetFields();
    },
  },
  
};
</script>

<style>
@import url(./user.css);
</style>