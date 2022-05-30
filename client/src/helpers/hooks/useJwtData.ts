import { getLocalStorage } from '../localStorage';
import { ACCESS_TOKEN_KEY } from '../../types/user';
import jwt_decode from 'jwt-decode';
import { IAppJwtPayload } from '../../types/common';
import dayjs from 'dayjs';

const useJwtData = () => {
  const token = getLocalStorage(ACCESS_TOKEN_KEY);
  try {
    const decoded = jwt_decode<IAppJwtPayload>(token??'');
    const {
      OrganisationId,
      UserId,
      FirstName,
      LastName,
      Phone,
      Created,
      Email,
      Role,
      nbf,
      exp,
      iat
    } = decoded

    const isExpired = dayjs.unix(exp??0).diff(dayjs()) < 1

    return{
      OrganisationId,
      UserId,
      FirstName,
      LastName,
      Email,
      Phone,
      Role,
      Created,
      nbf,
      exp,
      isExpired,
      iat,
      token: token??''
    }
  }catch(e){
    return{
      OrganisationId : '',
      UserId : '',
      FirstName : '',
      LastName : '',
      Email : '',
      Phone : '',
      Role : '',
      Created : '',
      nbf : '',
      exp : '',
      isExpired : '',
      iat : '',
      token: token??''
    }
  }

}

export default useJwtData