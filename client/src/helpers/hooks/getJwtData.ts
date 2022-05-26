import { getLocalStorage } from '../localStorage';
import { ACCESS_TOKEN_KEY } from '../../types/user';
import jwt_decode from 'jwt-decode';
import { IAppJwtPayload } from '../../types/common';

const getJwtData = () => {
  const token = getLocalStorage(ACCESS_TOKEN_KEY);
  const decoded = jwt_decode<IAppJwtPayload>(token??'');
  const {OrganisationId, UserId, nbf, exp, iat} = decoded

  return{
    OrganisationId,
    UserId,
    nbf,
    exp,
    iat
  }
}

export default getJwtData