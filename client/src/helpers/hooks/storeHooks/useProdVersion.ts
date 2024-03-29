import { useSelector } from 'react-redux';
import { IState } from '../../../stores/configureStore';

const useProdVersion= () => {
  const dataState = useSelector((state: IState) => state.prodVersion.dataState)
  const prodVersionList = useSelector((state: IState) => state.prodVersion.prodVersionList)
  const prodVersion = useSelector((state: IState) => state.prodVersion.prodVersion)
  const isProdVersionLoading = useSelector((state: IState) => state.prodVersion.isProdVersionLoading)
  const isProdVersionListLoading = useSelector((state: IState) => state.prodVersion.isProdVersionListLoading)

  return {
    dataState,
    prodVersionList,
    prodVersion,
    isProdVersionLoading,
    isProdVersionListLoading,
  }
}

export default useProdVersion