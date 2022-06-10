import DefaultConfigForm from './components/CreateConfigForm/DefaultConfigForm';
import React, { useEffect, useRef } from 'react';
import { useParams } from 'react-router-dom';
import useConfigurations from '../../../../helpers/hooks/useConfigurations';
import { useDispatch } from 'react-redux';
import { getSvfPath } from '../../../../stores/productConfigurations/reducer';
import AutodeskViewer from './components/AutodeskViewer/AutodeskVewer';

const ProductConfigurationPage = () => {
  const dispatch = useDispatch()
  const {configId} = useParams()
  const preview = useRef<HTMLDivElement>(null)
  const {svfPath} = useConfigurations()

  useEffect(()=>{
    if(configId)
      dispatch(getSvfPath(configId))
  }, [configId])

  return (
    <div className="page-body configuration-page">
      <div className="create-configuration-form">
        <h6 className="mt-3 config-page-header">Configuration details</h6>
        <DefaultConfigForm/>
      </div>
      <div className="config-svf" id="preview" ref={preview}>
        <AutodeskViewer urn={svfPath.url} elementId="preview"/>
      </div>
    </div>
  )
}

export default ProductConfigurationPage