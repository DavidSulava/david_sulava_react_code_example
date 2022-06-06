import useJwtData from './useJwtData';
import { getTariff, setUser } from '../../stores/authentication/reducer';
import { store } from '../../stores/configureStore';

const useUserFromToken = () => {
  const {
    isExpired,
    FirstName, LastName, UserId,
    Email, Phone, Role, token
  } = useJwtData()

  if(!isExpired && FirstName) {
    store.dispatch(setUser({
      id: UserId,
      firstName: FirstName,
      lastName: LastName,
      email: Email,
      phone: Phone,
      token: token,
      role: Role,
    }))
    store.dispatch(getTariff())
  }
  else {
    store.dispatch(setUser(null))
  }
}

export default useUserFromToken