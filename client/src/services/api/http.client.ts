import axios from "axios"
import { apiRoutes } from './routes';
import { ACCESS_TOKEN_KEY } from '../../types/user';
import { getLocalStorage } from '../../helpers/localStorage';
import { store } from '../../stores/configureStore';
import { setError } from '../../stores/common/reducer';

const token = getLocalStorage(ACCESS_TOKEN_KEY);
const getAuthHeaderString = (token: string|null) => `Bearer ${token}`

const client = axios.create({
  baseURL: apiRoutes.signIn,
  headers: {Authorization: getAuthHeaderString(token)}
});

axios.interceptors.request.use(async(req) => {
  store.dispatch(setError(''))
  if(req?.headers?.Authorization && !token) {
    const token = getLocalStorage(ACCESS_TOKEN_KEY);
    req.headers.Authorization = getAuthHeaderString(token)
  }
  return req;
}, function(error) {
  return Promise.reject(error);
});

export default client
