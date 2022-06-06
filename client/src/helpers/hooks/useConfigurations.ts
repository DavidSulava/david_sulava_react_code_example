import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';

const useConfigurations = () => {
  const dataState = useSelector((state: IState) => state.configurations.dataState)
  const configurationsList = useSelector((state: IState) => state.configurations.configurationsList)
  const isConfigLoading = useSelector((state: IState) => state.configurations.isConfigLoading)
  const searchedConfigList = useSelector((state: IState) => state.configurations.searchedConfigList)
  const configParams = useSelector((state: IState) => state.configurations.configParams)

  return {
    dataState,
    configurationsList,
    configParams,
    searchedConfigList,
    isConfigLoading
  }
}

export default useConfigurations