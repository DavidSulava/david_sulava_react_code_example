import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';

const useAuthCheck = () => {
  const user = useSelector((state: IState) => state.auth.user)
  const isUserLoading = useSelector((state: IState) => state.auth.isUserLoading)
  const tariff = useSelector((state: IState) => state.auth.tariff)

  return {
    user: user,
    authenticated: !!user,
    checkingAuth: isUserLoading,
    tariff
  }
}

export default useAuthCheck