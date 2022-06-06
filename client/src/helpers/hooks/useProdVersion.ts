import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';
import { useEffect } from 'react';

const useProdVersion= () => {
  const dataState = useSelector((state: IState) => state.prodVersion.dataState)
  const prodVersionList = useSelector((state: IState) => state.prodVersion.prodVersionList)
  const prodVersion = useSelector((state: IState) => state.prodVersion.prodVersion)
  const isProdVersionLoading = useSelector((state: IState) => state.prodVersion.isProdVersionLoading)

  return {
    dataState,
    prodVersionList,
    prodVersion,
    isProdVersionLoading
  }
}

export default useProdVersion