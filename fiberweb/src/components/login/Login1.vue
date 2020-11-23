<template>
<el-container> 
    <el-main>
        <el-form v-loading="loading" :model="ruleForm" status-icon :rules="rules" ref="ruleForm" label-width="140px" class="demo-ruleForm">
            <div class="title"><img src="../../assets/vnpt-large-logo.png" alt="VNPT"></div>
            <el-form-item label="TÊN ĐĂNG NHẬP" prop="username">
                <el-input type="text" v-model="ruleForm.username" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="MẬT KHẨU" prop="password">
                <el-input type="password" v-model="ruleForm.password" autocomplete="off"></el-input>
            </el-form-item>

            <el-form-item>
                <el-button type="primary" @click="submitForm('ruleForm')">Đăng nhập</el-button>
                <el-button @click="resetForm('ruleForm')">Làm mới</el-button>
            </el-form-item>
        </el-form>
    </el-main>
</el-container>
</template>

<script>
import FactoryRepository from "../../service/factory/repositoryfactory";
const auth = FactoryRepository.get("auth");
export default {
    data() {
      return {
        loading:false,
        ruleForm: {
          username: '',
          password: '',
        },
        rules: {
          username: [
            { required: true, message: 'Nhập tên đăng nhập', trigger: 'blur'  }
          ],
          password: [
            { required: true, message: 'Nhập mật khẩu', trigger: 'blur'  }
          ]
        }
      };
    },
    methods: {
      login() {
      var self = this;
      self.$message('Đang lấy thông tin người dùng');
      self.loading=true;
      auth.auth(self.ruleForm).then(function (response) {
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
            // self.$emit('changestatusloginEvent');
            self.$message({
                message: 'Đăng nhập thành công',
                type: 'success'
            });
            self.loading=false;
            window.location.href = "/";
          } else {
            self.loading=false;
            self.$message.error('Có lỗi xảy ra, kiểm tra lại thông tin đăng nhập');
          }
        })
        .catch( err=>{
            self.loading=false;
            self.$message.error(err)
        })
    },
      submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.login();
          } else {
            return false;
          }
        });
      },
      resetForm(formName) {
        this.$refs[formName].resetFields();
      }
    }
}
</script>

<style scoped>
@import url(login1.css);
</style>>

</style>