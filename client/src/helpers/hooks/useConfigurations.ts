import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';
import { BASE_URL } from '../../constants/api';
import { apiRoutes } from '../../services/api/routes';

const useConfigurations = () => {
  const dataState = useSelector((state: IState) => state.configurations.dataState)
  const configurationsList = useSelector((state: IState) => state.configurations.configurationsList)
  const configuration = useSelector((state: IState) => state.configurations.configuration)
  const isConfigLoading = useSelector((state: IState) => state.configurations.isConfigLoading)
  const searchedConfigList = useSelector((state: IState) => state.configurations.searchedConfigList)
  const configParams = useSelector((state: IState) => state.configurations.configParams)
  const svfPath = useSelector((state: IState) => state.configurations.svfPath)

  return {
    dataState,
    configurationsList,
    configuration,
    configParams,
    searchedConfigList,
    isConfigLoading,
    svfPath: {initial: svfPath, url: svfPath? apiRoutes.configurations.svf(configuration?.id??'') + '/' + svfPath : ''}
  }
}

export default useConfigurations