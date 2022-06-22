import DefaultConfigForm from './components/DefaultConfigForm/DefaultConfigForm';
import React, { useEffect, useRef, useState } from 'react';
import { useParams } from 'react-router-dom';
import useConfigurations from '../../../../helpers/hooks/storeHooks/useConfigurations';
import { useDispatch } from 'react-redux';
import { getSvfPath } from '../../../../stores/productConfigurations/reducer';
import AutodeskViewer from './components/AutodeskViewer/AutodeskVewer';
import { ESvfStatus } from '../../../../types/producVersionConfigurations';
import { enumGetKey } from '../../../../helpers/enumFunctions';
import { Loader } from '@progress/kendo-react-indicators';

const ProductConfigurationPage = () => {
  const dispatch = useDispatch()
  const {configId} = useParams()
  const preview = useRef<HTMLDivElement>(null)
  const {svfPath, configuration} = useConfigurations()
  const [isShowViewer, setIsShowViewer] = useState(false)

  useEffect(() => {
    if(configId)
      dispatch(getSvfPath(configId))
  }, [configId])
  useEffect(()=>{
    if(configuration){
      const isShowViewer = (configuration && configuration.svfStatus === ESvfStatus.Ready)
      setIsShowViewer(isShowViewer)
    }
  },[configuration])

  return (
    <div className="page-body configuration-page">
      <div className="create-configuration-form">
        <h6 className="mt-3 config-page-header">Configuration details</h6>
        <DefaultConfigForm/>
      </div>

      <div className="config-svf" id="preview" ref={preview}>
        {
          !configuration ? <Loader type="pulsing" size="small"/> :
          !isShowViewer ?
            <div>
              <span> Model status: </span>  <i>{enumGetKey(configuration.svfStatus, ESvfStatus)}</i>
            </div>
            :
            <AutodeskViewer urn={svfPath.url} elementId="preview"/>
        }
      </div>
    </div>
  )
}

export default ProductConfigurationPage