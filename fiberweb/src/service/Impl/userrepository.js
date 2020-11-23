import responsitory from '../repository/repository'
//import axios from 'axios';
const resource = '/user'
export default {
  getAllUser(){
    return responsitory.get(`${resource}/getAllUser`);
  },
  getById (id) {
    return responsitory.get(`${resource}/getById?id=${id}`)
  },
  create (user) {
    return responsitory.post(`${resource}/create`, user)
  },
  update (user) {
    return responsitory.put(`${resource}/update`, user)
  },
  delete (id) {
    return responsitory.delete(`${resource}/delete?id=${id}`)
  },
  createFisrtUser (){
    return responsitory.get(`${resource}/createFirstUser`)
  },
  search(dataFilter){
    return responsitory.post(`${resource}/search`,dataFilter)
  },
  changePass(data){
    return responsitory.post(`${resource}/changePass`,data)
  },
  deleteListUser(data){
    return responsitory.post(`${resource}/deleteListUser`,data)
  }
}
