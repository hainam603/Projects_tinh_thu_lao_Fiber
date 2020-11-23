import fiberrepository from '../Impl/fiberrepository';
import authrepository from '../Impl/authrepository';
import userrepository from '../Impl/userrepository';

const responsitories = {
    fiber: fiberrepository,
    auth: authrepository,
    user: userrepository
  };
  export default {
    get: name => responsitories[name]
  };