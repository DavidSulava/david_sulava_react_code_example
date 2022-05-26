import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';
import { useEffect } from 'react';

const useProdVersion= () => {
  const dataState = useSelector((state: IState) => state.prodVersion.dataState)
  const prodVersions = useSelector((state: IState) => state.prodVersion.prodVersions)
  const isProdVersionLoading = useSelector((state: IState) => state.prodVersion.isProdVersionLoading)

  return {
    dataState,
    prodVersions,
    isProdVersionLoading
  }
}

export default useProdVersion