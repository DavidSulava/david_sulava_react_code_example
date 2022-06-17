import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';

const useAppBundle= () => {
  const appBundleFilters = useSelector((state: IState) => state.appBundle.dataState)
  const appBundle = useSelector((state: IState) => state.appBundle.appBundle)
  const appBundleList = useSelector((state: IState) => state.appBundle.appBundleList)
  const appBundleTableList = useSelector((state: IState) => state.appBundle.appBundleTableList)
  const isBundleListLoading = useSelector((state: IState) => state.appBundle.isBundleListLoading)
  const isBundleLoading = useSelector((state: IState) => state.appBundle.isBundleLoading)

  return {
    appBundleFilters,
    appBundle,
    appBundleList,
    appBundleTableList,
    isBundleListLoading,
    isBundleLoading
  }
}

export default useAppBundle
