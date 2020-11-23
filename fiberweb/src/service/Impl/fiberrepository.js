import repository from '../repository/repository';
const resource = "/fiber";

export default {
    getFiber() {
        return repository.get(`${resource}`);
    },
    getPTMFiber(data) {
        return repository.post(`${resource}/getPTMFiber`, data);
    },
    gettotalrecordedPTMFiber(data) {
        return repository.post(`${resource}/gettotalrecordedPTMFiber`, data);
    },
    getThuLaoFiber(data) {
        return repository.post(`${resource}/getThuLaoFiber`, data);
    },
    gettotalrecordedThuLaoFiber(data) {
        return repository.post(`${resource}/gettotalrecordedThuLaoFiber`, data);
    },
    LayDS_PTM_Fiber_Theothang(data){
        return repository.post(`${resource}/LayDS_PTM_Fiber_Theothang`, data)
    },
    LayDS_Thulao_Fiber_Theo_Thang_D1(data){
        return repository.post(`${resource}/LayDS_Thulao_Fiber_Theo_Thang_D1`, data)
    },
    LayDS_Thulao_Fiber_Theo_Thang_D2(data){
        return repository.post(`${resource}/LayDS_Thulao_Fiber_Theo_Thang_D2`, data)
    }
}