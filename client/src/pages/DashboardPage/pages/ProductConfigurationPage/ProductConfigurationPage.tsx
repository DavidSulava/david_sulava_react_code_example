import DefaultConfigForm from './components/CreateConfigForm/DefaultConfigForm';
import React from 'react';

const ProductConfigurationPage = () => {

  return (
    <div className="page-body configuration-page">
      <div className="create-configuration-form">
        <h6 className="mt-3 config-page-header">Configuration details</h6>
        <DefaultConfigForm/>
      </div>
      <div className="config-svf">
        <h5>SVF</h5>
      </div>
    </div>
  )
}

export default ProductConfigurationPage