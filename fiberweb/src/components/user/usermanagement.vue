<template>
  <div class="usermanagement">
      <el-main>
          <div class="userupdateform" v-if="showformupdate">
            <userupdateComponent v-on:goback='goback' :userupdate='userupdate'></userupdateComponent>
          </div>
          <div class="usermanagementform">
            <h3> Quản lý người dùng</h3>
            <el-table
                border
                :data="posts">
                <el-table-column
                fixed
                label="#"
                width="50">
                    <template slot-scope="scope">
                <el-button @click="callUpdateUser(scope)"  type="text" size="small"><i class="el-icon-edit"></i></el-button>
                    </template>
                </el-table-column>
                <el-table-column
                fixed
                prop="idUser"
                label="ID"
                width="50">
                </el-table-column>
                <el-table-column
                prop="userName"
                label="Tên đăng nhập"
                width="200">
                </el-table-column>
                <el-table-column
                prop="role"
                label="Quyền"
                width="120">
                </el-table-column>
                <el-table-column
                prop="active"
                label="Hoạt động"
                width="120">
                </el-table-column>
                <el-table-column
                prop="status"
                label="Trạng thái"
                width="120">
                </el-table-column>
                
            </el-table>
            <el-pagination
            background
            @current-change="displayedPosts"
            @size-change="handleSizeChange"
            layout="sizes, prev, pager, next"
            :page-sizes="[5, 10, 20]"
            :page-size="perPage"
            :total="total">
            </el-pagination>
            <el-button @click="gohome();">Đóng</el-button>
          </div>
      </el-main>
  </div>
</template>

<script>
import factoryRepository from '../../service/factory/repositoryfactory';
import userRepository from '../../service/Impl/userrepository';
import userupdateComponent from './userupdate1';
import app from '../../App';
const factory =factoryRepository.get('user');
export default {
    data() {
        return{
            currentRow:null,
            showformupdate:false,
            tableData:[],
            userupdate:'',
            posts:[],
            page: 1,
			perPage: 5,
            pages: [],
            total:0
            
        }
    },
    created(){
        var self=this;
        self.getAllUser();
    },
    methods:{
        gohome(){
            this.$emit('changestatusloginEvent');
        },
        handleSizeChange(e){
            var self=this;
            self.perPage=e;
            self.displayedPosts(1);
        },
        displayedPosts(e){
            var self=this;
            self.page=e;
            self.posts=self.paginate(self.tableData);
        },
        paginate (posts) {
            var self=this;
			var page = self.page;
			var perPage = self.perPage;
			var from = (page * perPage) - perPage;
			var to = (page * perPage);
            return  posts.slice(from, to);
            
		},
        goback(){
            var self=this;
            this.showformupdate=false;
            self.getAllUser();

        },
        callUpdateUser(scope){
            var self=this;
            self.userupdate=scope.row;
            self.showformupdate=true;
        },
        getAllUser(){
            var self=this;
            userRepository.getAllUser().then(function(response){
                if(response.data.success)
                    self.tableData=[];
                    self.total=response.data.data.length;
                    response.data.data.forEach(element => {
                        var role;
                        if(element.role==1)
                            role='Quản trị';
                        else
                            role='Người dùng';
                        var active;
                        if(element.active)
                            active='Có';
                        else
                            active='Không';
                        var status
                        if(element.status==1)
                            status='Hoạt động';
                        else
                            status='Khóa';
                        self.tableData.push({
                            idUser:element.idUser,
                            userName:element.userName,
                            passWord:element.passWord,
                            role:role,
                            active:active,
                            status:status
                        }); 
                    });
                    self.posts=self.paginate(self.tableData);
            });
        }

    },
    
    components:{
        userupdateComponent
    }
}
</script>

<style>
@import url(/usermanagement.css);
</style>