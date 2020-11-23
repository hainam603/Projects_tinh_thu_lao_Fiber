import repository from '../repository/repository';
const resource = "/auth";
export default {
    auth(user) {
        // return repository.get(`${resource}?name=${username}&pwd=${password}`);
        return repository.post(`${resource}`,user);
    },
   
}