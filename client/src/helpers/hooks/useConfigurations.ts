import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';

const useConfigurations = () => {
  const dataState = useSelector((state: IState) => state.configurations.dataState)
  const configurationsList = useSelector((state: IState) => state.configurations.configurationsList)
  const configuration = useSelector((state: IState) => state.configurations.configuration)
  const isConfigLoading = useSelector((state: IState) => state.configurations.isConfigLoading)
  const searchedConfigList = useSelector((state: IState) => state.configurations.searchedConfigList)
  const configParams = useSelector((state: IState) => state.configurations.configParams)

  return {
    dataState,
    configurationsList,
    configuration,
    configParams,
    searchedConfigList,
    isConfigLoading
  }
}

export default useConfigurations